using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetQuote : MonoBehaviour {
	public TextAsset quoteData;
	public string quote;
	private int qNo;
	void Start () {
		
	}

	public void setQuote (){
		qNo = Random.Range (1, 13);
		quoteData = Resources.Load ("Quotes/" + qNo) as TextAsset;
		quote = "Quote #" + qNo + "\n\n" + "<i>" + quoteData.text + "</i>";
		gameObject.GetComponent<Text> ().text = quote;
	}

}
