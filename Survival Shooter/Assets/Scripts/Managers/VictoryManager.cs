using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
	public PlayerHealth playerHealth;
	public float quest;
	public Slider questSlider;
	Animator anim;
	GameObject enemy;
	GameObject player;
	bool isPlayer;

	public AudioClip WinClip;
	AudioSource playerAudio;

	PlayerMovement playerMovement;
	PlayerShooting playerShooting;

	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player");
		anim = GetComponent<Animator>();
		questSlider.GetComponentInChildren<Slider> ().maxValue = quest;
		isPlayer = false;
		playerAudio = player.GetComponent <AudioSource> ();
		playerMovement = player.GetComponent<PlayerMovement> ();
		playerShooting = player.GetComponentInChildren <PlayerShooting> ();
	}
		
	void Update(){
		if (playerHealth.cont >= quest) {
			enemy = GameObject.FindGameObjectWithTag ("enemy");
			if (enemy != null) {
				enemy.tag = "Untagged";
				enemy.GetComponent<EnemyAttack> ().enabled = false;
				enemy.GetComponent<EnemyMovement> ().nav.enabled = false;
				enemy.GetComponent<EnemyMovement> ().enabled = false;
			}
			if (isPlayer == false) {
				isPlayer = true;
				GameObject.Find ("BackgroundMusic").GetComponent<AudioSource> ().Pause ();
				playerMovement.enabled = false;
				playerShooting.enabled = false;
				GetComponent<TimePlay> ().enabled = false;
				if ((SceneManager.GetActiveScene ().buildIndex + 1) == 3)
					anim.SetTrigger ("Credits");
				else
					anim.SetTrigger ("Win");
				playerAudio.clip = WinClip;
				playerAudio.Play ();
			}
		}
	}

	void NewLevel(){
		if((SceneManager.GetActiveScene ().buildIndex + 1) != 3)
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1); // Carga la escena 0 (pantalla de inicio)
		else
			SceneManager.LoadScene(0);
	}
}