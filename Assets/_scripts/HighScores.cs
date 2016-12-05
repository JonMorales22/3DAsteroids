using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScores : MonoBehaviour {

	public InputScript input;
	public GameObject panel;

	public Text[] highScoresHolder;

	private const int MAX_SCORES = 10;

	private int numPlayers=0;
	private int index;
	private int score;
	private int[] scoreArray = new int[MAX_SCORES];
	private string[] nameArray = new string[MAX_SCORES];
	private string playerName;
	// Use this for initialization
	void Start () {
		score = PlayerStats.Instance.getScore ();
		retrieveInfo ();
		if (score > 0) {
			if (numPlayers < MAX_SCORES - 1) {
				StartCoroutine ("getInput");
				return;
			}
			else if (compareScores ()) {
				StartCoroutine ("getInput2",index);
				return;
			}
		}
		panel.SetActive (true);
		highScoresHolder = GameObject.FindWithTag ("HighScores").GetComponentsInChildren<Text>();
		input.Deactivate ();
		printArray ();
	}

	void retrieveInfo()
	{
		for (int i = 0; i < MAX_SCORES; i++)
		{
			if (PlayerPrefs.HasKey ("SCORE_Player" + i) && PlayerPrefs.HasKey ("NAME_Player" + i)) {
				scoreArray [i] = PlayerPrefs.GetInt ("SCORE_Player" + i);
				nameArray [i] = PlayerPrefs.GetString ("NAME_Player" + i);
			}
			else
			{
				numPlayers = i;
				return;
			}
		}
		numPlayers = MAX_SCORES;

	}

	bool compareScores()
	{
		for (int i = 0; i < MAX_SCORES; i++)
		{
			if (scoreArray [i] < score) {
				index = i;
				return true;
			}
		}
		return false;
	}

	IEnumerator getInput()
	{
		yield return new WaitUntil (() => input.flag==true);
		playerName  = input.inputField.text;
		input.Deactivate ();
		scoreArray [numPlayers] = PlayerStats.Instance.getScore ();
		nameArray [numPlayers] = playerName;
		sortArrays ();
		//Debug.Log ("Got info");
		//Debug.Log ("Print scores");
	}

	IEnumerator getInput2(int num)
	{
		yield return new WaitUntil (() => input.flag==true);
		playerName  = input.inputField.text;
		input.Deactivate ();
		scoreArray [numPlayers-1] = PlayerStats.Instance.getScore ();
		nameArray [numPlayers-1] = playerName;
		sortArrays ();
		//Debug.Log ("Got info");
		//Debug.Log ("Print scores");
	}
		
	int [] shiftScoreArray(int num) {
		int[] newArray = new int[MAX_SCORES];
		for (int i = 0; i < MAX_SCORES; i++) {
			if (i < numPlayers) {
				newArray [i] = scoreArray [i];
			} else if (i == numPlayers) {
				newArray [i] = score;
			} else { //i>numPlayers
				newArray [i] = scoreArray [i-1];
			}
		}
		return newArray;
	}

	string[] shiftNameArray(int numPlayers){
		string[] newArrayS = new string[MAX_SCORES];
		for (int i = 0; i < MAX_SCORES; i++) {
			if (i < numPlayers) {
				newArrayS [i] = nameArray [i];
			} else if (i == numPlayers) {
				newArrayS [i] = playerName;
			} else {
				newArrayS [i] = nameArray [i-1];
			}
		}
		return newArrayS;


	}

	void sortArrays()
	{
		for (int i = 0; i < MAX_SCORES; i++) {
			for (int x = 0; x < MAX_SCORES; x++) {
				if (scoreArray [x] < scoreArray [i]) {
					int temp = scoreArray [x];
					string tempS = nameArray [x];
					scoreArray [x] = scoreArray [i];
					nameArray [x] = nameArray [i];
					scoreArray[i] = temp;
					nameArray [i] = tempS;				
				}

			}
		}
		saveArray (numPlayers);

	}

	void saveArray(int numPlayers) {
		for (int i = 0; i < MAX_SCORES; i++) {
			if (nameArray [i] != null) {
				PlayerPrefs.SetString ("NAME_Player" + i, nameArray [i]);
				PlayerPrefs.SetInt ("SCORE_Player" + i, scoreArray [i]);
			}
		}
		printArray ();
		//displayText ();
	}
	void printArray()
	{
		panel.SetActive (true);
		highScoresHolder = GameObject.FindWithTag ("HighScores").GetComponentsInChildren<Text>();
		for (int i = 0; i < MAX_SCORES; i++) {
			if (nameArray [i] != null) {
				highScoresHolder [i].text = "Name: " + nameArray [i] + "\tScore: " + scoreArray [i].ToString();
			}
		}
	}
	public void DeleteHighScores()
	{
		GameObject gb = GameObject.FindWithTag ("HighScores");
		Destroy (gb);
		PlayerPrefs.DeleteAll ();
	}
}
