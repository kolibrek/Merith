using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StatusDisplay : MonoBehaviour {

	public Controller2D controller;
	Inventory inventory;
	Text statusDisplay;

	// Use this for initialization
	void Start () {
		statusDisplay = GetComponent<Text>();
		inventory = controller.GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	void Update () {
		statusDisplay.text = "Player Status";
		statusDisplay.text += "\nFPS: " + 1/Time.deltaTime;
		statusDisplay.text += "\nabove: " + controller.status.above + " below: " + controller.status.below;
		statusDisplay.text += "\nleft: " + controller.status.left + " right: " + controller.status.right;
		statusDisplay.text += "\nSlope angle: " + controller.status.slopeAngle;
		statusDisplay.text += "\nCollision objects:";
		StartCoroutine("ListColliders");
		statusDisplay.text += "\nInventory:";
		statusDisplay.text += "\n Equipped: ";
		statusDisplay.text += (inventory.equipped != null)? inventory.equipped.name : "None";
		StartCoroutine("ListInventory");
	}

	IEnumerator ListColliders() {
		foreach (GameObject collider in controller.status.colliders) {
			statusDisplay.text += "\n  " + collider.name;
		}
		yield return null;
	}

	IEnumerator ListInventory() {
		foreach (Item item in inventory.GetInventoryList()) {
			statusDisplay.text += "\n " + item.name;
		}
		yield return null;
	}
}
