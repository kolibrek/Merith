using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class PlatformPatroller : MonoBehaviour {

	public float speed = 4;

	Controller2D controller;
	Rigidbody2D rb;
	RaycastOrigins origins;

	public LayerMask platformLayer;

	// Use this for initialization
	void Start () {
		controller = GetComponent<Controller2D>();
		rb = GetComponent<Rigidbody2D>();
		origins = new RaycastOrigins();
		controller.status.facingDirection = 1;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateRaycastOrigins();
		Vector2 origin = (controller.status.facingDirection > 0)? origins.bottomRight : origins.bottomLeft;
		RaycastHit2D hit = Physics2D.Raycast(origin, -Vector2.up, 0.5f, platformLayer);
		//Debug.DrawRay(origin, -Vector2.up, Color.red);
		if (hit) {
			if ((controller.status.left && controller.status.facingDirection == -1) || 
			    (controller.status.right && controller.status.facingDirection == 1)) {
				Flip ();
			}
			Move();
		} else {
			Flip ();
		}
	}

	void UpdateRaycastOrigins() {
		Bounds bounds = GetComponent<BoxCollider2D>().bounds;
		origins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
		origins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
	}

	void Flip() {
		controller.status.facingDirection *= -1;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	void Move() {
		Vector2 velocity = rb.velocity;
		velocity.x = speed * controller.status.facingDirection;
		rb.velocity = velocity;
	}

	public struct RaycastOrigins {
		public Vector2 bottomRight, bottomLeft;
	}
}
