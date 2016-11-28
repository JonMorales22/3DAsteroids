using UnityEngine;
using System.Collections;

public class CamerController : MonoBehaviour {
	public float speedH = 2.0f;
	public float speedV = 2.0f;

	public float min;
	public float max;

	private float yaw = 0.0f;
	private float pitch = 0.0f;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		yaw += speedH * Input.GetAxis("Mouse X");
		//yaw=0;
		pitch -= speedV * Input.GetAxis("Mouse Y");
		//pitch=0;
		Vector3 deltaVec = new Vector3 (pitch, yaw, 0.0f);
		deltaVec.x = Mathf.Clamp (deltaVec.x, min, max);
		transform.eulerAngles = deltaVec;

		//Debug.Log (pitch);
		/*
		Vector2 pos = Camera.main.ScreenToViewportPoint (Input.mousePosition);
		Vector2 deltaVec = (center - pos)*-1;

		float delta = ((pos.x - center.x) * (pos.x - center.x))+((pos.y - center.y) * (pos.y - center.y));
		float distance = Mathf.Sqrt (delta);

		if (distance < .01)
			distance = 0;

		float x = Input.GetAxis ("Mouse X");
		float y = Input.GetAxis ("Mouse Y");

		//Debug.Log ("x: " + x );
		//Debug.Log ("y: " + y );
		Debug.Log ("Distance: " + distance);
		Debug.Log("Pos: "+pos);
		Debug.Log("DeltaVec: "+deltaVec);
		if(Mathf.Abs(deltaVec.x)>0||Mathf.Abs(deltaVec.y)>0)
		{
			//if (distance < 1.0)
			transform.rotation = Quaternion.Euler (transform.rotation.Euler, 0, 0);
			//else if (distance >= 1.0)
				//transform.rotation = Quaternion.Euler (2, 2, 0);
		}
		//float rotationX = Input.GetAxis("Mouse X") * xSpeed;
		//transform.Rotate(0, rotationX, 0);
		//float rotationY = Input.GetAxis("Mouse Y") * ySpeed;

		//transform.Rotate(rotationY, 0, 0);
		//transform.LookAt(center);
		//Debug.Log(distance);
		//transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(pos.y*ySpeed,pos.x*xSpeed,0), 5.0f);
		//if(Input.GetKeyDown(KeyCode.LeftArrow))
		//	transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 5);

	*/
	}

	public float getYaw()
	{
		return yaw;
	}

	public float getPitch()
	{
		return pitch;
	}
}
