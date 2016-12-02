using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {
	Transform playerT;
	// Use this for initialization
	void Start () {
		ParticleSystem explosion = GetComponent<ParticleSystem> ();
		explosion.Play ();
		Destroy (gameObject, explosion.duration);
	}
}
