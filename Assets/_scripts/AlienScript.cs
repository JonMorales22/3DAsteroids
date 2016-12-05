using UnityEngine;
using System.Collections;

public class AlienScript : MonoBehaviour {
	private Transform playerTrans;

	private AudioSource audiosource;

	public float speed = 500.0f;
	public GameObject explosion;
	public GameObject OneUp;
	public GameObject laser;

	public Transform spawn;
	// Use this for initialization
	void Start () {
		audiosource = GetComponent<AudioSource> ();
		playerTrans = GameObject.FindWithTag ("Player").GetComponent<Transform>();
		//StartCoroutine ("AttackPlayer");
	}
	
	// Update is called once per frame
	void Update () {
		//transform.RotateAround (Vector3.zero, transform.up, Time.deltaTime*10);
		transform.LookAt (playerTrans.position);
		spawn.transform.LookAt (playerTrans.position);
		if (Input.GetKeyDown (KeyCode.Space)) {
			Fire ();
		}
	}

	void OnCollisionEnter(Collision c)
	{
		//First calls explode, then destroys the missle prefab
		if (c.gameObject.CompareTag ("Missle"))
		{
			PlayerStats.Instance.IncrementScore (300);
			ForceFieldScript.Instance.Recharge (10);
			Explode ();
			Destroy (c.gameObject);
		}
		if (c.gameObject.CompareTag ("Player"))
		{
			if(!c.gameObject.GetComponent<PlayerController>().isDead)
				playSound ();
		}

	}

	IEnumerator AttackPlayer()
	{
		yield return new WaitForSeconds (5.0f);
		Fire();
		StartCoroutine ("AttackPlayer");
	}
	void Fire()
	{
		Vector3 vec = randVectorRadius (-5, 5);
		GameObject foo = (GameObject)Instantiate (laser, spawn.position, Quaternion.identity);
		Rigidbody missleRB = foo.GetComponent<Rigidbody> ();
		//missleRB.AddForce (((playerTrans.position-transform.position)+vec)*speed);//<----------DISABLED FOR DEBUGGING
		missleRB.AddForce (((playerTrans.position-transform.position))*speed);//<--------USED FOR TESTING
	}

	Vector3 randVectorRadius(int num, int num2)
	{
		int x = Random.Range (num, num2);
		int y = Random.Range (num, num2);
		int z = Random.Range (num, num2);
		Vector3 vec = new Vector3 (x,y,z);
		return vec;
	}
		
	void Explode()
	{
		Instantiate (explosion, transform.position, Quaternion.identity);
		Instantiate (OneUp, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}


	void playSound()
	{
		if (!audiosource.isPlaying)
			audiosource.Play();
	}
}
