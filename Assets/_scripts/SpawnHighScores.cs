using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpawnHighScores : MonoBehaviour {
	public Text copyText;
	private Text[] textArray = new Text[10];
	// Use this for initialization
	void Start () {
		for (int i = 0; i < textArray.Length; i++) {
			GameObject child;
			Vector3 pos = copyText.transform.position + new Vector3 (0,  0, 0);
			child = (GameObject)Instantiate (copyText, pos, Quaternion.identity);
			child.transform.parent = gameObject.transform;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
