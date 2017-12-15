using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChatterBotAPITest : MonoBehaviour {

	string API_KEY_cleverbot = "CC5vc-SATNu_33qUuHl5IZrzT8g"; // CLEVERBOT API
	string API_KEY_speechcloud = "176317ef004433a4037a65a00ec77d96"; // SPEECHCLOUD API
	public string palabra;
	public Text text_output;
	public Text text_input;
	ChatBotInfo chatBotInfo;

	public Button play;
	public Button pause;

	/// <summary>
	public Text text_interaction_count;
	public Text text_input_other;
	public Text text_input_label;
	public Text text_predicted_input;
	public Text text_accuracy;	

	public void Iniciar(string cadena){
		string url = "https://www.cleverbot.com/getreply?key="+API_KEY_cleverbot.ToString()+"&input="+cadena;
		WWW www = new WWW(url);
		StartCoroutine(WaitForRequest(www));
	}

	[System.Serializable]
	public class ChatBotInfo{
		public string interaction_count;
		public string input;
		public string input_other;
		public string input_label;
		public string predicted_input;
		public string accuracy;
		public string output_label;
		public string output;
		public string conversation_id;
		public string errorline;
		public string database_version;
		public string software_version;
		public string time_taken;
		public string random_number;
		public string time_second;
		public string time_minute;
		public string time_hour;
		public string time_day_of_week;
		public string time_day;
		public string time_month;
		public string time_year;
		public string reaction;
		public string reaction_tone;
		public string emotion;
		public string emotion_tone;
		public string clever_accuracy;
		public string clever_output;
		public string clever_match;
		public string time_elapsed;
		public string filtered_input;
		public string filtered_input_other;
		public string reaction_degree;
		public string emotion_degree;
		public string reaction_values;
		public string emotion_values;
		public string callback;
		public string interaction_1;
		public string interaction_1_other;
		public string interaction_2;
		public string interaction_3;
		public string interaction_4;
		public string interaction_5;
		public string interaction_6;
		public string interaction_7;
		public string interaction_8;
		public string interaction_9;
		public string interaction_10;
		public string interaction_11;
		public string interaction_12;
		public string interaction_13;
		public string interaction_14;
		public string interaction_15;
		public string interaction_16;
		public string interaction_17;
		public string interaction_18;
		public string interaction_19;
		public string interaction_20;
		public string interaction_21;
		public string interaction_22;
		public string interaction_23;
		public string interaction_24;
		public string interaction_25;
		public string interaction_26;
		public string interaction_27;
		public string interaction_28;
		public string interaction_29;
		public string interaction_30;
		public string interaction_31;
		public string interaction_32;
		public string interaction_33;
		public string interaction_34;
		public string interaction_35;
		public string interaction_36;
		public string interaction_37;
		public string interaction_38;
		public string interaction_39;
		public string interaction_40;
		public string interaction_41;
		public string interaction_42;
		public string interaction_43;
		public string interaction_44;
		public string interaction_45;
		public string interaction_46;
		public string interaction_47;
		public string interaction_48;
		public string interaction_49;
		public string interaction_50;
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;

		// check for errors
		if (www.error == null)
		{
			chatBotInfo = JsonUtility.FromJson<ChatBotInfo>(www.text);
			Debug.Log(chatBotInfo.output);
			text_output.text = "Bot says: "+chatBotInfo.output;
			text_input.text = "Say: "+chatBotInfo.input;

		} else {
			Debug.Log("WWW Error: "+ www.error);
		}   

		pause.gameObject.SetActive (false);
		play.gameObject.SetActive (true);
		GetComponent<ChatterBotAPITest> ().enabled = false;
		GetComponent<SpeechToText> ().enabled = false;
	}

	// Update is called once per frame
	void Update () {

	}
}
