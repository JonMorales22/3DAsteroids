using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public bool isImmune;
	public bool isRecharging;

	private int score;

	private int health;
	private int startHealth = 10;

	private float immunityTime = 3.0f;
	//private float waitTime = 5.0f;

	private static PlayerStats _instance;

	public static PlayerStats Instance
	{
		get {return _instance ?? (_instance = new GameObject("PlayerStats").AddComponent<PlayerStats>()); }
	}
		
	public void IncrementScore(int num)
	{
		score += num;
	}

	public void TakeDamage(int damage)
	{
		if (!ForceFieldScript.Instance.isDown)
		{
			ForceFieldScript.Instance.TakeDamage (damage);
			return;
		}
		if (!isImmune)
		{
			isImmune = true;
			//ForceFieldScript.Instance.StopRecharge ();
			StartCoroutine("ApplyImmunity");
			health -= damage;
			Debug.Log ("Health:" + health);
			if (health <= 0)
			{
				PlayerDie ();
			}
		}
	}

	public void Heal(int amount)
	{
		if (health + amount < startHealth)
			health += amount;
		else
			health = startHealth;
	}

	public int getHealth()
	{
		return health;
	}

	public int getScore()
	{
		return score;
	}

	IEnumerator ApplyImmunity()
	{
		yield return new WaitForSeconds (immunityTime);
		isImmune = false;
	}

	// Use this for initialization
	void Awake () {
		score = 0;
		health = startHealth;
	}

	void PlayerDie()
	{
		ForceFieldScript.Instance.StopRecharge ();
		Debug.Log ("Player is Dead!!");

	}
}
