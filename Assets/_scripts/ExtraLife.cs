using UnityEngine;
using System.Collections;

public class ExtraLife : MonoBehaviour {
	private AudioSource audiosource;

	void Start()
	{
		audiosource = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject.CompareTag ("Player")) {
			audiosource.Play ();
			PlayerStats.Instance.OneUp ();
			StartCoroutine ("Delete");
		}
	}

	IEnumerator Delete()
	{
		yield return new WaitUntil(() => !audiosource.isPlaying);
		Destroy (gameObject);
	}

}
