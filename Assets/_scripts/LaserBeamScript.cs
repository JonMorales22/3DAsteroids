using UnityEngine;
using System.Collections;

public class LaserBeamScript : MissleController {

	//private Rigidbody rb;
	//private Transform playerT;
	//public AudioClip hitPlayer;
	public GameObject hitPlayer;
	public GameObject normExplode;

	public override void Explode ()
	{
		//Instantiate (hitPlayer, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}

	void OnCollisionEnter(Collision c)
	{
		if (c.gameObject.CompareTag ("Player")) {
			GameObject obj = (GameObject)Instantiate (hitPlayer, playerT.position+(playerT.forward*2), Quaternion.identity);
			//obj.transform.LookAt (playerT);
			Destroy (gameObject);
		}
		if (c.gameObject.CompareTag ("Asteroid")) {
			Instantiate (normExplode, transform.position, Quaternion.identity);
		}
	}
}
