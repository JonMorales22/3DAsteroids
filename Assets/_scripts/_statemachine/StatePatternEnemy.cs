using UnityEngine;
using System.Collections;

public class StatePatternEnemy : MonoBehaviour {

	public GameObject laser;
	public GameObject explosion;
	public GameObject OneUp;

	public Transform spawn;

	public float evadeDistance;
	public float attackDistance;
	public float chaseDistance;
	public float laserSpeed;
	public float maxSpeed;
	public float attackTime;

	//[HideInInspector] 
	public float distance;
	[HideInInspector] public Transform chaseTarget;
	[HideInInspector] public IEnemyState currentState;
	[HideInInspector] public AttackState attackState;
	[HideInInspector] public ChaseState chaseState;
	[HideInInspector] public EvadeState evadeState;
	[HideInInspector] public OrbitState orbitState;

	private bool isAttacking;
	private Rigidbody rb;

	private Transform playerT;



	// Use this for initialization
	void Awake () {
		attackState = new AttackState (this);
		chaseState = new ChaseState (this);
		evadeState = new EvadeState (this);
		orbitState = new OrbitState (this);
	}

	void Start()
	{
		//spawn = GetComponentInChildren<Transform> ();
		playerT = GameObject.FindWithTag ("Player").GetComponent<Transform>();
		rb = GetComponent<Rigidbody> ();
		currentState = orbitState;
	}
	void FixedUpdate()
	{
		transform.LookAt(playerT.position+new Vector3(0,-10,0));
		//spawn.transform.LookAt(playerT.position);

		//FOR DEBUG ONLY
//		if (Input.GetKeyDown (KeyCode.Space))
//			Fire ();
		
		currentState.UpdateState ();
		CalcPlayerDistance ();
	}

	public void CalcPlayerDistance()
	{
		float xVal,yVal,zVal;

		xVal = Mathf.Sqrt ((transform.position.x - playerT.position.x) * (transform.position.x - playerT.position.x));
		yVal = Mathf.Sqrt ((transform.position.y - playerT.position.y) * (transform.position.y - playerT.position.y));
		zVal = Mathf.Sqrt ((transform.position.z - playerT.position.z) * (transform.position.z - playerT.position.z));

		distance = Mathf.Sqrt (xVal + yVal + zVal);
	}
	public void OrbitPlayer()
	{
		transform.RotateAround (Vector3.zero, transform.forward, 15 * Time.deltaTime);
		StartAttackPlayer ();
	}
//================FOR MOVEMENT===============================================================//
	public void ChasePlayer()
	{	
		if (rb.velocity.x < maxSpeed) {
			rb.AddForce ((playerT.transform.position - transform.position) * maxSpeed);
		} else if (rb.velocity.x > maxSpeed)
			rb.velocity = ((playerT.transform.position - transform.position) * maxSpeed);
	}
	public void EvadePlayer()
	{	
		if (rb.velocity.x < maxSpeed) {
			rb.AddForce ((playerT.transform.position - transform.position) * -maxSpeed);
		} else if (rb.velocity.x > maxSpeed)
			rb.velocity = ((playerT.transform.position - transform.position) * -maxSpeed);
	}
//===========================================================================================//

//==========FOR ATTACKING PLAYER!!!!=========================================================//
	public void StartAttackPlayer()
	{
		if (!isAttacking)
		{
			StartCoroutine ("AttackPlayer");
		}
	}

	public void StopAttackPlayer()
	{
		if (isAttacking)
		{
			isAttacking = false;
			StopCoroutine ("AttackPlayer");
		}
	}

	private IEnumerator AttackPlayer()
	{
		isAttacking = true;
		yield return new WaitForSeconds (attackTime);
		Fire();
		isAttacking = false;
	}
	private void Fire()
	{
		Vector3 vec = Random.insideUnitSphere;
		//Vector3 vec = new Vector3(0,1,0);
		GameObject foo = (GameObject)Instantiate (laser, spawn.transform.position, Quaternion.identity);
		Rigidbody missleRB = foo.GetComponent<Rigidbody> ();
		missleRB.AddForce (((playerT.position-spawn.transform.position)+vec)*laserSpeed);//<----------DISABLED FOR DEBUGGING
		//missleRB.AddForce (((playerT.position-spawn.transform.position))*laserSpeed);//<--------USED FOR TESTING
	}

	private Vector3 randVectorRadius(int num, int num2)
	{
		int x = Random.Range (num, num2);
		int y = Random.Range (num, num2);
		int z = Random.Range (num, num2);
		Vector3 vec = new Vector3 (x,y,z);
		return vec;
	}
//========================================================================================================//
	void OnCollisionEnter(Collision c)
	{
		//First calls explode, then destroys the missle prefab
		if (c.gameObject.CompareTag ("Missle"))
		{
			PlayerStats.Instance.IncrementScore (300);
			ForceFieldScript.Instance.Reset();
			Explode ();
			Destroy (c.gameObject);
		}
	}
	void Explode()
	{
		Instantiate (explosion, transform.position, Quaternion.identity);
		Instantiate (OneUp, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}

}

