using UnityEngine;
using System.Collections;

public class HighScores : MonoBehaviour {
	private const int MAX_SCORES = 10;

	private int index=0;
	private int[] scoreArray = new int[MAX_SCORES];
	private string[] nameArray = new string[MAX_SCORES];


	// Use this for initialization
	void Start () {
		retrieveInfo ();
		if (PlayerStats.Instance.getScore () > 0)
			if (index < MAX_SCORES - 1 || compareScores ())
				Debug.Log ("Get input");
		Debug.Log ("Print Scores");
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
				index = i;
				break;
			}
		}

	}

	bool compareScores()
	{
		return true;
	}
}
