using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour {
	public float force;

	private int rotation;
	private Vector3 dirVec;
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		dirVec = randVector ();
		rotation = Random.Range (5, 50);
		force = Random.Range (1, 26);
		//rb.AddTorque (new Vector3 (1, 0, 0) * force);
		//rb.AddTorque (randVector()*force);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rb.AddTorque (dirVec * force);
		transform.RotateAround(Vector3.zero, dirVec, rotation * Time.deltaTime);
		//Vector3.up
	}

	Vector3 randVector()
	{
		int num = Random.Range (0, 6);
		Vector3 vec;
		switch (num)
		{
		case 1:
			vec = new Vector3 (0, 0, 1);
			break;
		case 2:
			vec = new Vector3 (0, 1, 0);
			break;
		case 3:
			vec = new Vector3 (1, 0, 0);
			break;
		case 4:
			vec = new Vector3 (0, 0, -1);
			break;
		case 5:
			vec = new Vector3 (0, -1, 0);
			break;
		default:
			vec = new Vector3 (-1, 0, 0);
			break;
		}
		return vec;
	}

	int randNumber()
	{
		return Random.Range (5, 21);
	}
}
