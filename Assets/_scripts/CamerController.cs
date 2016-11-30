using UnityEngine;
using System.Collections;

public static class AsteroidCounter
{
	public static int counter;

	public static void setCounter(int num)
	{
		counter = num;
	}
}

public class CamerController : MonoBehaviour {
	public float speedH = 2.0f;
	public float speedV = 2.0f;
	public float speedR = 2.0f;

	public float thrust = 100.0f;
	public float missleForce = 500.0f;

	public float min;
	public float max;

	public GameObject missle;
	public AudioClip[] clips;
	public GameObject panel;


	private float yaw = 0.0f;
	private float pitch = 0.0f;
	private float roll = 0.0f;
	private Rigidbody rb;
	private AudioSource audioSource;

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
			Debug.Log ("Quit!");
			Application.Quit ();
		}
	}

	void OnCollisionExit(Collision c)
	{
		if (c.gameObject.CompareTag ("Asteroid")) 
		{
			StartCoroutine ("notifyCrash");
			audioSource.PlayOneShot (clips [0]);
		}

	}

	public void Fire()
	{
		audioSource.PlayOneShot (clips [2]);
		GameObject foo = (GameObject)Instantiate (missle, transform.forward+transform.position, Quaternion.identity);
		Rigidbody rb = foo.GetComponent<Rigidbody> ();
		rb.AddForce (transform.forward * missleForce);
		//foo.transform.rotation = Quaternion.LookRotation (rb.velocity);
	}

	public IEnumerator notifyCrash()
	{
		if (!panel.activeSelf)
		{
			panel.SetActive (true);
			yield return new WaitForSeconds (2.0f);
			panel.SetActive (false);
		}

	}

	//AUDIO FUNCTIONS!!!!!
	public void playExplosion()
	{
		audioSource.PlayOneShot (clips [1]);
	}

	void playThrust()
	{
		if (!audioSource.isPlaying)
			audioSource.PlayOneShot (clips [3]);
	}


}
