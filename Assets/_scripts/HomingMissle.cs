using UnityEngine;
using System.Collections;

public class HomingMissle: MonoBehaviour{
	public float force;
	public float turn;
	public Rigidbody rb;
	public GameObject explosion;

	private Transform playerT;
	private bool targetAcquired = false;
	private Transform target;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, 5.0f);

		playerT= GameObject.FindWithTag ("Player").GetComponent<Transform>();
		rb = GetComponent<Rigidbody> ();
	}
	void setTarget(Transform t)
	{
		this.target = t;
		targetAcquired = true;
	}
	void Explode()
	{
		GameObject gb = (GameObject)Instantiate (explosion, transform.position, Quaternion.identity);
		gb.transform.LookAt (playerT);
		//float distance= getDistance ();

		//if (distance < 6.0f) {
			/*
			 * y=(sqrt(36-x^2))6 ---> when we are at x=6, y=0 so the camera won't shake
			 * 					 ---> when we are at x=1, y=.98 so the camera will shake at nearly what I want the max to be.
			*/
			//float shakeAmount = (Mathf.Sqrt (36 - (distance*distance)))/6;

		//}
		Camera.main.GetComponent<CameraShake> ().StartShake ();
		Destroy (gameObject);
	}
	// Update is called once per frame
	void FixedUpdate () {
		if (targetAcquired) {
			rb.velocity = transform.forward * force;

			Quaternion targetRot = Quaternion.LookRotation (target.position - transform.position);
			rb.MoveRotation (Quaternion.RotateTowards (transform.rotation, targetRot, turn));
		}
	}
}
