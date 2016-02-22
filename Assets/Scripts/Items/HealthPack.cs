using UnityEngine;
using System.Collections;

public class HealthPack : ItemStack {

	GameObject player;

	public HealthPack() {
		name = "Health Pack";
		count = 1;
		maxCount = 1;
		rarity = Rarity.Common;
	}

	public override bool UseItem(GameObject user) {
		user.GetComponent<HealthController>().TakeDamage(-5);
		user.GetComponent<Inventory>().Remove(this);
		user.GetComponent<ParticleSystem>().Play();
		return true;
	}
}
