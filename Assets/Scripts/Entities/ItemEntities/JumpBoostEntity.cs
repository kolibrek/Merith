using UnityEngine;
using System.Collections;

public class JumpBoostEntity : ItemEntity {

	public override void GetItem(GameObject receiver) {
		Item itemCopy = ScriptableObject.CreateInstance<JumpBoost>();
		itemCopy.sprite = itemType.sprite;
		receiver.GetComponent<Inventory>().Add(itemCopy);
	}
}
