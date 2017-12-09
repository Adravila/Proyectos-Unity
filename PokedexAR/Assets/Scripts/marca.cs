using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class marca : MonoBehaviour {
	string minutosJugados;
	public Text texto;

	// Use this for initialization
	void Start () {
		string url = "http://www.marca.com/tag/Gareth_Bale/estadisticas/";
		WWW www = new WWW(url);
		StartCoroutine(WaitForRequest(www));


	}
	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;

		// check for errors
		if (www.error == null)
		{
			Debug.Log("WWW Ok MARCA !: " + www.text);
			GetDatos (www.text);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}

	void GetDatos(string HTMLResponse)
	{
		string minutosJugados = HTMLResponse.Substring (HTMLResponse.IndexOf ("<td>Minutos jugados</td>"), 75);
		Debug.Log ("minutos html >>> " + minutosJugados);
		texto.text = "Minutos Jugados: " + RemoveSpecialCharacters (minutosJugados);
	}

	public static string RemoveSpecialCharacters(string str) {
		StringBuilder sb = new StringBuilder();

		foreach (char c in str) {
			if ((c >= '0' && c <= '9')) {
				sb.Append(c);
			}
		}
		return sb.ToString();
	}
	// Update is called once per frame
	void Update () {

	}
}