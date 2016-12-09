using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	public float speedH = 2.0f;
	public float speedV = 2.0f;
	public float speedR = 2.0f;

	public float thrust = 100.0f;
	public float missleForce = 1500.0f;

	public float min;
	public float max;
	public int ammo;
	public bool isDead = false;
	public bool targetAcquired = false;
	public AudioClip dieSound;
	public AudioClip hitSound;

	public GameObject[] missles;

	public GameObject[] panel;
	public GameObject[] explosions;

	private float yaw = 0.0f;
	private float pitch = 0.0f;
	private float roll = 0.0f;
	private Rigidbody rb;
	private AudioSource audioSource;

	private PlayerStats stats;
	public Transform target;

	private bool isExploding=false;
	private bool isHoming = false;
	// Use this for initialization
	void Start () {
		//panel = GameObject.FindWithTag ("CrashPanel");
		rb = GetComponent<Rigidbody> ();
		stats = PlayerStats.Instance;
		audioSource = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (!stats.isDead) {
			yaw += speedH * Input.GetAxis ("Mouse X");
			pitch -= speedV * Input.GetAxis ("Mouse Y");

			if (Input.GetKey (KeyCode.LeftArrow))
				roll += speedR;
			else if (Input.GetKey (KeyCode.RightArrow))
				roll -= speedR;

			Vector3 deltaVec = new Vector3 (pitch, yaw, roll);
			deltaVec.x = Mathf.Clamp (deltaVec.x, min, max);

			transform.eulerAngles = deltaVec;
			if (Input.GetMouseButtonDown (0))
				Fire ();

			if (Input.GetMouseButtonDown (1))
			{
				isHoming = !isHoming;
				SetPanel (panel[4]);
			}

			if (Input.GetKeyDown (KeyCode.Space))
				AcquireTarget ();

			if (Input.GetKey (KeyCode.UpArrow)) {
				rb.AddForce (transform.forward * thrust);
				playThrust ();
			} else if (Input.GetKey (KeyCode.DownArrow)) {
				rb.AddForce (transform.forward * thrust * -1);
				playThrust ();
			}

			if (Input.GetKey (KeyCode.Escape)) {
				SceneManager.LoadScene ("GameOver");
			}

		} else {
			isDead = true;
			if (!isExploding) {
				isExploding = true;
				StartCoroutine ("PlayerDie");
			}
		}
	}

	void SetPanel(GameObject gb)
	{
		if (gb.activeSelf) {
			gb.SetActive (false);
		} else
			gb.SetActive (true);
	}

	void OnCollisionEnter(Collision c)
	{
		if (c.gameObject.CompareTag ("Asteroid")||c.gameObject.CompareTag("Enemy")) 
		{
			//if(!ForceFieldScript.Instance.isImmune)
			//{
				//Camera.main.GetComponent<CameraShake> ().StartShake ();
				//StartCoroutine ("notifyCrash");
			//}
		}
	}
	public void AcquireTarget()
	{
			RaycastHit hit;
			if (Physics.Raycast (transform.position, transform.forward, out hit))
			if (hit.collider.gameObject.CompareTag ("Asteroid")) {
				panel [3].gameObject.GetComponent<Image> ().color = Color.red;
				target = hit.collider.gameObject.transform;
				targetAcquired = true;
			}

	}
	public void Fire()
	{
		if (target != null&&ammo>0) {
			GameObject foo = (GameObject)Instantiate (missles [1], transform.forward + transform.position, Quaternion.identity);
			foo.BroadcastMessage ("setTarget", target.transform);
			ammo--;
		}
		else
		{
			GameObject foo = (GameObject)Instantiate (missles [0], transform.forward + transform.position, Quaternion.identity);
			Rigidbody missleRB = foo.GetComponent<Rigidbody> ();
			missleRB.AddForce (transform.forward * missleForce);
		}
		target = null;
		targetAcquired = false;
		panel [3].gameObject.GetComponent<Image> ().color = Color.white;
		//
	}
	public void StartNotifyCrash()
	{
		StartCoroutine("notifyCrash");
	}

	//Activates a UI panel to notify player they have collided with something
	public IEnumerator notifyCrash()
	{
		//we yield for one frame to see if the collision kills tha player. without this then both panels appear on death and it looks bad
		yield return new WaitForEndOfFrame ();
		if (!isDead) {
			if (!panel [0].activeSelf) {
				panel [0].SetActive (true);
				panel [2].SetActive (true);
				yield return new WaitForSeconds (3.0f);
				panel [0].SetActive (false);
				panel [2].SetActive (false);
			}
		}
	}


	//Instantiates a couple of explosions and then deletes the script which adds a cool spinning effect to the camera for some reason
	public IEnumerator PlayerDie()
	{
		Vector3 vec;

		//if crash panel is active, deactivate it
		//then turn on the "You Died" panel
		panel [0].SetActive (false);
		panel [1].SetActive (true);

		//Instantiates missleExplosions in the four different extremes of screen, also plays the explosion noise 4 times
		for (int i = 0; i < 4; i++)
		{
			switch (i)
			{
			case 1:
				vec = new Vector3 (2, 0, 0);
				break;
			case 2:
				vec = new Vector3 (-2, 0, 0);
				break;
			case 3:
				vec = new Vector3 (0, 1, 0);
				break;
			default:
				vec = new Vector3 (0, -1, 0);
				break;
			}
			Debug.Log (transform.position + (Vector3.forward * 2) + vec);
			Instantiate (explosions [1], transform.position + (transform.forward) +vec, Quaternion.identity);
			audioSource.PlayOneShot(dieSound);
			yield return new WaitForSeconds(.25f);
		}
		//Insatiates the asteroid explosion and then deletes this script
		Instantiate (explosions [0], transform.position, Quaternion.identity);
		Destroy (this);
	}
	public void playSound()
	{
		audioSource.PlayOneShot (hitSound);
	}
	void playThrust()
	{
		if (!audioSource.isPlaying)
			audioSource.Play();
	}


}
