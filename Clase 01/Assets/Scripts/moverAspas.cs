using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moverAspas : MonoBehaviour {

	public float velocidad = 2f;
	Vector3 rotacion = new Vector3(24,0,0);

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate (-rotacion * velocidad * Time.deltaTime);
	}

	void OnCollisionEnter(Collision obj){
		if (obj.gameObject.tag == "Player") {
			Debug.Log ("choca");
			obj.transform.position = new Vector3(5.9722f,11.850f,-0.121f);
		}
	}
}

