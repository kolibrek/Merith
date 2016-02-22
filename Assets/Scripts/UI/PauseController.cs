using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseController : MonoBehaviour {

	bool isPaused;
	GameObject pauseDisplay;
	public Controller2D controller;

	// Use this for initialization
	void Start () {
		isPaused = false;
		pauseDisplay = GameObject.Find("PauseBG");
		pauseDisplay.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Pause")) {
			if (!controller.status.dying) {
				TogglePause();
			} else {
				Quit ();
			}
		}
		if (Input.GetKeyDown(KeyCode.Q) && isPaused) {
			Quit();
		}
	}

	public void TogglePause() {
		Time.timeScale = (isPaused)? 1 : 0;
		isPaused = !isPaused;
		pauseDisplay.SetActive(isPaused);
	}

	public void Quit() {
		#if UNITY_EDITOR 
		EditorApplication.isPlaying = false;
		#else 
		Application.Quit();
		#endif
	}
}
