using UnityEngine;
using System.Collections;

public class StatePatternEnemy : MonoBehaviour {

	public Transform playerT;
	public Transform spawn;

	public GameObject laser;

	public float evadeDistance;
	public float attackDistance;
	public float chaseDistance;
	public float distance;
	public float laserSpeed;

	private IEnumerator attack;
	private bool isAttacking;

	[HideInInspector] public Transform chaseTarget;
	[HideInInspector] public IEnemyState currentState;
	[HideInInspector] public AttackState attackState;
	[HideInInspector] public ChaseState chaseState;
	[HideInInspector] public EvadeState evadeState;

	// Use this for initialization
	void Awake () {
		attackState = new AttackState (this);
		chaseState = new ChaseState (this);
		evadeState = new EvadeState (this);
	}

	void Start()
	{
		currentState = attackState;
	}
	void Update()
	{
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

	public void StartAttackPlayer()
	{
		if (!isAttacking)
		{
			StartCoroutine ("AttackPlayer");
		}
	}

	public void StopAttackPlayer()
	{
		StopCoroutine("AttackPlayer");
	}

	private IEnumerator AttackPlayer()
	{
		isAttacking = true;
		Fire();
		yield return new WaitForSeconds (5.0f);
		isAttacking = false;
	}
	private void Fire()
	{
		//Vector3 vec = randVectorRadius (-1, 1);
		Vector3 vec = new Vector3(0,1,0);
		GameObject foo = (GameObject)Instantiate (laser, spawn.position, Quaternion.identity);
		Rigidbody missleRB = foo.GetComponent<Rigidbody> ();
		missleRB.AddForce (((playerT.position-transform.position)+vec)*laserSpeed);//<----------DISABLED FOR DEBUGGING
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
}

