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
	
	// Update is called once per frame
	void Update () {
		//transform.Rotate(Vector3.forward * speed);
		//Quaternion rotate = Quaternion.Euler(0,0,speed);
		//transform.localRotation *= rotate;
		if (transform.localPosition.y > -1f) {
			transform.Translate(new Vector3(0, -speed, 0));
		}
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
