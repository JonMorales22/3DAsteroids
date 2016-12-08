using UnityEngine;
using System.Collections;

public class ForceFieldScript : MonoBehaviour {
	public int rechargeAmount=1;

	public bool isImmune;
	public bool isDown;

	private int health;
	private int maxHealth=20;

	private float immunityTime = 3.0f;

	//Singleton stuff
	private static ForceFieldScript _instance;

	public static ForceFieldScript Instance
	{
		get {return _instance ?? (_instance = new GameObject("ForceField").AddComponent<ForceFieldScript>()); }
	}
		
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

	//get method
	public int getHealth()
	{
		return health;
	}
	 
	//Applies damage. After applying damage it calls a coroutine to give player immunity during which the player can't take damage.
	public void TakeDamage(int damage)
	{
		if (!isImmune)
		{
			Camera.main.GetComponent<CameraShake> ().StartShake ();
			GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().StartNotifyCrash ();
			isImmune = true;
			//StopRecharge ();
			StartCoroutine("ApplyImmunity");
			health -= damage;
			Debug.Log ("Health:" + health);
			if (health <= 0)
			{
				ShieldDown ();
			}
		}
	}

	//recharges the forcefield by specified amount
	public void Recharge(int num)
	{
		health += num;
		if (health > maxHealth)
			health = maxHealth;
	}

	//resets the forcefield to max
	public void Reset()
	{
		health = maxHealth;
	}

	IEnumerator ApplyImmunity()
	{
		yield return new WaitForSeconds (immunityTime);
		isImmune = false;
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
	}
}
