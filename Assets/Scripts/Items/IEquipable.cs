using UnityEngine;
using System.Collections;

public interface IEquipable {

	void Equip(GameObject user);

	void Unequip(GameObject user);
}
