using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectiveEntity : MonoBehaviour {

	Controller2D player;
	Image gameOverDisplay;
	Text gameOverText;

	void Start () {
		player = GameObject.Find("Player").GetComponent<Controller2D>();
		gameOverDisplay = GameObject.Find("GameOverDisplay").GetComponent<Image>();
		gameOverText = gameOverDisplay.gameObject.GetComponentInChildren<Text>();
	}

	public void GetObjective() {
		player.gameObject.SetActive(false);
		gameOverDisplay.gameObject.SetActive(true);
		gameOverText.text = "Level Cleared!";
	}
}
