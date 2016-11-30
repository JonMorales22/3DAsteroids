using UnityEngine;
using System.Collections;

public class MissleController : MonoBehaviour {
	private Rigidbody rb;
	private CamerController script;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, 5.0f);
		script = GameObject.FindWithTag ("Player").GetComponent<CamerController>();
		rb = GetComponent<Rigidbody> ();

	}

	void Update()
	{
		transform.rotation = Quaternion.LookRotation(rb.velocity);
	}
	
	void OnCollisionEnter(Collision c)
	{
		if (c.gameObject.CompareTag ("Asteroid")) {
			AsteroidCounter.counter--;
			//Debug.Log (AsteroidCounter.counter);
			script.playExplosion ();
			Destroy (c.gameObject);
			Destroy(gameObject);
		}
	}
		
}
