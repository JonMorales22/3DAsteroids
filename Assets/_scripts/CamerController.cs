using UnityEngine;
using System.Collections;

public class CamerController : MonoBehaviour {
	public float speedH = 2.0f;
	public float speedV = 2.0f;
	public float speedR = 2.0f;

	public float thrust = 100.0f;
	public float missleForce = 500.0f;

	public float min;
	public float max;

	public GameObject missle;
	public GameObject spawn;

	private float yaw = 0.0f;
	private float pitch = 0.0f;
	private float roll = 0.0f;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
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
			rb.AddForce (transform.forward * thrust);
		else if (Input.GetKey (KeyCode.DownArrow))
			rb.AddForce (transform.forward * thrust*-1);
	}

	public void Fire()
	{
		GameObject foo = (GameObject)Instantiate (missle, transform.forward+transform.position, Quaternion.identity);
		Rigidbody rb = foo.GetComponent<Rigidbody> ();
		rb.AddForce (transform.forward * missleForce);
		//foo.transform.rotation = Quaternion.LookRotation (rb.velocity);
	}

	public void Thrust()
	{


	}
}
