using UnityEngine;
using System.Collections;

public class SmallAsteroidScript : AsteroidScript {

	public override void Explode()
	{

		//Instantiate (SmallAsteroid, transform.position+transform.right*-2, Quaternion.identity);
		//Instantiate (SmallAsteroid, transform.position+transform.right*2, Quaternion.identity);
		Instantiate (explosion, transform.position, Quaternion.identity);
		//rb.AddExplosionForce (1000, transform.position, 100);
		if(Random.Range(0,100)<=spawn1upChance)
			Instantiate (OneUp, transform.position, Quaternion.identity);
		AsteroidCounter.counter--;
		Destroy (gameObject);
	}
}
