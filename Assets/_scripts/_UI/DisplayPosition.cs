using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayPosition : MonoBehaviour {
	public Text[] textArray;
	private Transform player;
	private IEnumerator coroutine;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player").GetComponent<Transform> ();
		StartCoroutine ("Display");
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.deltaTime > .5f) {

		}
	}

	IEnumerator Display()
	{
		textArray [0].text = player.rotation.eulerAngles.x.ToString ("F2");
		textArray [1].text = player.rotation.eulerAngles.y.ToString ("F2");
		textArray [2].text = player.rotation.eulerAngles.z.ToString ("F2");
		textArray [3].text = player.position.ToString ();
		textArray [4].text = AsteroidCounter.counter.ToString ();
		yield return new WaitForSeconds (.1f);
		StartCoroutine ("Display");
	}
}
