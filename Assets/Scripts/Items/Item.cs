using UnityEngine;
using System.Collections;

public class Item : ScriptableObject {

	new public string name;
	public Rarity rarity;
	public Sprite sprite;

	public virtual bool UseItem(GameObject user) {
		return false;
	}

	public virtual string GetName() {
		return this.name;
	}
}

public enum Rarity {
	Common = 1,
	Uncommon = 2,
	Rare = 3,
	Unique = 4
}
