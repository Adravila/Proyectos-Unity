﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections;
using System.Text.RegularExpressions;
using System.Text;

namespace FrostweepGames.Plugins.GoogleCloud.SpeechRecognition
{
	public class GCSpeechScript_Test : MonoBehaviour
	{
		private GCSpeechRecognition _speechRecognition;

		private Button _startRecordButton,
		_stopRecordButton;

		private Image _speechRecognitionState;

		private Text _speechRecognitionResult;

		private Toggle _isRuntimeDetectionToggle;

		private Dropdown _languageDropdown;

		private InputField _contextPhrases;

		Respuesta respuesta;

		public string dialogFlowSession = "chat" + (new System.Random ()).Next();

		[System.Serializable]
		public class Fulfillment
		{
			public string speech;
		}

		[System.Serializable]
		public class Result
		{
			public Fulfillment fulfillment;
		}

		[System.Serializable]
		public class Respuesta
		{
			public Result result;
		}

		private void Start()
		{
			_speechRecognition = GCSpeechRecognition.Instance;
			_speechRecognition.apiKey = PlayerPrefs.GetString ("STT");
			_speechRecognition.RecognitionSuccessEvent += RecognitionSuccessEventHandler;
			_speechRecognition.NetworkRequestFailedEvent += SpeechRecognizedFailedEventHandler;
			_speechRecognition.LongRecognitionSuccessEvent += LongRecognitionSuccessEventHandler;

			_startRecordButton = transform.Find("Canvas/Button_StartRecord").GetComponent<Button>();
			_stopRecordButton = transform.Find("Canvas/Button_StopRecord").GetComponent<Button>();

			_speechRecognitionState = transform.Find("Canvas/Image_RecordState").GetComponent<Image>();

			_speechRecognitionResult = transform.Find("Canvas/Text_Result").GetComponent<Text>();

			_isRuntimeDetectionToggle = transform.Find("Canvas/Toggle_IsRuntime").GetComponent<Toggle>();

			_languageDropdown = transform.Find("Canvas/Dropdown_Language").GetComponent<Dropdown>();

			_contextPhrases = transform.Find("Canvas/InputField_SpeechContext").GetComponent<InputField>();

			_startRecordButton.onClick.AddListener(StartRecordButtonOnClickHandler);
			_stopRecordButton.onClick.AddListener(StopRecordButtonOnClickHandler);

			_speechRecognitionState.color = Color.white;
			_startRecordButton.interactable = true;
			_stopRecordButton.interactable = false;

			_languageDropdown.ClearOptions();

			for (int i = 0; i < Enum.GetNames(typeof(Enumerators.LanguageCode)).Length; i++)
			{
				_languageDropdown.options.Add(new Dropdown.OptionData(((Enumerators.LanguageCode)i).ToString()));
			}

			_languageDropdown.onValueChanged.AddListener(LanguageDropdownOnValueChanged);

			_languageDropdown.value = _languageDropdown.options.IndexOf(_languageDropdown.options.Find(x => x.text == Enumerators.LanguageCode.es_ES.ToString()));

		}

		private void OnDestroy()
		{
			_speechRecognition.RecognitionSuccessEvent -= RecognitionSuccessEventHandler;
			_speechRecognition.NetworkRequestFailedEvent -= SpeechRecognizedFailedEventHandler;
			_speechRecognition.LongRecognitionSuccessEvent -= LongRecognitionSuccessEventHandler;
		}


		private void StartRecordButtonOnClickHandler()
		{
			_startRecordButton.interactable = false;
			_stopRecordButton.interactable = true;
			_speechRecognitionState.color = Color.red;
			_speechRecognitionResult.text = string.Empty;
			_speechRecognition.StartRecord(_isRuntimeDetectionToggle.isOn);
		}

		private void StopRecordButtonOnClickHandler()
		{
			ApplySpeechContextPhrases();

			_stopRecordButton.interactable = false;
			_speechRecognitionState.color = Color.yellow;
			_speechRecognition.StopRecord();
		}

		private void LanguageDropdownOnValueChanged(int value)
		{
			_speechRecognition.SetLanguage((Enumerators.LanguageCode)value);
		}

