using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour {

	public float force=750.0f;
	public float torqueForce=250;
	public float distance;
	public float spawn1upChance = 10;
	public int value =100;

	public GameObject explosion;
	public GameObject OneUp;
	public GameObject SmallAsteroid;


	private float randForce;
	private int rotation;
	private Vector3 dirVec;
	[HideInInspector]
	public Rigidbody rb;
	[HideInInspector]
	public AudioSource audiosource;
	[HideInInspector]
	public Transform player;

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
		//rb.AddForce (dirVec * force);
		//rb.AddTorque (dirVec*torqueForce);
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		//adds a spin to the asteroid
		//rb.AddTorque (dirVec * 0.25f);

		//if the asteroid is "out of bounds", place it back in bounds and send it a random point in a sphere around the origin
		if (Mathf.Abs (transform.position.x) > distance || Mathf.Abs (transform.position.y) > distance || Mathf.Abs (transform.position.z) > distance) 
		{
			Vector3 rand = randVectorRadius (-5,5);
			killForces ();
			transform.position = Random.onUnitSphere * 100;
			//rb.AddForce (((Vector3.zero-transform.position+rand)) * 10.0f);
			rb.AddForce (((player.transform.position - transform.position) + rand) * 10.0f);
			rb.AddTorque (((player.transform.position - transform.position) + rand) * 2.0f);
		}
	}

	void OnCollisionEnter(Collision c)
	{
		//First calls explode, then destroys the missle prefab
		if (c.gameObject.CompareTag ("Missle"))
		{
			PlayerStats.Instance.IncrementScore (value);
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

	//Instantiantes the Explosion prefab at the asteroids current position
	//and decrements the amount of asteroids by 1, then destroys the GameObject
	public virtual void Explode()
	{

		Instantiate (SmallAsteroid, transform.position+transform.right*-2, Quaternion.identity,transform.parent);
		Instantiate (SmallAsteroid, transform.position+transform.right*2, Quaternion.identity,transform.parent);
		Instantiate (explosion, transform.position, Quaternion.identity);
		//rb.AddExplosionForce (1000, transform.position, 100);
		if(Random.Range(0,100)<=spawn1upChance)
			Instantiate (OneUp, transform.position, Quaternion.identity);
		AsteroidCounter.decrememt (1);
		AsteroidCounter.increment (2);
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
	public void killForces()
	{
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}

	//returns a random Vector in range specified by num & num2
	public Vector3 randVectorRadius(int num, int num2)
	{
		int x = Random.Range (num, num2);
		int y = Random.Range (num, num2);
		int z = Random.Range (num, num2);
		Vector3 vec = new Vector3 (x,y,z);
		return vec;
	}

	//returns a random unit vector
	public Vector3 randVector()
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
