using UnityEngine;
using System.Collections;

public abstract class ItemStack : Item {

	public int count;
	public int maxCount;

	public void AddToStack(ItemStack stack) {
		if (stack.name == this.name) {
			if (stack.count + this.count <= maxCount) {
				this.count += stack.count;
			} else {
				this.count = maxCount;
			}
		}
	}
}
