using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForLoyalsheep : MonoBehaviour {

	public GameObject LoyalsheepPanel;
	public GameObject Logo;
	public GameObject Presents;

	void Awake () {
		LoyalsheepPanel.SetActive (false);
		if (PlayerPrefs.GetInt ("loyalsheep") == 1) {
			PlayerPrefs.SetInt ("loyalsheep", 0);
			LoyalsheepPanel.SetActive (true);
			StartCoroutine (SerialAnimation());
		}
	}

	IEnumerator SerialAnimation () {
		yield return new WaitForSeconds (0.5f);

		Logo.GetComponent<Animation> ().Play("Logo");
		yield return new WaitForSeconds (2.5f);

		Presents.GetComponent<Animation> ().Play("presents");
		yield return new WaitForSeconds (2.5f);

		LoyalsheepPanel.GetComponent<Animation> ().Play("LoyalsheepPanel");
		yield return new WaitForSeconds (1.5f);

		LoyalsheepPanel.SetActive (false);
	}
}
