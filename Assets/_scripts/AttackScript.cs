using UnityEngine;
using System.Collections;

public class AttackScript : MonoBehaviour {
	public AsteroidScript[] asteroids;
	public float time;
	// Use this for initialization
	void Start () {
		asteroids = GetComponentsInChildren<AsteroidScript> ();
		AsteroidCounter.setCounter (asteroids.Length);
		StartCoroutine("AttackPlayer");
	}
	IEnumerator AttackPlayer()
	{
		if (AsteroidCounter.counter == 0)
			yield break;
		
		yield return new WaitForSeconds (time);
		asteroids = GetComponentsInChildren<AsteroidScript> ();
		int num = Random.Range (0, asteroids.Length);
		//int num = 0;
		if (asteroids.Length>0&&asteroids [num] != null) 
		{
			asteroids [num].Attack ();
		}
		Debug.Log ("Asteroid number " + num + " is attacking!!");
		StartCoroutine ("AttackPlayer");
	}
}
