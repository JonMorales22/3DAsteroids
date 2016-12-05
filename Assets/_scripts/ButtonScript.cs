using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class ButtonScript : MonoBehaviour {
	//public GameObject levelSelectUI;
	//public GameObject mainMenuUI;

	void Start(){

	}

//	public void loadMainMenuUI()
//	{
//		mainMenuUI.SetActive (true);
//		levelSelectUI.SetActive (false);
//	
//	}
//		
//	public void loadLevelSelectUI()
//	{
//		levelSelectUI.gameObject.SetActive (true);
//		mainMenuUI.SetActive (false);
//	}
//
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
