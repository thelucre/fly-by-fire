using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	// Source: http://stackoverflow.com/a/22018593/840982

	public Transform player1;
	public Transform player2;

	private const float DISTANCE_MARGIN = 1.0f;

	private Vector3 middlePoint, targetPos;
	private float distanceFromMiddlePoint;
	private float distanceBetweenPlayers;
	private float cameraDistance;
	private float aspectRatio;
	private float fov;
	private float tanFov;

	void Start() {
		aspectRatio = Screen.width / Screen.height;
		tanFov = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2.0f);
	}

	void FixedUpdate () {
		// Position the camera in the center.
		Vector3 newCameraPos = Camera.main.transform.position;
		newCameraPos.x = middlePoint.x;
		newCameraPos.y = middlePoint.y;
//		Camera.main.transform.position = newCameraPos;

		// Find the middle point between players.
		Vector3 vectorBetweenPlayers = player2.position - player1.position;
		middlePoint = player1.position + 0.5f * vectorBetweenPlayers;

		// Calculate the new distance.
		distanceBetweenPlayers = vectorBetweenPlayers.magnitude;
		cameraDistance = (distanceBetweenPlayers / 2.0f / aspectRatio) / tanFov;

		// Set camera to new position.
		Vector3 dir = (newCameraPos - middlePoint).normalized;
		targetPos = middlePoint + dir * (cameraDistance + DISTANCE_MARGIN);

		// Closest that the camera can go 
		targetPos.z = Mathf.Min(targetPos.z, -10f);

		Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPos, 8.0f*Time.deltaTime);
	}
}
