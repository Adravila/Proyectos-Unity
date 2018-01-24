using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class obtenerScore : MonoBehaviour {

	public Text textScore;

	// Use this for initialization
	void Start () {
		textScore.text = "Puntuación: "+PlayerPrefs.GetInt ("Player score").ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
