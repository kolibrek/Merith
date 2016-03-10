using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Controller2D))]
[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour {

	public float speed = 10;
	public float maxJumpHeight = 4f;
	public float minJumpHeight = 1f;
	float timeToJumpApex;
	public int maxMultiJumps = 1;
	public float attackCoolDown = 2;
	float attackTimer;
	float maxJumpVelocity;
	float minJumpVelocity;
	
	int jumpsLeft;

	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallJumpLeap;
	public Vector2 throwBack;
	public float wallSlideSpeedMax = 3;
	public float wallStickTime = 0.3f;
	float timeToWallUnstick;
	float accelerationTimeAirborne = 0.3f;
	float accelerationTimeGrounded = 0.1f;
	float velocityXSmoothing;

	Controller2D controller;
	Rigidbody2D rb;

	public GameObject attackObject;

	// Use this for initialization
	void Start () {
		controller = GetComponent<Controller2D>();
		rb = GetComponent<Rigidbody2D>();

		// Kinematic equations!
		timeToJumpApex = Mathf.Sqrt((2 * maxJumpHeight) / -Physics2D.gravity.y);
		maxJumpVelocity = Mathf.Abs(Physics2D.gravity.y) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(Physics2D.gravity.y) * minJumpHeight);

		jumpsLeft = maxMultiJumps;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0 || controller.status.dying) {
			return;
		}
		Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		controller.status.input = input;
		Vector2 velocity = rb.velocity;
		int wallDirX = (controller.status.left)? -1 : 1;

		// Determine velocity.x
		if (!controller.status.attacking) {
			float targetVelocityX = input.x * speed;
			velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.status.below)? accelerationTimeGrounded : accelerationTimeAirborne);
		}
		// Determine velocity.y
		if (controller.status.below || controller.status.left || controller.status.right) {
			jumpsLeft = maxMultiJumps;
		}

		CalculateWallSlide(input, ref velocity, wallDirX);

		// Jump Mechanics
		if (Input.GetButtonDown("Jump") && !controller.status.dying) {
			JumpControl(input, ref velocity, wallDirX);
		}
		if (Input.GetButtonUp("Jump") && !controller.status.dying) {
			if (velocity.y > minJumpVelocity) {
				velocity.y = minJumpVelocity;
			}
		}

		if (Input.GetButtonDown("Fire1") && attackTimer <= 0) {
			controller.status.attacking = true;
			GameObject attackClone = Instantiate<GameObject>(attackObject);
			attackClone.transform.SetParent(this.transform);
			attackClone.transform.localPosition = new Vector3(1.2f,0f,0f);
			attackClone.transform.localScale = new Vector3(1f,1f,1f);
			attackTimer = attackCoolDown;
		}
		if (attackTimer > 0) {
			attackTimer -= Time.deltaTime;
		}
		if (Input.GetButtonUp("Fire1")) {
			controller.status.attacking = false;
		}

		if (input.x != 0 && !controller.status.wallSliding) {
			controller.status.facingDirection = (int)Mathf.Sign(input.x);
		}
		// Thrown back after taking damage
		if (controller.status.takingDamage && !controller.status.invulnerable && !controller.status.dying) {
			velocity.x = -wallDirX * throwBack.x;
			if (controller.status.below) {
				velocity.y = throwBack.y;
			}
			if (controller.status.above) {
				velocity.y = -throwBack.y;
			}
			GetComponent<HealthController>().TakeDamage(controller.status.damageTaken);
		}
		rb.velocity = velocity;
	}

	// Do most of the logic for figuring out jump speed and direction here!
	void JumpControl(Vector2 input, ref Vector2 velocity, int wallDirX) {
		if (controller.status.wallSliding) {
			// if moving towards wall
			if (wallDirX == Mathf.Sign (input.x)) {
				velocity.x = -wallDirX * wallJumpClimb.x;
				velocity.y = wallJumpClimb.y;
				// if not moving horizontally
			} else if (input.x == 0) {
				velocity.x = -wallDirX * wallJumpOff.x;
				velocity.y = wallJumpOff.y;
				// if moving away from wall
			} else {
				velocity.x = -wallDirX * wallJumpLeap.x;
				velocity.y = wallJumpLeap.y;
			}
			jumpsLeft--;
		}
		if (controller.status.below || jumpsLeft > 0) {
			velocity.y = maxJumpVelocity;
			jumpsLeft--;
		}
		if (input.y < 0 && controller.status.below) {
			foreach (GameObject collider in controller.status.colliders) {
				if (collider.GetComponent<Vehicle>() || collider.GetComponent<Platform>()) {
					if (collider.GetComponent<Platform>()) {
						collider.GetComponent<Platform>().TempDisableCollider();
						velocity.y = -minJumpVelocity;
					}
					if (collider.GetComponent<Vehicle>()) {
						collider.GetComponent<Vehicle>().Disembark(gameObject);
					}
					controller.status.colliders.Remove(collider);
					jumpsLeft++;
					break;
				}
			}
		}
	}

	void CalculateWallSlide(Vector2 input, ref Vector2 velocity, int wallDirX) {
		if (velocity.y < 0 && !controller.status.below && (controller.status.right || controller.status.left)) {
			controller.status.wallSliding = true;
			if (velocity.y < -wallSlideSpeedMax) {
				velocity.y = -wallSlideSpeedMax;
			}
			if (timeToWallUnstick > 0) {
				velocityXSmoothing = 0;
				velocity.x = 0;
				if (Mathf.Sign (input.x) != wallDirX && input.x != 0) {
					timeToWallUnstick -= Time.deltaTime;
				} else {
					timeToWallUnstick = wallStickTime;
				}
			} else {
				velocity.x += input.x * speed;
				timeToWallUnstick = wallStickTime;
				controller.status.wallSliding = false;
			}
		} else {
			controller.status.wallSliding = false;
		}
	}
}
