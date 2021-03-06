﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class HealthController : MonoBehaviour {

	public GameOverDisplay gameOverDisplay;

	public int maxHealth;
	int health;
	
	Controller2D controller;
	Renderer render;

	Color invulnerable;
	Color normal;

	// Use this for initialization
	void Start () {
		controller = GetComponent<Controller2D>();
		render = GetComponent<Renderer>();
		gameOverDisplay = GameObject.Find("GameOverDisplay").GetComponent<GameOverDisplay>();

		invulnerable = new Color(0.8f, 0.3f, 0.3f, 1f);
		normal = new Color(1f,1f,1f,1f);

		health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void TakeDamage(int amount) {
		health -= amount;

		if (health <= 0) {
			controller.status.dying = true;
			if (gameObject.tag == "Player") {
				Invoke("Die", 2.0f);
			} else {
				Die ();
			}
			return;
		}
		if (health > maxHealth) {
			health = maxHealth;
		}
		StartCoroutine("Flash");
		controller.status.invulnerable = true;
		controller.status.takingDamage = false;
		controller.status.damageTaken = 0;
		Invoke("ResetInvulnerable", 1.5f);
	}

	void Die() {
		if (gameObject.tag == "Player") {
			gameOverDisplay.Display(true);
		}
		GameObject.Find ("Player").GetComponent<Controller2D> ().status.colliders.Remove (gameObject);
		gameObject.SetActive(false);
	}

	public int GetHealth() {
		return health;
	}

	IEnumerator Flash() {
		for (int i = 0; i < 5; i++) {
			render.material.color = invulnerable;
			yield return new WaitForSeconds(0.15f);
			render.material.color = normal;
			yield return new WaitForSeconds(0.15f);
		}
	}

	void ResetInvulnerable() {
		controller.status.ResetInvulnerable();
	}
}
