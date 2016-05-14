using UnityEngine;
using System.Collections;
using Rewired;

public class PlaneScript : MonoBehaviour {

	public int PlayerID;

	Player player; 

	float
	Rotation = 500f,
	Thrust = 25f
	;

	Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
		rigidbody = gameObject.GetComponent<Rigidbody>();
		player = ReInput.players.GetPlayer(PlayerID);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(player.GetAxis("Move Horizontal"));
		Vector2 move = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
		rigidbody.AddTorque(0,0,-move.x*Rotation*Time.deltaTime);
			
		Vector3 forward = transform.localRotation * new Vector3(1,0,1);

		if(move.y > 0) {
			rigidbody.AddForce( forward * Thrust );
		}
	}
}
