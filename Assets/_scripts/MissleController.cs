using UnityEngine;
using System.Collections;

public class MissleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 5.0f);
	}
	
	void OnCollisionEnter(Collision c)
	{
		if (c.gameObject.CompareTag ("Meteor")) {
			Destroy (c.gameObject);
			Destroy(gameObject);
		}
	}
		
}
