using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayStats : MonoBehaviour {
	PlayerStats playerstats;
	ForceFieldScript forcefield;
	/*
	public Transform camera; <-----USED FOR PLAYER POSITION
	*/
	public Text[] textArray;


	// Use this for initialization
	void Start () {
		playerstats = PlayerStats.Instance;
		forcefield = ForceFieldScript.Instance;
	}

	// Update is called once per frame
	void Update () {
		textArray [0].text = forcefield.getHealth().ToString();
		textArray [1].text = playerstats.getScore().ToString();
		textArray [2].text = playerstats.getLives ().ToString ();
		/*
		textArray [0].text = camera.transform.rotation.eulerAngles.x.ToString(); <----- USED FOR DISPLAY PLAYER POSITION
		textArray [1].text = camera.transform.rotation.eulerAngles.y.ToString();
		textArray [2].text = camera.transform.rotation.eulerAngles.z.ToString();
		textArray [3].text = camera.transform.position.ToString();
		textArray [4].text = AsteroidCounter.counter.ToString ();
		*/
	}
}
