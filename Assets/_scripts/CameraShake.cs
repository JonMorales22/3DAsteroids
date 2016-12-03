using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	//FOR CAMERA STUFF

	//public float shakeAmount;
	//public float shakeRange;
	public float defaultShake;

	private float decrement=2;
	private float shake;

	public void StartShake(float shakeAmount)
	{
		StartCoroutine("Shake",shakeAmount);
	}
	public void StartShake()
	{
		StartCoroutine("Shake",defaultShake);
	}
	IEnumerator Shake(float shakeAmount)
	{
		Vector3 initialPos = transform.position;
		shake = 1;
		while (shake > 0.0f)
		{
			//Debug.Log (Mathf.PerlinNoise (shakeRange, 0));
			transform.position = initialPos+Random.insideUnitSphere*shakeAmount;
			shake -= Time.deltaTime*decrement;
			yield return new WaitForSeconds(.01f);
		}
		transform.position = initialPos;
	}
}
