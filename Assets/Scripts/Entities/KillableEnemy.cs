using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class KillableEnemy : MonoBehaviour {

	Controller2D controller;

	// Use this for initialization
	void Start () {
		controller = GetComponent<Controller2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (controller.status.above) {
			foreach (GameObject collider in controller.status.colliders) {
				if (collider.tag == "Player") {
					collider.GetComponent<Controller2D>().status.colliders.Remove(gameObject);
					Die ();
					break;
				}
			}
		} 
	}

	void Die() {
		Destroy (gameObject);
	}
}
