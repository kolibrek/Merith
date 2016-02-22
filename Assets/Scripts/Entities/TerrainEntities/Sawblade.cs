using UnityEngine;
using System.Collections;

public class Sawblade : MonoBehaviour {

	[Range(0,2)]
	public float spinSpeed = 1.5f;
	Vector3 rotate;

	// Use this for initialization
	void Start () {											// Axis orientation for this is screwy, the sawblade interprets y as z
		rotate = new Vector3(0,spinSpeed,0);				// Probably need to change a setting in Blender...
	}
	
	void FixedUpdate () {
		rotate.y = spinSpeed * Time.deltaTime * 100;
		transform.Rotate(rotate);
	}
}
