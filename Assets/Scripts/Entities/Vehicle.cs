using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vehicle : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}


	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.GetComponent<Controller2D>()) {
			foreach (ContactPoint2D contact in coll.contacts) {
				if (contact.normal.y < -0.9f) {
					coll.gameObject.GetComponent<Controller2D>().status.riding = true;
					coll.transform.SetParent(this.transform);
					break;
				}
			}
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.GetComponent<Controller2D>()) {
			foreach (ContactPoint2D contact in coll.contacts) {
				if (contact.normal.y < -0.9f) {
					Disembark(coll.gameObject);
					break;
				}
			}
		}
	}

	public void Disembark(GameObject passenger) {
		passenger.GetComponent<Controller2D>().status.riding = false;
		passenger.transform.SetParent(null);
	}
}
