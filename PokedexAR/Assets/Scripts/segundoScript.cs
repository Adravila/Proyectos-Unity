using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class segundoScript : MonoBehaviour {
	string myIP;
	public Text texto;
	public Text estadisticas;
	DatosPokemon datosPokemon;
	public string ID;
	// Use this for initialization
	void Start () {
		string url = "https://pokeapi.co/api/v2/pokemon/"+ID.ToString();
		WWW www = new WWW(url);
		StartCoroutine(WaitForRequest(www));
	}

	[System.Serializable]
	public class DatosPokemon
	{
		/*
		public string ip;
		public string country_code;
		public string country_name;
		public string region_code;
		public string region_name;
		public string city;
		public string zip_code;
		public string time_zone;
		public float latitude;
		public float longitude;
		public int metro_code;
		*/
		public string name;
		public string weight;
		public string height;
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;

		// check for errors
		if (www.error == null)
		{
			datosPokemon = JsonUtility.FromJson<DatosPokemon>(www.text);
			Debug.Log(datosPokemon.name);
			texto.text = datosPokemon.name;
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}

	// Update is called once per frame
	void Update () {

	}


}