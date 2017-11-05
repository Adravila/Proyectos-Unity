using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
	public PlayerHealth playerHealth;
	public GameObject[] item;
	public float spawnTime = 3f;

	public Transform[] spawnPoints;

	// Añadido
	EnemyAttack enemyAttack;
	EnemyHealth enemyHealth;
	private float indice;

	void Awake(){
		PlayerPrefs.SetInt ("Kills", 0); // Reinicia el número de asesinatos del jugador

		spawnTime = 3f;
	}

	void Start ()
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	void Spawn ()
	{
		if(playerHealth.currentHealth <= 0f)
		{
			return;
		}

		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		int i = Random.Range (0, 1);

		Vector3 pos_loot = spawnPoints[spawnPointIndex].position;
		pos_loot.y = 0.9f;

		Instantiate (item[i], pos_loot, spawnPoints[spawnPointIndex].rotation);
	}
}