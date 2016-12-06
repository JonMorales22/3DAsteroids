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
	private int startLives = 3;

	private float immunityTime = 3.0f;

	private ForceFieldScript forcefield;

	//Singelton stuff
	private static PlayerStats _instance;

	public static PlayerStats Instance
	{
		get {return _instance ?? (_instance = new GameObject("PlayerStats").AddComponent<PlayerStats>()); }
	}
	// Use this for initialization
	void Awake () {
		score = 0;
		lives = startLives;
		health = startHealth;
		DontDestroyOnLoad (gameObject);
		if (usesForceField)
			forcefield = ForceFieldScript.Instance;
	}

	public void IncrementScore(int num)
	{
		score += num;
		if (AsteroidCounter.counter == 0) {
			SceneManager.LoadScene ("GameOver");
		}
	}

	//very similar to the TakeDamage method in ForceFieldScript. Only difference is we check to see if forcefield is down or if its in an immunity state
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

	public void OneUp()
	{
		lives++;
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

	//if player has more lives, we reset health and score, then reload the scene
	//else we go to GameOver screen
	IEnumerator NewScene ()
	{
		yield return new WaitForSeconds(4.0f);
		if (lives > 0)
		{
			isDead = false;
			health = startHealth;
			score = 0;
			forcefield.Reset ();
			SceneManager.LoadScene (1);
		}
		else
			SceneManager.LoadScene ("GameOver");

	}

	void PlayerDie()
	{
		isDead = true;
		lives--;
		StartCoroutine ("NewScene");
	}

	public void Reset()
	{
		isDead = false;
		health = startHealth;
		score = 0;
		lives = startLives;
		forcefield.Reset ();
	}
}
