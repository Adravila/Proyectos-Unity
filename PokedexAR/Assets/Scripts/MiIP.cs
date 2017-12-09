using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiIP : MonoBehaviour {

	string myIP;
	public Text texto;

	// Use this for initialization
	void Start () {
		string url = "http://checkip.dyndns.org";
		WWW www = new WWW(url);
		StartCoroutine(WaitForRequest(www));
	}

	// Corutina para tomar los datos
	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;

		// check for errors
		if (www.error == null)
		{
			Debug.Log("WWW Ok!: " + www.text);
			GetIP (www.text);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}

	// Transformaci�n de los datos
	void GetIP(string HTMLResponse)
	{
		myIP = HTMLResponse.Substring(HTMLResponse.IndexOf(":")+2);
		myIP = myIP.Substring(0,myIP.IndexOf("<"));
		texto.text = myIP;
	}


	// Update is called once per frame
	void Update () {

	}
}