using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speaker : MonoBehaviour {

	void Start () {
		if (PlayerPrefs.GetInt ("Speaker") == 1) {
			gameObject.GetComponent<Toggle> ().isOn = true;
			//GameObject.Find ("AudioManager").GetComponent<Audio> ().UnmuteIt ();
		} else {
			gameObject.GetComponent<Toggle> ().isOn = false;
		}
	}

	public void MuteUnmute(){
		if (gameObject.GetComponent<Toggle> ().isOn) {
			PlayerPrefs.SetInt ("Speaker", 1);
			GameObject.Find ("AudioManager").GetComponent<Audio> ().UnmuteIt ();
		} else {
			PlayerPrefs.SetInt ("Speaker", 0);
			GameObject.Find ("AudioManager").GetComponent<Audio> ().MuteIt ();
		}
	}
}
