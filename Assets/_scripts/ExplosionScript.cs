using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {
	Transform playerT;
	//private Rigidbody rb;
	// Use this for initialization

	public float radius = 10.0F;
	public float power;

	void Start() {
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		foreach (Collider hit in colliders) {
			Rigidbody rb = hit.GetComponent<Rigidbody>();

			if (rb != null)
				rb.AddExplosionForce(power, explosionPos, radius);

		}
		ParticleSystem explosion = GetComponent<ParticleSystem> ();
		explosion.Play ();
		//GetComponent<Rigidbody> ().AddExplosionForce (1000,transform.position,0);
		Destroy (gameObject, explosion.duration);
	}
}
