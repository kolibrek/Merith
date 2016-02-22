using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public int size;
	List<Item> inventory;
	GameObject invDisplay;
	Image invImage;
	Text invText;

	// Use this for initialization
	void Start () {
		inventory = new List<Item>();
		invDisplay = GameObject.Find("InventoryDisplay");
		invImage = invDisplay.GetComponentInChildren<Image>();
		invText = invDisplay.GetComponentInChildren<Text>();
	}

	public List<Item> GetInventoryList() {
		return inventory;
	}

	public bool Add(Item item) {
		if (inventory.Count < size) {
			inventory.Add(item);
			invText.text = item.name;
			invImage.sprite = item.sprite;
			invImage.color = new Color(1,1,1,1);
			return true;
		}
		return false;
	}

	public bool Remove(Item item) {
		invText.text = "";
		invImage.sprite = null;
		invImage.color = new Color(1,1,1,0);
		return inventory.Remove(item);
	}

	public void UseCurrent() {
		if (inventory.Count != 0) {
			Item current = inventory[0];
			Debug.Log("Using " + current.GetName());
			current.UseItem(gameObject);
		} else {
			Debug.Log("Inventory Empty!");
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

	public void Sort() {
		inventory.Sort();
	}
}
