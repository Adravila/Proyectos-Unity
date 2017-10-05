using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moverBalon : MonoBehaviour {

	private Rigidbody rb;
	public float fuerza = 3;
	public Transform target;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		Vector3 vector = new Vector3 (h, 0.5f, v);
		rb.AddForce (vector * fuerza * Time.deltaTime);
	}

	void OnTriggerEnter(Collider obj){
		if (obj.gameObject.tag == "Meta_1") {
			Debug.Log ("choca");
			Application.LoadLevel(0);
		}
		if (obj.gameObject.tag == "Meta_2") {
			Debug.Log ("choca");
			Application.LoadLevel(2);
		}
		if (obj.gameObject.tag == "Meta_3") {
			Debug.Log ("chocado");
			Application.LoadLevel (3);
		}
	}

	void OnCollisionEnter(Collision obj){
		if (obj.gameObject.tag == "Pared") {
			transform.position = new Vector3(-9, 1, -9);
		}

		if (obj.gameObject.tag == "Plataforma") {
			Debug.Log ("choca");
			obj.gameObject.GetComponent<Renderer> ().material.color = Color.red;
		}

	}
}
