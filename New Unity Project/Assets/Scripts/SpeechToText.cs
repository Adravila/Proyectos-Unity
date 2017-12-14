using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.Windows;
using UnityEngine.UI;
using System.Linq;

public class SpeechToText : MonoBehaviour {

	KeywordRecognizer keywordRecognizer;
	Dictionary<string,System.Action> keywords = new Dictionary<string, System.Action>();
	private DictationRecognizer m_DictationRecognizer;
	[SerializeField]

	void Start () {

		m_DictationRecognizer = new DictationRecognizer ();

		m_DictationRecognizer.DictationResult += (cadena, confidence) => {
			Debug.LogFormat ("Dictation result: {0}", cadena);
			GetComponent<ChatterBotAPITest> ().Iniciar(cadena);
			GetComponent<PlayerController>().movimientoPorVoz(cadena);
			cadena += cadena + "\n";
		};
		m_DictationRecognizer.Start ();
	}
}
