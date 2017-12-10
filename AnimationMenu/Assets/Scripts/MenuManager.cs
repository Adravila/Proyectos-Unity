using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public Button StartButton, LoadButton, DifficultyButton, ExitButton,
				  ReturnButton, Button_Easy, Button_Normal, Button_Hard;
	private Animator anim;
	public Text DificultyText;
	void Start()
	{
		anim = GetComponent<Animator> ();
		// Menú principal
		Button btn_1 = StartButton.GetComponent<Button>();
		Button btn_2 = LoadButton.GetComponent<Button>();
		Button btn_3 = DifficultyButton.GetComponent<Button>();
		Button btn_4 = ExitButton.GetComponent<Button>();

		btn_1.onClick.AddListener(PlayOnClick);
		btn_2.onClick.AddListener(LoadOnClick);
		btn_3.onClick.AddListener(DifficultyOnClick);
		btn_4.onClick.AddListener(ExitOnClick);

		// Menú dificultad
		Button btn_5 = ReturnButton.GetComponent<Button>();
		Button btn_6 = Button_Easy.GetComponent<Button>();
		Button btn_7 = Button_Normal.GetComponent<Button>();
		Button btn_8 = Button_Hard.GetComponent<Button>();

		btn_5.onClick.AddListener(MainReturn);
		btn_6.onClick.AddListener(DifficultyButtonOnClick_1);
		btn_7.onClick.AddListener(DifficultyButtonOnClick_2);
		btn_8.onClick.AddListener(DifficultyButtonOnClick_3);
	}

	void PlayOnClick(){
		anim.SetTrigger("FadeOut");
	}
		
	void LoadOnClick() { 
		anim.SetTrigger("FadeOut"); 
	}		

	void DebugVolverAlMenu(){ SceneManager.LoadScene (0); }	

	void DifficultyOnClick(){ 
		anim.SetTrigger ("FadeMenuChange"); 
	}

	void ExitOnClick(){ 
		anim.SetTrigger ("FadeExit"); 
	}

	void MainReturn(){ 
		anim.SetTrigger ("FadeMainMenu"); 
	}

	void DifficultyButtonOnClick_1(){ 
		DificultyText.text = "Dificultad: Fácil"; 
		DifficultyButton.GetComponentInChildren<Text> ().text = "Dificultad: Fácil";

	}
	void DifficultyButtonOnClick_2(){ 
		DificultyText.text = "Dificultad: Normal"; 
		DifficultyButton.GetComponentInChildren<Text> ().text = "Dificultad: Normal";
	}
	void DifficultyButtonOnClick_3(){ 
		DificultyText.text = "Dificultad: Difícil"; 
		DifficultyButton.GetComponentInChildren<Text> ().text = "Dificultad: Difícil";
	}
}