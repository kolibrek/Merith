using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	public int damage;
	public float speed;
	public float time;

	//Controller2D controller;

	// Use this for initialization
	void Start () {
		Invoke ("Fade", time);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.GetComponent<HealthController>()) {
			coll.GetComponent<HealthController>().TakeDamage(damage);
		}
	}

	void Fade() {
		Destroy(this.gameObject);
	}
}
