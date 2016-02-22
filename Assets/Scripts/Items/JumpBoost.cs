using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class JumpBoost : Item, IEquipable {
	
	public JumpBoost() {
		name = "Jump Boost";
		rarity = Rarity.Rare;
	}

	public void Equip(GameObject user) {
		Inventory inventory = user.GetComponent<Inventory>();
		inventory.equipped = this;
		inventory.Remove(this);
		user.GetComponent<PlayerController>().maxMultiJumps++;
		GameObject.Find("Player/BoostParticleSystem").GetComponentInChildren<ParticleSystem>().Play();
	}

	public void Unequip(GameObject user) {
		Inventory inventory = user.GetComponent<Inventory>();
		inventory.Add(this);
		inventory.equipped = null;
		user.GetComponent<PlayerController>().maxMultiJumps--;
	}

	public override bool UseItem(GameObject user) {
		Equip(user);
		return true;
	}
}
