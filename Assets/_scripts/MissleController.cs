using UnityEngine;
using System.Collections;

public class MissleController : MonoBehaviour {
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, 5.0f);
		rb = GetComponent<Rigidbody> ();

	}

	void Update()
	{
		transform.rotation = Quaternion.LookRotation(rb.velocity);
	}
	
	void OnCollisionEnter(Collision c)
	{
		if (c.gameObject.CompareTag ("Meteor")) {
			Destroy (c.gameObject);
			Destroy(gameObject);
		}
	}
		
}
