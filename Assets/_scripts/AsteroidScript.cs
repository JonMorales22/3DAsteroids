using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour {

	public float force=750.0f;
	public float distance;


	public GameObject explosion;

	private float randForce;
	private int rotation;
	private Vector3 dirVec;
	private Rigidbody rb;
	private AudioSource audiosource;
	private Transform player;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		player = GameObject.FindWithTag ("Player").GetComponent<Transform>();
		audiosource = GetComponent<AudioSource> ();
		//gets a random float to apply torque force to asteroid
		//randForce = Random.Range (5, 10);

		//gets a random vector and then sends the asteroid in a random direction
		//audiosource=GetComponent<AudioSource>();
		dirVec = randVector ();
		rb.AddForce (dirVec * force);
		//rb.AddTorque (dirVec * force);
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		//adds a spin to the asteroid
		rb.AddTorque (dirVec * 0.25f);

		//if the asteroid is "out of bounds", place it back in bounds and send it a random point in a sphere around the origin
		if (Mathf.Abs (transform.position.x) > distance || Mathf.Abs (transform.position.y) > distance || Mathf.Abs (transform.position.z) > distance) 
		{
			Vector3 rand = randVectorRadius (-5,5);
			killForces ();
			transform.position = Random.onUnitSphere * 100;
			//rb.AddForce (((Vector3.zero-transform.position+rand)) * 10.0f);
			rb.AddForce (((player.transform.position - transform.position) + rand) * 10.0f);
		}
	}

	void OnCollisionEnter(Collision c)
	{
		//First calls explode, then destroys the missle prefab
		if (c.gameObject.CompareTag ("Missle"))
		{
			PlayerStats.Instance.IncrementScore (100);
			ForceFieldScript.Instance.Recharge (10);
			Explode ();
			Destroy (c.gameObject);
		}
		if (c.gameObject.CompareTag ("Player"))
		{
			//Rigidbody playerRB = c.gameObject.GetComponent<Rigidbody>();
			//playerRB.AddForce (-1*player.position*50);
			if(!c.gameObject.GetComponent<PlayerController>().isDead)
				playSound ();
		}

	}

	void OnCollisionExit(Collision c)
	{

	}

	//Instantiantes the Explosion prefab at the asteroids current position
	//and decrements the amount of asteroids by 1, then destroys the GameObject
	void Explode()
	{
		Instantiate (explosion, transform.position, Quaternion.identity);
		AsteroidCounter.counter--;
		Destroy (gameObject);
	}
		
	//Sends the asteroid towards the a point in a sphere around the player's current position
	public void Attack()
	{
		killForces ();
		Vector3 rand = randVectorRadius (-3,4);
		rb.AddForce (((player.transform.position - transform.position) + rand) * 10.0f);
	}

	//neutralizes the current forces acting on the rigidbody
	void killForces()
	{
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}

	//returns a random Vector in range specified by num & num2
	Vector3 randVectorRadius(int num, int num2)
	{
		int x = Random.Range (num, num2);
		int y = Random.Range (num, num2);
		int z = Random.Range (num, num2);
		Vector3 vec = new Vector3 (x,y,z);
		return vec;
	}

	//returns a random unit vector
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

	void playSound()
	{
		if (!audiosource.isPlaying)
			audiosource.Play();
	}


}
