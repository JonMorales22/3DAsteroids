using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayStats : MonoBehaviour {
	public Transform camera;
	public Text[] textArray;
	// Use this for initialization
	void Start () {
		//for (int i = 0; i < textArray.Length; i++)
			//textArray [i].text = i.ToString();
	}

	// Update is called once per frame
	void Update () {
		textArray [0].text = camera.transform.localRotation.eulerAngles.y.ToString();
		textArray [1].text = camera.transform.localRotation.eulerAngles.x.ToString();
	}
}
