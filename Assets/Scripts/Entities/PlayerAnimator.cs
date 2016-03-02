using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour {

	Animator anim;
	Controller2D controller;
	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		controller = GetComponent<Controller2D>();
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		//ResetAnimations();
		if (controller.status.dying) {
			anim.SetTrigger("dying");
		}
		// Player in the air?
		if (!(controller.status.below || controller.status.left || controller.status.right)) {
			anim.SetBool("falling", rb.velocity.y < 0);
			anim.SetBool("jumping", rb.velocity.y > 0);
			anim.SetBool("falling", rb.velocity.y < 0);
		} else {
			anim.SetBool("jumping", false);
			anim.SetBool("falling", false);
		}
		anim.SetBool("wallsliding", controller.status.wallSliding);
		anim.SetFloat("velocity", Mathf.Abs(rb.velocity.x));
		anim.SetBool ("attacking", controller.status.attacking);

		if (controller.status.facingDirection != Mathf.Sign(rb.transform.localScale.x)) {
			Flip();
		}
	}

	void ResetAnimations() {
		anim.SetFloat("velocity", 0);
		anim.SetBool("falling", false);
		anim.SetBool("jumping", false);
		anim.SetBool("wallsliding", false);
	}

	void Flip() {
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
