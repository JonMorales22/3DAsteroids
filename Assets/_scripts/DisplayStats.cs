using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayStats : MonoBehaviour {
	PlayerStats playerstats;
	ForceFieldScript forcefield;

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
	}
}
