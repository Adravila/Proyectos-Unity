using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;

    Text text;

    void Awake ()
    {
        text = GetComponent <Text> ();
		score = PlayerPrefs.GetInt ("Player score");
    }


    void Update ()
    {
        text.text = "Score: " + score;
    }

	void FixedUpdate(){
		PlayerPrefs.SetInt ("Player score", score);
	}
}
