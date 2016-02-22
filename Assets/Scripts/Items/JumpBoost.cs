using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class JumpBoost : Item {
	
	public JumpBoost() {
		name = "Jump Boost";
		rarity = Rarity.Rare;
	}

	public override bool UseItem(GameObject user) {
		user.GetComponent<PlayerController>().maxMultiJumps++;
		user.GetComponent<Inventory>().Remove(this);
		GameObject.Find("Player/BoostParticleSystem").GetComponentInChildren<ParticleSystem>().Play();
		return true;
	}
}
