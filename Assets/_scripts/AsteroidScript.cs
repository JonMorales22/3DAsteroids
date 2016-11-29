using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour {
	public float force=750.0f;
	public float distance;
	public Transform player;

	private float randForce;
	private int rotation;
	private Vector3 dirVec;
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		randForce = Random.Range (5, 50);
		rb = GetComponent<Rigidbody> ();
		dirVec = randVector ();
		rb.AddForce (dirVec * force);
	}

	// Update is called once per frame
	void FixedUpdate () {
		rb.AddTorque (dirVec * randForce);
		if (Mathf.Abs (transform.position.x) > distance || Mathf.Abs (transform.position.y) > distance || Mathf.Abs (transform.position.z) > distance) 
		{
			//transform.position = (Random.onUnitSphere)*100;

			//Debug.Log ("Called");
			killForces ();
			//rb.velocity=Vector3.zero;
			rb.AddForce ((Vector3.zero-dirVec) * force);
		}
			//transform.RotateAround(Vector3.zero, dirVec, rotation * Time.deltaTime);
		//Vector3.up
	}

	public void Attack()
	{
		Vector3 rand = randVectorRadius ();
		//killForces ();
		rb.AddForce (((player.transform.position - transform.position) + rand) * 100.0f);
	}

	void killForces()
	{
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}

	Vector3 randVectorRadius()
	{
		int x = Random.Range (-3, 4);
		int y = Random.Range (-3, 4);
		int z = Random.Range (-3, 4);
		Vector3 vec = new Vector3 (x,y,z);
		return vec;
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

}
