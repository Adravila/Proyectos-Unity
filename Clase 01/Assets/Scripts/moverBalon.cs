using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moverBalon : MonoBehaviour {

	private Rigidbody rb;
	public float fuerza = 10;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		float s = Input.GetAxis ("Jump");
		Vector3 vector = new Vector3 (h, 0.5f, v);
		Vector3 vector_s = new Vector3 (0, s, 0);
		rb.AddForce (vector * fuerza * Time.deltaTime);
		rb.AddForce (vector_s * 3000 * Time.deltaTime);
	}

	void OnTriggerEnter(Collider obj){
		// Escena 1
		if (obj.gameObject.tag == "Meta_1") 
			Application.LoadLevel(1);
		// Escena 2
		if (obj.gameObject.tag == "Meta_2") 
			Application.LoadLevel(2);
		// Escena 3
		if (obj.gameObject.tag == "Meta_3") 
			Application.LoadLevel(3);
	}

	void OnCollisionEnter(Collision obj){
		// Escena 2
		if (obj.gameObject.tag == "Pared") 
			transform.position = new Vector3(-9, 1, -9);
		// Escena 4
		if (obj.gameObject.tag == "Plataforma")
			obj.gameObject.GetComponent<Renderer> ().material.color = Color.red;
	}
}
