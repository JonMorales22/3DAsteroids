﻿using UnityEngine;
using System.Collections;

public class LaserBeamScript : MissleController {

	//private Rigidbody rb;
	//private Transform playerT;
	//public AudioClip hitPlayer;
	public GameObject hitPlayer;

	public override void Explode ()
	{
		//Instantiate (hitPlayer, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}

	void OnCollisionEnter(Collision c)
	{
		if (c.gameObject.CompareTag ("Player")) {
			GameObject obj = (GameObject)Instantiate (hitPlayer, transform.position, Quaternion.identity);
			Destroy (obj, 3);
			Destroy (gameObject);
		}
	}
}
