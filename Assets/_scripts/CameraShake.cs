using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	public float defaultShake;
	public bool isShaking;

	private float decrement=2;
	private float shake;

	//shakes camera using the defualt shake
	public void StartShake()
	{
		if (!isShaking) {
			isShaking = true;
			StartCoroutine("Shake",defaultShake);
		}
			
	}

	//overload, shakes camera using an amount different than defualt shake
	public void StartShake(float shakeAmount)
	{	if (!isShaking)
		{
			isShaking = true;
			StartCoroutine ("Shake", shakeAmount);
		}
	}
		
	/* Shakes camera, takes in float shakeAmount as a parameter which is used to deteremine how much to shake the camera
	 * First we get the inital position of the camera then we offset its position by using a random value in a unit sphere
	 * Lastly we put the camera back in its inital position
	*/
	IEnumerator Shake(float shakeAmount)
	{
		Debug.Log ("Camera shake!");
		Vector3 initialPos = transform.localPosition;
		shake = 1;
		while (shake > 0.0f)
		{
			//Debug.Log (Mathf.PerlinNoise (shakeRange, 0));
			transform.localPosition = initialPos+Random.insideUnitSphere*shakeAmount;
			shake -= Time.deltaTime*decrement;
			yield return new WaitForSeconds(.01f);
		}
		transform.localPosition = initialPos;
		isShaking = false;
	}
}
