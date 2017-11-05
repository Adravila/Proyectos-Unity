using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
	public float spawnTime = 3f;

    public Transform[] spawnPoints;

	// Añadido
	EnemyAttack enemyAttack;
	EnemyHealth enemyHealth;
	private float indice;

	void Awake(){
		enemyAttack = enemy.GetComponent<EnemyAttack> ();
		enemyHealth = enemy.GetComponent<EnemyHealth> ();

		indice = (float)SceneManager.GetActiveScene ().buildIndex - 1;
		if (PlayerPrefs.GetString ("MODE") == "easy") {
			if (enemy.name == "ZomBunny" || enemy.name == "ZomBear")
				spawnTime = 3f;
			else if (enemy.name == "Hellephant")
				spawnTime = 10f;
			indice += 0.5f; // Incrementa +5% en estadísticas por nivel
		}
		if (PlayerPrefs.GetString ("MODE") == "normal") {
			if (enemy.name == "ZomBunny" || enemy.name == "ZomBear")
				spawnTime = 2.5f;
			else if (enemy.name == "Hellephant")
				spawnTime = 8f;
			indice += 1f; // Incrementa +10% en estadísticas por nivel
		}
		if (PlayerPrefs.GetString ("MODE") == "hard") {
			if (enemy.name == "ZomBunny" || enemy.name == "ZomBear")
				spawnTime = 2f;
			else if (enemy.name == "Hellephant")
				spawnTime = 6f;
			indice += 2f; // Incrementa +20% en estadísticas por nivel	
		}

		enemyAttack.MODE_attackDamage = (int)(enemyHealth.startingHealth * indice * 0.1f);
		enemyAttack.MODE_timeBetweenAttacks = enemyAttack.timeBetweenAttacks * indice * (-0.025f);
		enemyHealth.MODE_startingHealth = (int)(enemyHealth.startingHealth * indice * 0.2f);
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

        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
