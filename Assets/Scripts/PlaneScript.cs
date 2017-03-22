using UnityEngine;
using System.Collections;
using Rewired;
using UnityEngine.SceneManagement;

public class PlaneScript : MonoBehaviour, IShootable {

	public int PlayerID;
	public Transform ShootOrigin;
	public GameObject BulletPrefab;
	public Transform Propeller;

	Player player; 
	int Health = 3;
	bool IsBoosting = false;

	float
	Rotation = 550f,
	Thrust = 75f,
	BoostModifier = 700f,
	CurrentBoostMod = 0f
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

		if (!IsBoosting && player.GetButton ("Booster")) { Boost (); }
		if (player.GetButton ("Reload")) { SceneManager.LoadScene ("Game"); }

		if(player.GetButton("Thrust") || CurrentBoostMod > 0f) {
			Debug.Log ("PLAYER " + PlayerID + " BOOST: " + CurrentBoostMod);
			rigidbody.AddForce( forward * (Thrust + CurrentBoostMod) );
			if (CurrentBoostMod > 0f)
				DecreaseBoost ();
		}

		if(player.GetButtonDown("Shoot")) {
			GameObject bullet = (GameObject)Instantiate(BulletPrefab, ShootOrigin.position, Quaternion.LookRotation(forward, Vector3.up));
			Bullet b = bullet.GetComponent<Bullet>();
			b.SetOriginSpeed( rigidbody.velocity.magnitude );
			b.SetOriginObject (gameObject);
		}

		if(Propeller) {
			Propeller.Rotate(rigidbody.velocity.magnitude, 0, 0);
		}
	}

	public void TakeDamage ()
	{
		Health--;
		CheckDead ();
	}

	void CheckDead()
	{
		if (Health <= 0) SceneManager.LoadScene ("Game");
	}

	void Boost() {
		IsBoosting = true; 
		CurrentBoostMod = BoostModifier;
	}

	void DecreaseBoost()
	{
		CurrentBoostMod *= 0.9f;
		if (CurrentBoostMod <= 0.2f) {
			CurrentBoostMod = 0f;
			Invoke ("BoostComplete", 2f);
		}
	}

	void BoostComplete()
	{
		IsBoosting = false;
	}
}
