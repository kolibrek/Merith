using UnityEngine;
using System.Collections;

[RequireComponent (typeof(PlatformEffector2D))]
public class Platform : MonoBehaviour {

	Collider2D coll;

	// Use this for initialization
	void Start () {
		coll = GetComponent<Collider2D>();
	}

	public void TempDisableCollider() {
		coll.enabled = false;
		Invoke("EnableCollider", 0.5f);
	}

	void EnableCollider() {
		coll.enabled = true;
	}
}
