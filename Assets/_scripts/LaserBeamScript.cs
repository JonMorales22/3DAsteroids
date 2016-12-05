using UnityEngine;
using System.Collections;

public class LaserBeamScript : MonoBehaviour {
	public float speed;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward* -speed;

	}
}
