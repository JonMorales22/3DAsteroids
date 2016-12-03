using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speedH = 2.0f;
	public float speedV = 2.0f;
	public float speedR = 2.0f;

	public float thrust = 100.0f;
	public float missleForce = 500.0f;

	public float min;
	public float max;

	public GameObject missle;
	//public AudioClip[] clips;
	public GameObject panel;

	private bool isCrashing;
	private float yaw = 0.0f;
	private float pitch = 0.0f;
	private float roll = 0.0f;
	private Rigidbody rb;
	private AudioSource audioSource;


	//FOR CAMERA STUFF
	public float decreaseFactor;
	public float shakeAmount;
	public float shakeRange;

	private float shake;
	// Use this for initialization
	void Start () {
		//panel = GameObject.FindWithTag ("CrashPanel");
		rb = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update ()
	{
		yaw += speedH * Input.GetAxis("Mouse X");
		pitch -= speedV * Input.GetAxis("Mouse Y");

		if (Input.GetKey (KeyCode.LeftArrow))
			roll += speedR;
		else if (Input.GetKey (KeyCode.RightArrow))
			roll -= speedR;

		Vector3 deltaVec = new Vector3 (pitch, yaw, roll);
		deltaVec.x = Mathf.Clamp (deltaVec.x, min, max);

		transform.eulerAngles = deltaVec;
		if (Input.GetMouseButtonDown (0))
			Fire ();

		if (Input.GetKey (KeyCode.UpArrow))
		{
			rb.AddForce (transform.forward * thrust);
			playThrust ();
		}
		else if (Input.GetKey (KeyCode.DownArrow))
		{
			rb.AddForce (transform.forward * thrust * -1);
			playThrust ();
		}

		if (Input.GetKey (KeyCode.Escape))
		{
			Application.Quit ();
		}

		//FOR DEBUG
		if (Input.GetKeyDown (KeyCode.Space)) {
			StartCoroutine ("CameraShake");
		
		}
	}

	void OnCollisionEnter(Collision c)
	{
		if (c.gameObject.CompareTag ("Asteroid")) 
		{
			if (!isCrashing) {
				isCrashing = true;
				Camera.main.GetComponent<CameraShake> ().StartShake ();
				StartCoroutine ("notifyCrash");
			}
		}

	}

	public void StartCameraShake()
	{
		StartCoroutine("CameraShake");
	}

	IEnumerator CameraShake(float shakeAmount)
	{
		Vector3 initialPos = transform.position;
		shake = 1;
		while (shake > 0.0f)
		{
			//Debug.Log (Mathf.PerlinNoise (shakeRange, 0));
			transform.position = initialPos+Random.insideUnitSphere*shakeAmount;
			shake -= Time.deltaTime*decreaseFactor;
			yield return new WaitForSeconds(.01f);
		}
		transform.position = initialPos;
	}

	public void Fire()
	{
		GameObject foo = (GameObject)Instantiate (missle, transform.forward+transform.position, Quaternion.identity);
		Rigidbody missleRB = foo.GetComponent<Rigidbody> ();
		missleRB.AddForce (transform.forward * missleForce);
	}

	public IEnumerator notifyCrash()
	{
		if (!panel.activeSelf)
		{
			panel.SetActive (true);
			yield return new WaitForSeconds (3.0f);
			panel.SetActive (false);
		}
		isCrashing = false;
	}

	void playThrust()
	{
		if (!audioSource.isPlaying)
			audioSource.Play();
	}


}
