using UnityEngine;
using System.Collections;

public class MissleController : MonoBehaviour {
	
	public GameObject explosion;

	public Rigidbody rb;
	public Transform playerT;

	private float torqueSpeed=500;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, 5.0f);

		playerT= GameObject.FindWithTag ("Player").GetComponent<Transform>();
		rb = GetComponent<Rigidbody> ();		
	}

	//I don't remember why I put this here
	void Update()
	{
		transform.rotation = Quaternion.LookRotation(rb.velocity);
	}

	void OnDestroy()
	{
		Debug.Log ("Destroy");
		Explode ();
	}

	//Creates an explosion and then calculates the missle's distance from the player. It then shakes the camera based on the distance to the player 
	public virtual void Explode()
	{
		GameObject gb = (GameObject)Instantiate (explosion, transform.position, Quaternion.identity);
		gb.transform.LookAt (playerT);
		float distance= getDistance ();

		if (distance < 6.0f) {
			/*
			 * y=(sqrt(36-x^2))6 ---> when we are at x=6, y=0 so the camera won't shake
			 * 					 ---> when we are at x=1, y=.98 so the camera will shake at nearly what I want the max to be.
			*/
			float shakeAmount = (Mathf.Sqrt (36 - (distance*distance)))/6;
			Camera.main.GetComponent<CameraShake> ().StartShake (shakeAmount);
		}
		Destroy (gameObject);
	}

	//get the distance from the player
	float getDistance()
	{
		float xVal,yVal,zVal;

		xVal = Mathf.Sqrt ((transform.position.x - playerT.position.x) * (transform.position.x - playerT.position.x));
		yVal = Mathf.Sqrt ((transform.position.y - playerT.position.y) * (transform.position.y - playerT.position.y));
		zVal = Mathf.Sqrt ((transform.position.z - playerT.position.z) * (transform.position.z - playerT.position.z));

		return Mathf.Sqrt (xVal + yVal + zVal);
	}
		
}
