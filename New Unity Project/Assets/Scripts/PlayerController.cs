using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 0.5f;
	Vector3 mov;
	public GameObject player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
		//float y = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
		//mov = new Vector3 (x,0, 0);
		player.transform.Translate (mov);
	}

	public void movimientoPorVoz(string cadena){
		Debug.Log ("hola"+cadena);
		if (cadena == "izquierda")
			mov = new Vector3 (-3*Time.deltaTime*speed,0, 0);
		else if (cadena == "derecha")
			mov = new Vector3 (3*Time.deltaTime*speed,0, 0);
		else if (cadena == "frente")
			mov = new Vector3 (0,0,3*Time.deltaTime*speed);
		else if (cadena == "atrás")
			mov = new Vector3 (0,0,-3*Time.deltaTime*speed);
		else mov = new Vector3 (0,0,0);


			
	}
}
