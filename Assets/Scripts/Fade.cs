using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

	void OnEnable () {
		gameObject.GetComponent<Animation> ().Play ("FadeIN");
	}

	void OnDisable () {
		gameObject.GetComponent<Animation> ().Play ("FadeOUT");
	}
}
