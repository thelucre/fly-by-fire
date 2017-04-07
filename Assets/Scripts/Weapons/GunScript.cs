using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum FIRING_TYPE {
	SEMIAUTOMATIC,
	AUTOMATIC
};

public class GunScript : MonoBehaviour 
{
	
	// Only applies if FiringType is AUTOMATIC
	protected float RateOfFire = 0.2f;

	// Counter to time when next shot is possible
	protected float CoolDown = 0f;

	// Was pressing shoot button last frame?
	protected bool WasPressingFireButton = false;

	// Button up required to fire semi automatic
	// Button pressed used to check automatic, with RateOfFire limiting Shoot()
	protected FIRING_TYPE FiringType = FIRING_TYPE.SEMIAUTOMATIC; 

	// String to load prefab from "Prefabs/Projectiles/{prefab}" path
	protected string ProjectilePrefab;

	/// <summary>
	/// Determines whether this instance can shoot.
	/// </summary>
	/// <returns><c>true</c> if this instance can shoot the specified fireButtonPressed; otherwise, <c>false</c>.</returns>
	/// <param name="fireButtonPressed">If set to <c>true</c> fire button pressed.</param>
	public bool ShouldShoot(bool fireButtonPressed)
	{	
		bool canShoot = false;

		switch (FiringType) 
		{
		case FIRING_TYPE.AUTOMATIC:
			
			// Subtract the change in time to measure cooldown
			CoolDown -= Time.deltaTime;
			break;

		case FIRING_TYPE.SEMIAUTOMATIC:
			// Faking a cooldown based on if the button is released or not
			CoolDown = (WasPressingFireButton) ? 1f : 0f;
			break;
		}

		// If firing cool down is still occuring, you can't shoot
		canShoot = (CoolDown <= 0f);

		// Update last press check 
		WasPressingFireButton = fireButtonPressed;

		// Must pass firing tests and the button should be pressed to shoot
		return (fireButtonPressed && canShoot);
	}

	public void Shoot(Vector3 position, Quaternion rotation, float speed, GameObject origin)
	{	
		GameObject bullet = Instantiate(
			Resources.Load("Prefabs/Projectiles/"+ProjectilePrefab, typeof(GameObject)),
			position, 
			rotation
		) as GameObject;

		if (bullet == null)
			Debug.Log ("Couldn't instantiate bullet: " + "Prefabs/Projectiles/" + ProjectilePrefab);

		Bullet b = bullet.GetComponent<Bullet>();
		b.SetOriginSpeed( speed );
		b.SetOriginObject( origin );

		if (FiringType == FIRING_TYPE.AUTOMATIC)
			CoolDown = RateOfFire;

		// Spawn the bullet on the Clients
		NetworkServer.Spawn(bullet);
	}
}
