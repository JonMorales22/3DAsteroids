using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputScript : MonoBehaviour {
	public bool flag=false;
	public InputField inputField;

	public void EnterInfo()
	{
		flag = true;
	}

	public void Deactivate()
	{
		transform.parent.gameObject.SetActive (false);
	}
}
