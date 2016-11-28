using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float tiltAngle;
	public float smooth;
	public Transform target;

	public Camera cam;
	// Use this for initialization
	void Start () {
		//cam = GameObject.FindWithTag ("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 pos = Camera.main.WorldToViewportPoint (Input.mousePosition);

		//Vector3 pos = target.position - transform.position;
		//Quaternion rotation = Quaternion.LookRotation (pos);
		//Debug.Log (pos);
		//float x_input =mousePosition.x;
		//float y_input = mousePosition.y;
		//float z_input;

		//Transform target = new Transform(pos,0,1);

		//Quaternion target = Quaternion.Euler(pos.y*tiltAngle, pos.x*tiltAngle, 0);
		//transform.position = new Vector3(pos.x,pos.y,0);

	}
}
