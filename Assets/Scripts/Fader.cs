using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fader : MonoBehaviour {

	public float LerpTime = 4.5f;
	float z = 0;

	void OnEnable () {
		StartCoroutine (FadeIn ());
	}

	IEnumerator FadeIn () {
		//Debug.Log ("Enabling");
		Color myColor = gameObject.GetComponent<Text> ().color;
		//Debug.Log ("MyColor = " + myColor.a);
		while (myColor.a < 1) {
			z += Time.deltaTime * LerpTime;
			myColor.a = Mathf.Lerp(0, 1, z);
			//Debug.Log ("MyColor = " + myColor.a);
			gameObject.GetComponent<Text> ().color = myColor;
			//Debug.Log (z);
			yield return new WaitForEndOfFrame ();
		}	
		z = 0;
		yield return new WaitForSeconds(1.5f);
		StartCoroutine (FadeOut());
	}

	IEnumerator FadeOut () {
		Color myColor = gameObject.GetComponent<Text> ().color;
		//Debug.Log ("MyColor = " + myColor.a);
		while (myColor.a > 0) {
			z += Time.deltaTime * LerpTime;
			myColor.a = Mathf.Lerp(1, 0, z);
			//Debug.Log ("MyColor = " + myColor.a);
			gameObject.GetComponent<Text> ().color = myColor;
			//Debug.Log (z);
			yield return new WaitForEndOfFrame ();
		}	
		z = 0;
		gameObject.SetActive (false);
	}

}
