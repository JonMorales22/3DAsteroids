using UnityEngine;
using System.Collections;

public class MissleController : MonoBehaviour {
	
	public GameObject explosion;

	private Rigidbody rb;
	private Transform playerT;
	private GameObject player;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, 5.0f);
		playerT= GameObject.FindWithTag ("Player").GetComponent<Transform>();
		player = GameObject.FindWithTag ("Player");
		rb = GetComponent<Rigidbody> ();
	}

	void Update()
	{
		transform.rotation = Quaternion.LookRotation(rb.velocity);
	}

	void OnDestroy()
	{
		Explode ();

	}
	void Explode()
	{
		GameObject gb = (GameObject)Instantiate (explosion, transform.position, Quaternion.identity);
		gb.transform.LookAt (playerT);
		float distance= getDistance ();
		Debug.Log (distance);
		if(distance<5.0f)
		{
			PlayerController ps;
			ps = player.GetComponent<PlayerController>();
			ps.StartCameraShake ();
		}
		Destroy (gameObject);
	}

	float getDistance()
	{
		float xVal,yVal,zVal;

		xVal = Mathf.Sqrt ((transform.position.x - playerT.position.x) * (transform.position.x - playerT.position.x));
		yVal = Mathf.Sqrt ((transform.position.y - playerT.position.y) * (transform.position.y - playerT.position.y));
		zVal = Mathf.Sqrt ((transform.position.z - playerT.position.z) * (transform.position.z - playerT.position.z));

		return Mathf.Sqrt (xVal + yVal + zVal);
	}
		
}
