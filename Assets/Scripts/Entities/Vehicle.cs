using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vehicle : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.GetComponent<Controller2D>()) {
			foreach (ContactPoint2D contact in coll.contacts) {
				if (contact.normal.y < -0.9f) {
					Embark(coll.gameObject);
					break;
				}
			}
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.GetComponent<Controller2D>()) {
			Disembark(coll.gameObject);
		}
	}

	public void Embark(GameObject passenger) {
		passenger.gameObject.GetComponent<Controller2D>().status.riding = true;
		passenger.transform.SetParent(this.transform);
	}

	public void Disembark(GameObject passenger) {
		passenger.GetComponent<Controller2D>().status.riding = false;
		passenger.transform.SetParent(null);
	}
}
