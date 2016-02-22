using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public int size;
	List<Item> inventory;
	Item current;
	GameObject invDisplay;
	Image invImage;
	Text invText;

	// Use this for initialization
	void Start () {
		inventory = new List<Item>();
		current = null;
		invDisplay = GameObject.Find("InventoryDisplay");
		invImage = GameObject.Find ("InvImage").GetComponent<Image>();
		invText = invDisplay.GetComponentInChildren<Text>();
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
		if (current && inventory.Count != 0) {
			Debug.Log("Using " + current.GetName());
			current.UseItem(gameObject);
			// Replace current slot with next item in inventory
			ResetCurrent();
		} else {
			Debug.Log("Inventory Empty!");
		}
	}

	public void ResetCurrent() {
		current = (inventory.Count > 0)? inventory[0] : null;
		if (current) {
			invText.text = current.name;
			invImage.sprite = current.sprite;
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
}
