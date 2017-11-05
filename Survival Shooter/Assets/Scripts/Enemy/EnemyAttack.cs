using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;
	public float MODE_timeBetweenAttacks = 0;	// Añadido
	public int MODE_attackDamage = 0;	// Añadido

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;

    void Awake ()
    {
		player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }
		
    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }
		
    void Update ()
    {
        timer += Time.deltaTime;

		// Añadido para aumentar la dificultad
		if(timer >= timeBetweenAttacks+MODE_timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
			anim.SetBool ("AttackEnemy",true);
            Attack ();
        }
		else
			anim.SetBool ("AttackEnemy",false);

        if(playerHealth.currentHealth <= 0)
        {
			anim.SetBool ("AttackEnemy",false);
            anim.SetTrigger ("PlayerDead");
        }
    }


    void Attack ()
    {
        timer = 0f;

        if(playerHealth.currentHealth > 0)
        {
			playerHealth.TakeDamage (attackDamage + MODE_attackDamage); // Añadido para aumentar la dificultad
        }
    }
}