		private void ApplySpeechContextPhrases()
		{
			string[] phrases = _contextPhrases.text.Trim().Split(","[0]);

			if (phrases.Length > 0)
				_speechRecognition.SetContext(new List<string[]>() { phrases });
		}

		private void SpeechRecognizedFailedEventHandler(string obj, long requestIndex)
		{		
			_speechRecognitionResult.text = "Speech Recognition failed with error: " + obj;

			if (!_isRuntimeDetectionToggle.isOn)
			{
				_speechRecognitionState.color = Color.green;
				_startRecordButton.interactable = true;
				_stopRecordButton.interactable = false;
			}
		}

		private void RecognitionSuccessEventHandler(RecognitionResponse obj, long requestIndex)
		{

			if (!_isRuntimeDetectionToggle.isOn)
			{
				_startRecordButton.interactable = true;
				_speechRecognitionState.color = Color.green;
			}

			if (obj != null && obj.results.Length > 0)
			{
				_speechRecognitionResult.text = "Speech Recognition succeeded! Detected Most useful: " + obj.results[0].alternatives[0].transcript;
			}
			else
			{
				_speechRecognitionResult.text = "Speech Recognition succeeded! Words are no detected.";
			}
		}

		IEnumerator DownloadTheAudio(string obj){
			Regex rgx = new Regex ("\\s+");
			string result = rgx.Replace (obj,"+");

			//string urlTextToSpeech = "https://translate.google.com/translate_tts?ie=UTF-8&tl=es-es&client=tw-ob&q=" + result;
			string urlTextToSpeech = "http://api.voicerss.org/?key=67212edbfdf6438096c9195abde0c932&hl=es-es&c=OGG&src="+result;
			WWW wwwTextToSpeech = new WWW (urlTextToSpeech);
			yield return wwwTextToSpeech;  
			AudioSource audio = GetComponent<AudioSource> ();
			audio.clip = wwwTextToSpeech.GetAudioClip(false, false, AudioType.OGGVORBIS);
			audio.Play ();
			Debug.Log((float)audio.time);
			_speechRecognitionResult.text = obj;
		}

		IEnumerator PostDialogflow(string url, string bodyJsonString)
		{
			var request = new UnityWebRequest(url, "POST");
			byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
			request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
			request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
			request.SetRequestHeader ("Content-Type", "application/json; charset=utf-8");
			request.SetRequestHeader ("Authorization", "Bearer 2600d1b6ea5544e49a5748e22d84cf53");

			yield return request.Send();
			if (request.isNetworkError || request.isHttpError)
			{
				_speechRecognitionResult.text = request.error;
			}
			else
			{
				respuesta = JsonUtility.FromJson<Respuesta>(request.downloadHandler.text);
				StartCoroutine (DownloadTheAudio(respuesta.result.fulfillment.speech));
			}
		}

		private void LongRecognitionSuccessEventHandler(OperationResponse operation, long index)
		{
			if(operation == null)
				_speechRecognitionResult.text = "ERROR NULL";
			if (!_isRuntimeDetectionToggle.isOn)
			{
				_startRecordButton.interactable = true;
				_speechRecognitionState.color = Color.green;
			}

			if (operation != null && operation.response.results != null)
			{
				Debug.Log (operation.response.results.Length);
				if ( operation.response.results.Length > 0){
					String pregunta = operation.response.results[0].alternatives[0].transcript;
					_speechRecognitionResult.text = "Long Speech Recognition succeeded! Detected Most useful: " + pregunta;
					_speechRecognitionResult.text += "\nTime for the recognition: " + 
						(operation.metadata.lastUpdateTime - operation.metadata.startTime).TotalSeconds + "s";

					string urlChatbotRequest = "https://console.dialogflow.com/api/query?v=20170712";
					string postData = "{\"q\":\"" + pregunta + "\",\"timezone\":\"Europe/Paris\",\"lang\":\"es\",\"sessionId\":\""+ dialogFlowSession +"\",\"resetContexts\":false}";

					StartCoroutine (PostDialogflow (urlChatbotRequest, postData)); //hace request POST a la API v1 de smalltalk
				}
				else _speechRecognitionResult.text = "Speech Recognition succeeded! Words are no detected.";
			}
			else
			{
				_speechRecognitionResult.text = "Speech Recognition succeeded! Words are no detected.";
			}
		}
	}
}