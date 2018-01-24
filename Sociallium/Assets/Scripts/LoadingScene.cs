using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {

	public GameObject loadingScreenObj;
	public Slider slider;
	public Canvas canvas;
	public GameObject load;
	AsyncOperation async;

	public void LoadScene(int scn){
		StartCoroutine (LoadingScreen (scn));
		canvas.enabled = false;
		load.SetActive (true);
	}

	IEnumerator LoadingScreen(int scn){

		AsyncOperation async = Application.LoadLevelAdditiveAsync(2);
		yield return async;
		Debug.Log("Loading complete");

		loadingScreenObj.SetActive (true);
	
		async = SceneManager.LoadSceneAsync (scn);
		async.allowSceneActivation = false;
		Debug.Log (async.isDone);

		while (async.isDone == false) {

			slider.value = async.progress;
			if (async.progress >= 0.9f) {
				slider.value = 1f;
				async.allowSceneActivation = true;
			}
			yield return null;
		}
	}
}
