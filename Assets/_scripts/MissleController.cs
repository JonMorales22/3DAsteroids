using UnityEngine;
using System.Collections;

public class MissleController : MonoBehaviour {
	private Rigidbody rb;
	private PlayerController script;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, 5.0f);
		script = GameObject.FindWithTag ("Player").GetComponent<PlayerController>();
		rb = GetComponent<Rigidbody> ();
	}

	void Update()
	{
		transform.rotation = Quaternion.LookRotation(rb.velocity);
	}
		
}
