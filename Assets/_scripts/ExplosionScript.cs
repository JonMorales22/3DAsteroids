using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {
	Transform playerT;

	public float radius = 10.0F;
	public float power;

	void Start() {
		Vector3 explosionPos = transform.position;

		//finds any gameobjects with colliders in a sphere
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		foreach (Collider hit in colliders) {
			Rigidbody rb = hit.GetComponent<Rigidbody>();

			//makes sure there is a rigidbody attached to each collider 
			if (rb != null)
				rb.AddExplosionForce(power, explosionPos, radius);
		}
			
		ParticleSystem explosion = GetComponent<ParticleSystem> ();
		explosion.Play ();
		Destroy (gameObject, explosion.duration);
	}
}
