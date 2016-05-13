using UnityEngine;
using System.Collections;

public class PlaneScript : MonoBehaviour {

	float
	Rotation = 500f,
	Thrust = 25f
	;

	Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
		rigidbody = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 move = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
		rigidbody.AddTorque(0,0,-move.x*Rotation*Time.deltaTime);
			
		Vector3 forward = transform.localRotation * new Vector3(1,0,1);

		if(move.y > 0) {
			rigidbody.AddForce( forward * Thrust );
		}
	}
}
