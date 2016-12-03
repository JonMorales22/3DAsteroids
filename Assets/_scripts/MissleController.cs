using UnityEngine;
using System.Collections;

public class MissleController : MonoBehaviour {
	
	public GameObject explosion;

	private Rigidbody rb;
	private Transform playerT;
	private GameObject player;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, 5.0f);
		playerT= GameObject.FindWithTag ("Player").GetComponent<Transform>();
		player = GameObject.FindWithTag ("Player");
		rb = GetComponent<Rigidbody> ();
	}

	void Update()
	{
		transform.rotation = Quaternion.LookRotation(rb.velocity);
	}

	void OnDestroy()
	{
		Explode ();
	}

	void Explode()
	{
		GameObject gb = (GameObject)Instantiate (explosion, transform.position, Quaternion.identity);
		gb.transform.LookAt (playerT);
		float distance= getDistance ();

		if (distance < 6.0f) {
			/*
			 * y=(sqrt(25-x^2))5 ---> when we are at x=5, y=0 so the camera won't shake
			 * 					 ---> when we are at x=1, y=4.8 so the camera will shake at nearly what I want the max to be.
			*/
			float shakeAmount = (Mathf.Sqrt (36 - (distance*distance)))/6;
			Debug.Log ("Distance: " + distance);
			Debug.Log ("Shake Amount: " + shakeAmount);
			Camera.main.GetComponent<CameraShake> ().StartShake (shakeAmount);
				}
		Destroy (gameObject);
	}

	float getDistance()
	{
		float xVal,yVal,zVal;

		xVal = Mathf.Sqrt ((transform.position.x - playerT.position.x) * (transform.position.x - playerT.position.x));
		yVal = Mathf.Sqrt ((transform.position.y - playerT.position.y) * (transform.position.y - playerT.position.y));
		zVal = Mathf.Sqrt ((transform.position.z - playerT.position.z) * (transform.position.z - playerT.position.z));

		return Mathf.Sqrt (xVal + yVal + zVal);
	}
		
}
