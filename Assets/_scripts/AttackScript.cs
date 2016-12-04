using UnityEngine;
using System.Collections;

//static class used to hold number of asteroids
public static class AsteroidCounter
{
	public static int counter;

	public static void setCounter(int num)
	{
		counter = num;
	}
	public static void increment (int num)
	{
		counter += num;
	}
	public static void decrememt (int num)
	{
		counter -= num;
	}
}

public class AttackScript : MonoBehaviour {
	private AsteroidScript[] asteroids;
	public float time;


	// Use this for initialization
	void Start () {
		//First we get all the asteroids in children, then we use static class AsteroidCounter to hold the number of asteroids
		asteroids = GetComponentsInChildren<AsteroidScript> ();
		AsteroidCounter.setCounter (asteroids.Length);

		StartCoroutine("AttackPlayer");
	}

	//after a specified delay, choose a random asteroid and send it towards the player
	IEnumerator AttackPlayer()
	{
		//makes sure that all the asteroids haven't been destoryed
		if (AsteroidCounter.counter == 0)
			yield break;
		yield return new WaitForSeconds (time);

		//Since the player can destroy asteroids at any time,
		//we get the asteroids in children again to prevent using an asteroid that's been destroyed
		asteroids = GetComponentsInChildren<AsteroidScript> ();
		AsteroidCounter.setCounter (asteroids.Length);
		int num = Random.Range (0, asteroids.Length);
		if (asteroids.Length>0&&asteroids [num] != null) 
		{
			asteroids [num].Attack ();
		}
		//Debug.Log ("Asteroid number " + num + " is attacking!!");
		StartCoroutine ("AttackPlayer");
	}
}
