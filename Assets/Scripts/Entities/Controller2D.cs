using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class Controller2D : MonoBehaviour {

	public CollisionStatus status;

	float threshold = 0.0000001f;

	// Use this for initialization
	void Start () {
		status = new CollisionStatus();
		status.Setup();
	}

	void Update() {
		if (status.colliders.Count == 0) {
			status.Reset();
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (!status.colliders.Contains(coll.gameObject)) {
	 		foreach (ContactPoint2D contact in coll.contacts) {
				//Debug.Log(contact.normal.x + ", " + contact.normal.y);

				if (contact.normal.y < -threshold && (coll.gameObject.GetComponent<PlatformEffector2D>() == null)) {				// Platform Effectors are a special case.
					status.above = true;
					break;
				}
				if (contact.normal.y > threshold) {
					status.below = true;
					status.slopeAngle = contact.normal.y;
					break;
				}
				if (contact.normal.x < -threshold) {
					status.right = true;
					break;
				}
				if (contact.normal.x > threshold) {
					status.left = true;
					break;
				}
			}
			status.colliders.Add(coll.gameObject);
		}
		if (!status.invulnerable && coll.gameObject.tag == "Damaging") {
			status.takingDamage = true;
			status.damageTaken = coll.gameObject.GetComponent<Damager>().GetDamageAmount();
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		foreach (ContactPoint2D contact in coll.contacts) {
			Debug.DrawRay(contact.point, contact.normal, Color.blue);
		}
		if (!status.invulnerable && coll.gameObject.tag == "Damaging") {
			status.takingDamage = true;
			status.damageTaken = coll.gameObject.GetComponent<Damager>().GetDamageAmount();
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		foreach (ContactPoint2D contact in coll.contacts) {
			//Debug.Log(contact.normal.x + ", " + contact.normal.y);
			if (contact.normal.y < -threshold) {
				status.above = false;
				break;
			}
			if (contact.normal.y > threshold) {
				status.slopeAngle = 0;
				status.below = false;
				break;
			}
			if (contact.normal.x < -threshold) {
				status.right = false;
				break;
			}
			if (contact.normal.x > threshold) {
				status.left = false;
				break;
			}
		}
		status.colliders.Remove(coll.gameObject);
		/*if (status.colliders.Contains(coll.gameObject)) {

		}*/
	}

	public struct CollisionStatus {
		// which sides are colliding?
		public bool above, below;
		public bool left, right;
		public bool wallSliding;
		public bool riding;
		public float slopeAngle;
		public List<GameObject> colliders;
		public Vector2 input;

		// 1 = right, -1 = left
		public int facingDirection;
		public bool takingDamage, invulnerable, dying;
		public int damageTaken;

		public void Setup() {
			colliders = new List<GameObject>();
			above = below = false;
			left = right = false;
			takingDamage = invulnerable = dying = false;
			damageTaken = 0;
			facingDirection = 1;
			slopeAngle = 0f;
		}

		public void Reset() {
			above = below = false;
			left = right = false;
			takingDamage = false;
			damageTaken = 0;
		}

		public void ResetInvulnerable() {
			invulnerable = false;
		}
	}
}
