using UnityEngine;
using System.Collections;

public abstract class ItemEntity : MonoBehaviour {
	
	public Item itemType;
	[Range(-4,4)]
	public float spinSpeed = 1.5f;
	Vector3 rotate;

	public abstract void GetItem(GameObject receiver);

	void Start() {
		rotate = new Vector3(0,spinSpeed,0);
	}

	void Update() {
		rotate.y = spinSpeed * Time.deltaTime * 100;
		transform.Rotate(rotate);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.GetComponent<Inventory>()) {
			GetItem(coll.gameObject);
			Destroy(gameObject);
		}
	}
}
