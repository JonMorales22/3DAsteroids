using UnityEngine;
using System.Collections;

public class SmallAsteroidScript : AsteroidScript {

	//overwrites the parent class (AsteroidScript) explode method b/c we don't want to spawn more asteroids on death
	public override void Explode()
	{
		Instantiate (explosion, transform.position, Quaternion.identity);
		if(Random.Range(0,100)<=spawn1upChance)
			Instantiate (OneUp, transform.position, Quaternion.identity);
		AsteroidCounter.decrememt(1);
		Destroy (gameObject);
	}
}
