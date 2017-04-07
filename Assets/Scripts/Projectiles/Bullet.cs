using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Collider))]
public class Bullet : MonoBehaviour {

	Rigidbody rigidbody;
	float OriginSpeed = 0, Speed = 80f;
	GameObject OriginObject; 


	public void SetOriginSpeed(float _playerMagnitude) { OriginSpeed = _playerMagnitude; }

	void Start() 
	{
		rigidbody = gameObject.GetComponent<Rigidbody>();
		Sound.Instance.PlaySFX (Sound.SHOOT);
	}

	void FixedUpdate() 
	{
		rigidbody.velocity = transform.forward * (Speed + OriginSpeed);
	}

	void OnBecameInvisible() {
		Destroy(gameObject);
	}

	public void SetOriginObject(GameObject obj)
	{
		OriginObject = obj;
	}

	void OnCollisionEnter(Collision hit)
	{
		IShootable shootable = hit.gameObject.GetComponent<IShootable> ();
		if (hit.gameObject != OriginObject && shootable != null) {
			shootable.TakeDamage ();
			Sound.Instance.PlaySFX (Sound.HIT);
			Destroy (gameObject);
		}
	}
		
}
