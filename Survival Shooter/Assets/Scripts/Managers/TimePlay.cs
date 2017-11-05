using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimePlay : MonoBehaviour {

	public Slider TimeSlider;

	private GameObject player;
	private GameObject other;
	PlayerMovement playerMovement;
	PlayerShooting playerShooting;
	bool isPlayer;
	private Animator anim;
	EnemyAttack enemyAttack;
	EnemyMovement enemyMovement;

	public AudioClip deathClip;
	AudioSource playerAudio;

	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player");
		playerMovement = player.GetComponent<PlayerMovement> ();
		playerShooting = player.GetComponentInChildren <PlayerShooting> ();
		anim = GetComponent<Animator> ();
		playerAudio = player.GetComponent <AudioSource> ();
		isPlayer = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () { 
		TimeSlider.value += Time.deltaTime;
		if (TimeSlider.value == TimeSlider.maxValue) { // Time lost :(
			other = GameObject.FindGameObjectWithTag ("enemy");
			if (other != null) {
				other.tag = "Untagged";
				other.GetComponent<EnemyAttack> ().enabled = false;
				other.GetComponent<EnemyMovement> ().nav.enabled = false;
				other.GetComponent<EnemyMovement> ().enabled = false;
			}
			if (isPlayer == false) {
				isPlayer = true;
				GameObject.Find ("BackgroundMusic").GetComponent<AudioSource>().Pause();
				playerAudio.clip = deathClip;
				playerAudio.Play ();
				playerMovement.enabled = false;
				playerShooting.enabled = false;
				if (PlayerPrefs.GetInt ("Life") >= 2)
					anim.SetTrigger("GameOver");
				else
					anim.SetTrigger("Dead");
			}
		}
	}

	public void Reset ()
	{
		PlayerPrefs.SetInt ("Life", PlayerPrefs.GetInt("Life")+1);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex); // Carga la escena actual (reinicia)
	}

	public void GameOver(){
		PlayerPrefs.SetInt ("Life", 0); // Prefijar las vidas a 3
		SceneManager.LoadScene (0); // Carga la escena 0 (pantalla de inicio)
	}
}
