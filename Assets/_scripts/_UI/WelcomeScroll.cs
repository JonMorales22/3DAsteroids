using UnityEngine;
using System.Collections;

public class WelcomeScroll : MonoBehaviour {
	public float speed;
	public int smoothing;
	public GameObject[] background;

	private float[] parallaxScales;
	private Renderer[] render;

	// Use this for initialization
	void Start () {
		//background = GetComponentsInChildren<GameObject> ();
		parallaxScales = new float[background.Length];
		render = new Renderer[background.Length];

		for (int i = 0;i<background.Length ; i++) {
			parallaxScales [i] = background [i].transform.localPosition.z * 1;
			render [i] = background [i].GetComponent<Renderer> ();
		}
	}

	// Update is called once per frame
	void LateUpdate () {
		int x = background.Length-1;
		for (int i = 0; i < background.Length; i++) {
			float xVal = (speed * parallaxScales [x]) / smoothing;
			//Debug.Log ("xVal[" + i + "] = " + xVal);
			//float yVal = ((playerRB.velocity.y/2) * parallaxScales [x]) / smoothing;
			Vector2 offset = new Vector2 (xVal,0);
			render[i].material.mainTextureOffset += offset;
			x--;
		}
		//Vector2 offset = new Vector2 ((playerRB.velocity.x/smoothing),0);
		//Debug.Log ("Offset= "+playerRB.velocity.x / smoothing);
		//GetComponent<Renderer> ().material.mainTextureOffset += offset;
	}
}
