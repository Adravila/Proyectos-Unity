using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;
using UnityEngine.XR;
// TTS 67212edbfdf6438096c9195abde0c932
// STT AIzaSyA95NlcuuHPBAnShe6nlzLugMqt3CM30_0
public class Menu : MonoBehaviour {

	public Button button_1;
	public Button button_2;
	public Button button_3;

	public Button button_update_STTkey;
	public Button button_update_TTSkey;
	public InputField input_STTkey;
	public InputField input_TTSkey;

	public GameObject section_rv;
	public GameObject section_options;
	public GameObject section_home;

	Animator anim;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		button_1.onClick.AddListener (app_1_OnClick);	
		button_2.onClick.AddListener (app_2_OnClick);	
		button_3.onClick.AddListener (app_3_OnClick);	
		button_update_STTkey.onClick.AddListener (update_STTKey_OnClick);
		button_update_TTSkey.onClick.AddListener (update_TTSkey_OnClick);
		StartCoroutine (LoadDevice("none"));
	}

	void Start(){
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		input_STTkey.text = PlayerPrefs.GetString ("STT");
		input_TTSkey.text = PlayerPrefs.GetString ("TTS");
	}

	IEnumerator LoadDevice(string newDevice){
		XRSettings.LoadDeviceByName (newDevice);
		yield return null;
		XRSettings.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// MENU
	void app_1_OnClick(){ 
		anim.SetTrigger("app1"); 
		section_home.SetActive (true); 
		section_rv.SetActive (false); 
		section_options.SetActive (false); 
	}

	void app_2_OnClick(){ 
		anim.SetTrigger("app2"); 
		section_home.SetActive (false); 
		section_rv.SetActive (true); 
		section_options.SetActive (false); 
	}

	void app_3_OnClick(){ 
		anim.SetTrigger("app3"); 
		section_home.SetActive (false); 
		section_rv.SetActive (false); 
		section_options.SetActive (true); 
	}

	// OPTIONS
	void update_STTKey_OnClick(){
		PlayerPrefs.SetString ("STT", input_STTkey.text);
	}

	void update_TTSkey_OnClick(){
		PlayerPrefs.SetString ("TTS", input_TTSkey.text);
	}
}