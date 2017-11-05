using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour {

	public GameObject[] loot;
	public float[] dropRatio; // Añadido para añadir el ratio del despojo de botín.

	void Awake(){

	}

	public void SoltarObjeto(){
		for (int i = 0; i<loot.Length; i++) {
			if (Random.Range (1, 100) < dropRatio [i]) { // Ratio de 0.05
				Vector3 pos_loot = transform.position;
				pos_loot.y = 0.9f;
				Instantiate (loot [i], pos_loot, this.transform.rotation);
			}
		}
	}
}
