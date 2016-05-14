using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Collider))]
public class Bullet : MonoBehaviour {

	Rigidbody rigidbody;
	float OriginSpeed = 0, Speed = 50f;

	public void SetOriginSpeed(float _playerMagnitude) { OriginSpeed = _playerMagnitude; }

	void Start() {
		rigidbody = gameObject.GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		rigidbody.velocity = transform.forward * (Speed + OriginSpeed);
	}

	void OnBecameInvisible() {
		Destroy(gameObject);
	}
		
}
