using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public Button StartButton, LoadButton, DifficultyButton, ReturnButton;
	public Text score;
	private Animator anim;

	void Start()
	{
		anim = GetComponent<Animator> ();

		Button btn_1 = StartButton.GetComponent<Button>();
		Button btn_2 = LoadButton.GetComponent<Button>();
		Button btn_3 = DifficultyButton.GetComponent<Button>();

		Button btn_4 = ReturnButton.GetComponent<Button>();

		btn_1.onClick.AddListener(PlayOnClick);
		btn_2.onClick.AddListener(LoadOnClick);
		btn_3.onClick.AddListener(DifficultyOnClick);

		btn_4.onClick.AddListener(MainReturn);
	}

	void PlayOnClick()
	{
		anim.SetTrigger("FadeOut");
		//SceneManager.LoadScene (1); // Carga la escena 1 (LEVEL 01)
	}
		
	void LoadOnClick() { anim.SetTrigger("FadeOut"); }		
	void DifficultyOnClick(){ anim.SetTrigger ("FadeMenuChange"); }
	void MainReturn(){ anim.SetTrigger ("FadeMainMenu"); }
}