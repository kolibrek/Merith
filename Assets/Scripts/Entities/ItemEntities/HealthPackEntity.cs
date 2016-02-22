using UnityEngine;
using System.Collections;

public class HealthPackEntity : ItemEntity {
	
	public override void GetItem(GameObject receiver) {
		Item itemCopy = ScriptableObject.CreateInstance<HealthPack>();
		itemCopy.sprite = itemType.sprite;
		receiver.GetComponent<Inventory>().Add(itemCopy);
	}
}
