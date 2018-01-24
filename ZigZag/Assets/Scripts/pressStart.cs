using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pressStart : MonoBehaviour {

	int mostrar;
	bool count;
	Button boton;
	Image myImage;
	public Sprite imageUno;
	public Sprite imageDos;
	public Sprite imageTres;
	private float myDealy = 1;

	// Use this for initialization
	void Start () {
		count = false;
		boton = GetComponent<Button> ();
		myImage = GetComponent<Image> ();
		boton.onClick.AddListener (BotonPulsado);
		mostrar = 3;
	}
	
	// Update is called once per frame
	void Update () {}

	void BotonPulsado() { count = true; }

	//Fixedupdate me permite que se refresque de una manera homogénea en la línea del tiempo
	void FixedUpdate (){
		if (count) {
			switch (mostrar) {
			case 0:
				SceneManager.LoadScene (1);
				break;
			case 1:
				myImage.sprite = imageUno;
				break;
			case 2:
				myImage.sprite = imageDos;
				break;
			case 3:
				this.transform.localScale += new Vector3 (-1.5f, 0,0);
				myImage.sprite = imageTres;
				break;
			}
			//Debug.Log ("antes de llamar esperar");
			//LLamamos al proceso de espera
			StartCoroutine (Esperar ());
			//Debug.Log ("Después de llamar esperar");
			mostrar--;
			count = false;
		}
	}

	//Usado para conseguir que haya un tiempo de espera
	IEnumerator Esperar(){
		yield return new WaitForSeconds (myDealy);
		count = true;
	}

}
