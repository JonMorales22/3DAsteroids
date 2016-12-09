using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayHomingStats : MonoBehaviour {
	public PlayerController script;
	public GameObject gb;
	public Text text;
	// Update is called once per frame
	void Update () {
		text.text = script.ammo.ToString();
		if (script.targetAcquired) {
			gb.SetActive (true);
		} else
			gb.SetActive (false);

	}
}
