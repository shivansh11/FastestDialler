using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour {

	private static Audio instance;
	public AudioSource Music;
	public AudioSource Click;
	public AudioSource CountDown;
	public AudioSource Engine;

	public static Audio GetInstance () {
		return instance;
	}

	void Awake () {
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad (this.gameObject);
	}

	public void PlayClick () {
		Click.Play ();
	}

	public void PlayCountDown () {
		CountDown.Play ();
	}

	public void StartCar () {
		Engine.Play (); 
	}
		
	public void MuteIt () {
		Music.mute = true;
	}

	public void UnmuteIt () {
		Music.mute = false;
	}
}
