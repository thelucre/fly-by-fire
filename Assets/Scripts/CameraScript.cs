using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraScript : MonoBehaviour {

	// Source: http://stackoverflow.com/a/22018593/840982

	public Transform player1;
	public Transform player2;
	public List<Transform> players;

	private const float DISTANCE_MARGIN = 1.0f;

	private Vector3 middlePoint, targetPos;
	private float distanceFromMiddlePoint;
	private float distanceBetweenPlayers;
	private float cameraDistance;
	private float aspectRatio;
	private float fov;
	private float tanFov;

	float 
		ShakeAmount = 0f,
		ShakeDecay = 0.1f
	;


	void Start() {
		// Will break in free aspect mode in hte editor :/
		aspectRatio = Screen.width / Screen.height;
		tanFov = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2.0f);

		players = new List<Transform>();
	}

	void FixedUpdate () {
		// Position the camera in the center.
		Vector3 newCameraPos = Camera.main.transform.position;

		// Find the middle point between all players.
		middlePoint = FindCenterPoint(players);
		newCameraPos.x = middlePoint.x;
		newCameraPos.y = middlePoint.y;

		// Calculate the max distance between all players
		distanceBetweenPlayers = FindMaxDistanceBetweenPlayers(players);
		cameraDistance = (distanceBetweenPlayers / 2.0f / aspectRatio) / tanFov;

		// Set camera to new position.
		Vector3 dir = (newCameraPos - middlePoint).normalized;
		targetPos = middlePoint + dir * (cameraDistance + DISTANCE_MARGIN);

		// Closest that the camera can go 
		targetPos.z = Mathf.Min(targetPos.z, -20f);
			
		Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPos, 20.0f*Time.deltaTime)
			+ ((ShakeAmount*ShakeAmount) * Random.insideUnitSphere);

		ShakeAmount -= ShakeDecay; 
		ShakeAmount = Mathf.Max (ShakeAmount, 0);

	}

	Vector3 FindCenterPoint(List<Transform> transforms)
	{	
		if(transforms.Count == 0) return Vector3.zero;
		if(transforms.Count == 1) return transforms[0].position;
		var bounds = new Bounds(transforms[0].position, transforms[0].position);
		for (int i = 1; i < transforms.Count; i++)
				bounds.Encapsulate(transforms[i].position); 
		return bounds.center;
	}

	float FindMaxDistanceBetweenPlayers(List<Transform> transforms)
	{
		float dist = 0f;
		foreach(Transform p in transforms) {
			foreach(Transform p2 in transforms) {
				if(p == p2) continue;
				float d = Vector3.Distance(p.position, p2.position);
				if(d > dist) dist = d;
			}
		}
		return dist;
	}

	public void AddToShake(float amt) 
	{
		ShakeAmount += amt;
	}

	public void AddPlayer(Transform player) 
	{
		players.Add(player);
	}
}
