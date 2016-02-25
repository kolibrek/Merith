using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class KillableEnemy : MonoBehaviour {

	Controller2D controller;
	GameObject player;

	// Use this for initialization
	void Start () {
		controller = GetComponent<Controller2D> ();
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (controller.status.above) {
			foreach (GameObject collider in controller.status.colliders) {
				if (collider.GetComponent<Controller2D>()) {
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
