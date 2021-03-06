﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//static class used to hold number of asteroids
public static class AsteroidCounter
{
	public static int counter=0;

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
		if (counter <= 0) {
			SceneManager.LoadScene ("GameOver");
		}
	}
}

public class AttackScript : MonoBehaviour {
	private AsteroidScript[] asteroidsArray;

	//public GameObject asteroid;
	public float time;
	public int numAsteroids;
	public float spawnRadius;

	public float force=750.0f;
	public float torqueForce=250;

	public GameObject asteroid;

	private Vector3 dirVec;

	// Use this for initialization
	void Start () {
		//First we get all the asteroids in children, then we use static class AsteroidCounter to hold the number of asteroids
		//asteroids = GetComponentsInChildren<AsteroidScript> ();
		//AsteroidCounter.setCounter (numAsteroids);
		SpawnAsteroids ();
		//StartCoroutine("AttackPlayer");
	}

	//Spawns asteroids randomly around the map and applies a random force to them
	void SpawnAsteroids()
	{
		for (int i = 0; i < numAsteroids; i++)
		{
			GameObject child = (GameObject) Instantiate (asteroid, Random.insideUnitCircle * spawnRadius, Quaternion.identity, transform);
			Rigidbody rb = child.gameObject.GetComponent<Rigidbody> ();
			dirVec = randVector();
			rb.AddForce (dirVec * force);
			rb.AddTorque (dirVec*torqueForce);
		}
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
		asteroidsArray = GetComponentsInChildren<AsteroidScript> ();
		AsteroidCounter.setCounter (asteroidsArray.Length);
		int num = Random.Range (0, asteroidsArray.Length);
		if (asteroidsArray.Length>0&&asteroidsArray [num] != null) 
		{
			asteroidsArray [num].Attack ();
		}
		//Debug.Log ("Asteroid number " + num + " is attacking!!");
		StartCoroutine ("AttackPlayer");
	}

	//gets a random unit vector
	public Vector3 randVector()
	{
		int num = Random.Range (0, 6);
		Vector3 vec;
		switch (num)
		{
		case 1:
			vec = new Vector3 (0, 0, 1);
			break;
		case 2:
			vec = new Vector3 (0, 1, 0);
			break;
		case 3:
			vec = new Vector3 (1, 0, 0);
			break;
		case 4:
			vec = new Vector3 (0, 0, -1);
			break;
		case 5:
			vec = new Vector3 (0, -1, 0);
			break;
		default:
			vec = new Vector3 (-1, 0, 0);
			break;
		}
		return vec;
	}
}
