using UnityEngine;
using System.Collections;
using Rewired;

public class PlaneScript : MonoBehaviour {

	public int PlayerID;
	public Transform ShootOrigin;
	public GameObject BulletPrefab;
	public Transform Propeller;

	Player player; 

	float
	Rotation = 500f,
	Thrust = 45f
	;

	Rigidbody rigidbody;

	// Use this for initialization
	void Start () 
	{
		rigidbody = gameObject.GetComponent<Rigidbody>();
		player = ReInput.players.GetPlayer(PlayerID);
		Debug.Log(player.name);
		Debug.Log("Joystick count : " +ReInput.controllers.joystickCount);

		if (Input.GetJoystickNames().Length == 0) {
			Debug.Log("no joysticks");
		}

		if (Input.GetJoystickNames().Length > 0) {
			foreach(string joystick in Input.GetJoystickNames()) {
				print (joystick);
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector2 move = new Vector2(player.GetAxis("Rotate"),0);
		rigidbody.AddTorque(0,0,-move.x*Rotation*Time.deltaTime);
			
		Vector3 forward = transform.localRotation * new Vector3(1,0,1);

		if(player.GetButton("Thrust")) {
			rigidbody.AddForce( forward * Thrust );
		}

		if(player.GetButtonDown("Shoot")) {
			GameObject bullet = (GameObject)Instantiate(BulletPrefab, ShootOrigin.position, Quaternion.LookRotation(forward, Vector3.up));
			bullet.GetComponent<Bullet>().SetOriginSpeed( rigidbody.velocity.magnitude );
		}

		if(Propeller) {
			Propeller.Rotate(rigidbody.velocity.magnitude, 0, 0);
		}
	}
}
