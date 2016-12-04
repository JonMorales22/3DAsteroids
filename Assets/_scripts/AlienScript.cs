using UnityEngine;
using System.Collections;

public class AlienScript : MonoBehaviour {
	private Transform playerTrans;
	private AudioSource audiosource;

	public GameObject explosion;
	public GameObject OneUp;

	// Use this for initialization
	void Start () {
		audiosource = GetComponent<AudioSource> ();
		playerTrans = GameObject.FindWithTag ("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (Vector3.zero, transform.up, Time.deltaTime*10);
		transform.LookAt (playerTrans.position);
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
