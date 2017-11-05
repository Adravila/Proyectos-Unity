using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemQuest : MonoBehaviour {

	Transform player;
	public AudioClip healItem;
	float secondsCount;

	private float targetScale = 0.1f;
	private float shrinkSpeed = 1.1f;

	// Use this for initialization
	void Start () {}

	void Awake(){}

	void DestroyItem(){
		this.transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
		if (transform.localScale.x < targetScale) {
			Destroy (this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		secondsCount += Time.deltaTime;
		if (secondsCount > 10)
			DestroyItem();
	}

	void OnTriggerEnter(Collider obj){
		if(obj.gameObject.CompareTag ("Player")){
			obj.GetComponent<PlayerHealth> ().cont+= 5;
			AudioSource.PlayClipAtPoint (healItem,Camera.main.transform.position);
			secondsCount = 10;
		}
	}
}
