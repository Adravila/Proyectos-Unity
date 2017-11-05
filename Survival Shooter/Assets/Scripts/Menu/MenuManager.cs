using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public Button PlayButton, DifficultyButton, ExitButton;
	static int op = 2;
	public Text score;

	void Start()
	{
		Button btn_1 = PlayButton.GetComponent<Button>();
		Button btn_2 = DifficultyButton.GetComponent<Button>();
		Button btn_3 = ExitButton.GetComponent<Button>();
		btn_1.onClick.AddListener(PlayOnClick);
		btn_2.onClick.AddListener(DifficultyOnClick);
		btn_3.onClick.AddListener(ExitOnClick);
		if (PlayerPrefs.GetInt ("Player score") > PlayerPrefs.GetInt ("Record score"))
			PlayerPrefs.SetInt ("Record score", PlayerPrefs.GetInt ("Player score"));
		PlayerPrefs.SetInt ("Player score", 0);
		PlayerPrefs.SetInt ("Life", 0);
		score.text = "Record score: " + PlayerPrefs.GetInt ("Record score");

	}

	void PlayOnClick()
	{
		SceneManager.LoadScene (1); // Carga la escena 1 (LEVEL 01)
	}

	void DifficultyOnClick()
	{
		switch (op) {
		case 0:
			PlayerPrefs.SetString ("MODE","easy");
			DifficultyButton.GetComponentInChildren<Text> ().text = "Difficulty: Easy";
			op = 1;
			break;
		case 1:
			DifficultyButton.GetComponentInChildren<Text> ().text = "Difficulty: Normal"; 
			PlayerPrefs.SetString ("MODE","normal");
			op = 2;
			break;
		case 2:
			DifficultyButton.GetComponentInChildren<Text> ().text = "Difficulty: Hard";
			PlayerPrefs.SetString ("MODE","hard");
			op = 0;
			break;
		}
	}

	void ExitOnClick()
	{
		Application.Quit();
	}
}
