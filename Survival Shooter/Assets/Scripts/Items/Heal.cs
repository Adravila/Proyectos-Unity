using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour {

	Transform player;
	PlayerHealth playerHealth;
	public AudioClip healAudio;
	float secondsCount;

	private float targetScale = 0.1f;
	private float shrinkSpeed = 1.1f;

	// Use this for initialization
	void Start () {}

	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
	}
		
	void DestroyLife(){
		this.transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;

		if (transform.localScale.x < targetScale) {
			Destroy (this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		secondsCount += Time.deltaTime;
		if (secondsCount > 10)
			DestroyLife();
	}

	void OnTriggerEnter(Collider obj){
		if(obj.gameObject.CompareTag ("Player")){
			if (playerHealth.currentHealth < 100) {
				playerHealth.currentHealth = playerHealth.currentHealth + 10;
				playerHealth.healthSlider.value = playerHealth.currentHealth;
				if (playerHealth.currentHealth > 100) {
					playerHealth.currentHealth = 100;
					playerHealth.healthSlider.value = 100;
				}
				AudioSource.PlayClipAtPoint (healAudio,Camera.main.transform.position);
				secondsCount = 10;
			}
		}
	}
}
