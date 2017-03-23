using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;


public class GameControllerScript : MonoBehaviour 
{
	// Singleton
	public static GameControllerScript Instance;

	public Player player; 
	public CameraScript Cam;

	void Awake()
	{
		if (Instance == null)
			Instance = this;

		Cam = Camera.main.gameObject.GetComponent<CameraScript> ();
	}
		
	void Start () 
	{
		player = ReInput.players.GetPlayer(0);
	}

	void Update () 
	{
		
	}
}
