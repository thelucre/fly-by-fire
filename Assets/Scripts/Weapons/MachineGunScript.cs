using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunScript : GunScript {

	public MachineGunScript()
	{
		ProjectilePrefab = "Bullet";
		FiringType = FIRING_TYPE.AUTOMATIC;
		RateOfFire = 0.1f;
	}
}
