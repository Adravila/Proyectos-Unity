using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;

    Animator anim;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
		if (playerHealth.currentHealth <= 0 && PlayerPrefs.GetInt ("Life") < 2)
        {
			GetComponent<TimePlay> ().enabled = false;
            anim.SetTrigger("Dead");
        }
		if (playerHealth.currentHealth <= 0 && PlayerPrefs.GetInt ("Life") >= 2)
		{
			GetComponent<TimePlay> ().enabled = false;
			anim.SetTrigger("GameOver");
		}
    }
}
