using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class player : MonoBehaviour {
	//Para mover la cámara según se mueve la bola
	public Camera camara;
	public float force = 1;
	public int score = 0;
	private Vector3 offset;
	private Rigidbody rb;
	private bool changeDir;
	private Vector3 dir;
	private float timeLeft;

	//Trabajando con el prefab
	public float x = 0;
	public float z = 0;
	public GameObject prefab; //el cubo que voy  a duplicar
	public GameObject prefabMonedas;
	public Text TextScore;
	public Text TextTime;

	//Variable para el retraso
	private float myDealy = 0.5f;

	// Use this for initialization
	void Start () {
		//Obtenemos la posición inicial de la cámara
		offset = camara.transform.position;
		rb = GetComponent<Rigidbody> ();
		changeDir = true;
		dir = new Vector3 (0, 0, 0);
		timeLeft = 0;
	
		//Trabajar con el prefab
		//Empezamos a crear GameObject, el primero debemos desplazarlos
		GameObject elcubo = Instantiate(prefab, new Vector3(x, 0, z), Quaternion.identity) as GameObject;
		z = z + 3;
		elcubo.transform.position = new Vector3 (x, 0, z);
		//Creamos unos cuantos cuadros iniciales
		for (int i = 0; i <100 ; i++)
		{
			float ran = Random.Range (0f, 1f);
			if (ran < 0.5f) {
				x = x + 3;
			} else {
				z = z + 3;
			}
			elcubo = Instantiate (prefab, new Vector3 (x, 0, z), Quaternion.identity) as GameObject;

			ran = Random.Range (0f, 1f);
			if (ran < 0.5f) {
				GameObject moneda = Instantiate (prefabMonedas, new Vector3 (x, 1.05f, z), Quaternion.identity) as GameObject;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		TextScore.text = "Puntuación: "+score;

		timeLeft += Time.deltaTime;
		TextTime.text = "Tiempo: "+(int)timeLeft+" s";

		//Vamos moviendo la cámara
		camara.transform.position = this.transform.position + offset;
		//Según vaya pulsando el botón de espacio se mueve en una dirección
		if (Input.GetKeyUp(KeyCode.Space)) {
			rb.Sleep();
			if (changeDir) {
				dir = new Vector3(0,0,1);
				changeDir= false;
			} else {
				dir = new Vector3(1,0,0);
				changeDir = true;
			}
		}
	}

	//Movimiento continuo de la bola
	void FixedUpdate(){
		rb.MovePosition (transform.position + dir * Time.deltaTime * force);
		if (rb.position.y < -5) {
			SceneManager.LoadScene (2);
			PlayerPrefs.SetInt ("Player score", score);
		}
	}

	//Cada vez que salimos de un bloque creamos otros y dejamos caer este
	void OnCollisionExit(Collision col){
		Debug.Log ("Entrado en OnCollisionExit");
		if (col.transform.tag == "suelo"){
			Debug.Log ("si es suelo");
			StartCoroutine (Destruir (col));
			float ran = Random.Range (0f, 1f);
			if (ran < 0.5f) {
				x = x + 3.005f;
			} else {
				z = z + 3.005f;
			}
			GameObject elcubo = Instantiate (prefab, new Vector3 (x, 0, z), Quaternion.identity) as GameObject;
			ran = Random.Range (0f, 1f);
			if (ran < 0.5f) {
				GameObject moneda = Instantiate (prefabMonedas, new Vector3 (x, 1.05f, z), Quaternion.identity) as GameObject;
			}
		}
	} 

	void OnTriggerEnter(Collider obj){
		if (obj.gameObject.tag == "moneda") {
			score += 1;
			Destroy (obj.gameObject);
			Debug.Log (score);
		}	
	}

	//Caida con retraso.
	IEnumerator Destruir(Collision col){
		yield return new WaitForSeconds (myDealy);
		col.rigidbody.isKinematic = false;
	}
}
