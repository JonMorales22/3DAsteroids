using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayPosition : MonoBehaviour {
	public Text[] textArray;
	private Transform player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player").GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		textArray [0].text = player.rotation.eulerAngles.x.ToString();
		textArray [1].text = player.rotation.eulerAngles.y.ToString();
		textArray [2].text = player.rotation.eulerAngles.z.ToString();
		textArray [3].text = player.position.ToString();
		textArray [4].text = AsteroidCounter.counter.ToString ();
	}
}
