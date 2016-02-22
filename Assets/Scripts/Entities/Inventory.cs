using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public Controller2D controller;
	public int size;
	List<Item> inventory;
	int current;

	[HideInInspector]
	public Item equipped;

	bool scrollAxisInUse = false;

	GameObject invDisplay;
	Image invImage;
	Text invText;

	// Use this for initialization
	void Start () {
		inventory = new List<Item>();
		current = 0;
		invDisplay = GameObject.Find("InventoryDisplay");
		invImage = GameObject.Find ("InvImage").GetComponent<Image>();
		invText = invDisplay.GetComponentInChildren<Text>();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire3") && !controller.status.dying) {
			GetComponent<Inventory>().UseCurrent();
		}
		float scrollDir = Input.GetAxisRaw("Scroll");
		if (scrollDir != 0) {
			if (!scrollAxisInUse && inventory.Count > 0) {
				Debug.Log(scrollDir);
				GetComponent<Inventory>().SwapItem(scrollDir);
				scrollAxisInUse = true;
			}
		} else {
			scrollAxisInUse = false;
		}
	}

	public List<Item> GetInventoryList() {
		return inventory;
	}

	public bool Add(Item item) {
		if (inventory.Count < size) {
			inventory.Add(item);
			ResetCurrent();
			return true;
		}
		return false;
	}

	public void Remove(Item item) {
		inventory.Remove(item);
		ResetCurrent();
	}

	public void UseCurrent() {
		if (inventory.Count > 0 && current <= inventory.Count - 1) {
			Debug.Log("Using " + inventory[current].GetName());
			inventory[current].UseItem(gameObject);
			// Replace current slot with next item in inventory
			ResetCurrent();
		} else {
			Debug.Log("Inventory Empty!");
		}
	}

	public void ResetCurrent() {
		if (inventory.Count > 0) {
			while (current > inventory.Count - 1) {
				current--;
			}
			invText.text = inventory[current].name;
			invImage.sprite = inventory[current].sprite;
			invImage.color = new Color(1,1,1,1);
		} else {
			invText.text = "inventory";
			invImage.sprite = null;
			invImage.color = new Color(0,0,0,0);
		}
	}

	public void AddSlots(int slots) {
		size += slots;
	}

	public void RemoveSlots(int slots) {
		if (size - slots >= 0) {
			size -= slots;
		} else {
			size = 0;
		}
	}

	public void SwapItem(float direction) {
		if (direction > 0) {
			current = (current < inventory.Count - 1)? current + 1 : 0;
		} else {
			current = (current != 0 && current >= inventory.Count - 1)? current - 1 : inventory.Count - 1;
		}
		ResetCurrent ();
	}
}
