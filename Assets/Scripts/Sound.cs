using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum AUDIO_TYPE {
	SFX,
	BGM
};

public class Sound : Singleton<Sound> {

	/****************************************************
	 * Use as Game.Instance.Sound.Play( Sound.KEY )
	 ****************************************************/

	//Control
	public bool muteGlobal;
	private Dictionary<string, AudioClip> SFX;

	private static string AUDIO_RESOURCES_DIR = "audio";
	private static string SFX_DIR = "SFX";
	private static string BGM_DIR = "BGM";

	/****************************************************
	 * SFX KEYS
	 ****************************************************/
	public static string 
	HIT = "explosion",
	SHOOT = "hit"
		;

	// BGM KEYS
	// public static string MUSIC_2 = "Game_BGM";
	// public static string BGM_TITLE = "TitleScreen";

	private AudioSource audioSource;

	public Sound ()
	{
		SFX = new Dictionary<string, AudioClip>();

	}

	private void AddSFXClip ( string file, AUDIO_TYPE type = AUDIO_TYPE.SFX ) 
	{	
		string dir = ( type == AUDIO_TYPE.SFX ) ? SFX_DIR : BGM_DIR;
		SFX.Add ( file , (AudioClip) Resources.Load ( AUDIO_RESOURCES_DIR + "/" + dir + "/" + file, typeof(AudioClip)) );
	}

	void Awake()
	{	
		// transform.parent = Camera.main.transform;
		// transform.localPosition = Vector3.zero;
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.spatialBlend = 0.0f;

		// sounds
		AddSFXClip ( SHOOT );
		AddSFXClip ( HIT );

		// music
		// AddSFXClip( MUSIC_2, AUDIO_TYPE.BGM );
		// AddSFXClip( BGM_TITLE, AUDIO_TYPE.BGM );
	}

	// METHODS
	public void PlaySFX(string clipToPlay)
	{
		AudioClip tmp;
		if( SFX.TryGetValue( clipToPlay, out tmp ) )
		{
			try 
			{
				audioSource.PlayOneShot ( tmp );
			} catch ( System.Exception e ) {
				Debug.Log ( clipToPlay + " failed to play" );
			}
		}
		else 
			Debug.Log ( "sound SFX could not be be played : " + clipToPlay ); 
	}

	public bool PlayBGM( string musicToPlay )
	{
		audioSource.loop = true;

		Debug.Log ("trying to play : " + musicToPlay);
		if( audioSource.isPlaying && audioSource.clip.name == musicToPlay ) return false; 
		if( audioSource.isPlaying ) audioSource.Stop();

		AudioClip tmp;
		if( SFX.TryGetValue( musicToPlay, out tmp ) )
		{
			if( tmp == null )
				return false;
			audioSource.clip = tmp;
			audioSource.Play ();
			return true;
		}
		else 
			Debug.Log ( "sound SFX could not be be played : " + musicToPlay );  
		return false;
	}

	public void StopMusic()
	{
		audioSource.Stop();
	}


}







