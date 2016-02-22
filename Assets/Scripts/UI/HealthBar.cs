using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public Controller2D controller;
	HealthController health;
	Text healthText;

	void Start() {
		health = controller.GetComponent<HealthController>();
		healthText = GameObject.Find("HealthText").GetComponent<Text>();
		healthText.text = health.GetHealth() + "/" + health.maxHealth;
	}

	void Update() {
		gameObject.transform.localScale = new Vector3((float)health.GetHealth() / (float)health.maxHealth, 1, 1);
		if (controller.status.dying) {
			healthText.gameObject.SetActive(false);
		}
	}

	void FixedUpdate() {
		healthText.text = health.GetHealth() + "/" + health.maxHealth;
	}
}
