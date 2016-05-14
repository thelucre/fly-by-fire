using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Collider))]
public class Bullet : MonoBehaviour {

	Rigidbody rigidbody;
	float Speed = 30f;

	void Start() {
		rigidbody = gameObject.GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		rigidbody.velocity = transform.forward * Speed;
	}

	void OnBecameInvisible() {
		Destroy(gameObject);
	}
		
}
