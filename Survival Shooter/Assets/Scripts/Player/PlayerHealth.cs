using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
	public Slider questSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool damaged;
	bool isDead;

	// Añadido
	public int life;
	public int cont;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
		life = 0;
		life = PlayerPrefs.GetInt ("Life");
		cont = 0;
    }


    void Update ()
    {

		questSlider.value = cont;

		if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
	{
		if (life < 2) { // Tienes 3 vidas (de 0 a 2)
			life++;
			PlayerPrefs.SetInt ("Life", life);
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex); // Carga la escena actual (reinicia)
		}else {
			PlayerPrefs.SetInt ("Life", 0); // Prefijar las vidas a 3
			SceneManager.LoadScene (0); // Carga la escena 0 (pantalla de inicio)
		}
	}
}
