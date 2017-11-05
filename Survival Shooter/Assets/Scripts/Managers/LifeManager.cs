using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour {

	public GameObject heal_1, heal_2, heal_3;
	public GameObject player;
	private PlayerHealth playerHealth;

	void Awake () {
		playerHealth = player.GetComponent<PlayerHealth> ();
	}
	
	void Update () {
		// Pierde una vida
		if (playerHealth.life == 1) {
			Destroy (heal_3);	
		} 
		// Pierde dos vidas;
		else if (playerHealth.life == 2) {
			Destroy (heal_3);
			Destroy (heal_2);
		}
	}
}
