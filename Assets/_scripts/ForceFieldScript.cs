using UnityEngine;
using System.Collections;

public class ForceFieldScript : MonoBehaviour {
	public int rechargeAmount=1;

	public bool isImmune;
	public bool isDown;

	private int health;
	private int maxHealth=0;

	private float immunityTime = 3.0f;

	private static ForceFieldScript _instance;

	public static ForceFieldScript Instance
	{
		get {return _instance ?? (_instance = new GameObject("ForceField").AddComponent<ForceFieldScript>()); }
	}

	// Use this for initialization
	void Awake()
	{
		health = maxHealth;
		DontDestroyOnLoad (gameObject);
	}
	void Update()
	{
		if (health > 0)
		{
			isDown = false;
		}
	}

	public void StartRecharge()
	{
		StartCoroutine ("Recharge");
	}

	public void StopRecharge()
	{
		StopCoroutine ("Recharge");
	}

	public int getHealth()
	{
		return health;
	}
	 
	public void TakeDamage(int damage)
	{
		if (!isImmune)
		{
			Camera.main.GetComponent<CameraShake> ().StartShake ();
			GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().StartNotifyCrash ();
			isImmune = true;
			StopRecharge ();
			StartCoroutine("ApplyImmunity");
			health -= damage;
			Debug.Log ("Health:" + health);
			if (health <= 0)
			{
				ShieldDown ();
			}
		}
	}

	public void Recharge(int num)
	{
		health += num;
		if (health > maxHealth)
			health = maxHealth;
	}
	public void Reset()
	{
		health = maxHealth;
	}

	IEnumerator ApplyImmunity()
	{
		yield return new WaitForSeconds (immunityTime);
		isImmune = false;
		//StartRecharge();
	}


//	IEnumerator Recharge()
//	{
//		yield return new WaitForSeconds (wait);
//		while (health < maxHealth) 
//		{
//			health += rechargeAmount;
//			yield return new WaitForSeconds (rechargeWait);
//		}
//		if (health > maxHealth)
//			health = maxHealth;
//	}

	void ShieldDown()
	{
		health=0;
		isDown = true;
		//Debug.Log ("Shield is Down!!");
	}
}
