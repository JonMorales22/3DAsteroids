using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class ButtonScript : MonoBehaviour {

	public void PlayAgain()
	{
		PlayerStats.Instance.Reset ();
		LoadScene (1);
	}

	public void LoadScene(int i)
	{
		SceneManager.LoadScene (i);
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
