using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {

	public bool isImmune;
	public bool usesForceField=true;
	public bool isDead=false;

	private int score;
	private int lives;
	private int health;
	private int startHealth = 10;

	private float immunityTime = 3.0f;

	private ForceFieldScript forcefield;
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
		if (usesForceField&&forcefield.isDown == false && forcefield.isImmune == false)
			forcefield.TakeDamage (damage);
		else if (usesForceField&&forcefield.isImmune)
			return;
		else if (!this.isImmune)
			{
				this.isImmune = true;
				StartCoroutine("ApplyImmunity");
				this.health -= damage;
				if (this.health <= 0)
				{
					this.PlayerDie ();
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

	public int getLives()
	{
		return lives;
	}


	IEnumerator ApplyImmunity()
	{
		yield return new WaitForSeconds (immunityTime);
		isImmune = false;
	}

	IEnumerator NewScene ()
	{
		yield return new WaitForSeconds(4.0f);
		if (lives > 0)
		{
			isDead = false;
			health = startHealth;
			score = 0;
			forcefield.Reset ();
			SceneManager.LoadScene (0);
		}
		else
			SceneManager.LoadScene ("GameOver");

	}
	// Use this for initialization
	void Awake () {
		score = 0;
		lives = 3;
		health = startHealth;
		DontDestroyOnLoad (gameObject);
		if (usesForceField)
			forcefield = ForceFieldScript.Instance;
	}

	void PlayerDie()
	{
		//Destroy (forcefield.gameObject);
		isDead = true;
		lives--;
		StartCoroutine ("NewScene");
		//Debug.Log ("Player is Dead!!");

	}
}
