using UnityEngine;
using UnityEngine.UI;
using System.Collections;

#pragma warning disable 414

public class GameOverDisplay : MonoBehaviour {

	Image gameOverDisplay;
	Text[] gameOverText;

	// Use this for initialization
	void Start () {
		gameOverDisplay = GetComponent<Image>();
		gameOverText = GetComponentsInChildren<Text>();
		Display(false);
	}
	
	public void Display(bool setting) {
		gameOverDisplay.enabled = setting;
		foreach (Text text in gameOverText) {
			text.enabled = setting;
		}
	}
}
