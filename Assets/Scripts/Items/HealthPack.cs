using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class HealthPack : ItemStack {

	public HealthPack() {
		name = "Health Pack";
		count = 1;
		maxCount = 1;
		rarity = Rarity.Common;
	}

	public override bool UseItem(GameObject user) {
		user.GetComponent<HealthController>().TakeDamage(-5);
		user.GetComponent<Inventory>().Remove(this);
		GameObject.Find("Player/HealthParticleSystem").GetComponent<ParticleSystem>().Play();
		return true;
	}
}
