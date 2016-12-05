using UnityEngine;
using System.Collections;

public class LaserBeamScript : MissleController {

	//private Rigidbody rb;
	//private Transform playerT;

	public override void Explode ()
	{
		Debug.Log ("Explode");
	}

	void OnCollisionEnter(Collision c)
	{
		if (c.gameObject.CompareTag ("Player")) {
			this.Explode ();
			Debug.Log ("Player hit!");
		}
	}
}
