using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class Loader : MonoBehaviour {

	public GameObject slider;
	public GameObject NickPanel;
	public GameObject InputNick;
	public GameObject Countries;
	public GameObject CountryGreeting;
	public GameObject Tick;
	public GameObject Flag;
	public GameObject InvalidText;
	public GameObject Instructions;
	AsyncOperation operation;
	float progress = 0f;
	string nick;
	string country;
	string[] rows;
	ulong lastReward;

	public void Start (){
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		//Remove it after testing!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		//PlayerPrefs.DeleteAll ();
		Countries.SetActive(false);
		CountryGreeting.SetActive (false);
		Tick.SetActive(false);
		Flag.SetActive (false);

		PlayerPrefs.SetInt ("isLoadScene", 1);

		if (PlayerPrefs.HasKey ("rate") == false) 
			PlayerPrefs.SetInt ("rate", 0);

		if (PlayerPrefs.HasKey ("isGameWon") == false) 
			PlayerPrefs.SetInt ("isGameWon", 1);

		if (PlayerPrefs.HasKey ("skill") == false)
			PlayerPrefs.SetInt ("skill", 25);

		if (PlayerPrefs.HasKey ("GamesWon") == false)
			PlayerPrefs.SetInt ("GamesWon", 0);
		//OFFER!!!!!!!!!!!!!! Change it after a week youself
		if (PlayerPrefs.HasKey ("Godlies") == false)
			PlayerPrefs.SetInt ("Godlies", 100);

		if (PlayerPrefs.HasKey ("IsGodly") == false)
			PlayerPrefs.SetInt ("IsGodly", 0);

		if (PlayerPrefs.HasKey ("Lyner") == false) 
			PlayerPrefs.SetInt ("Lyner", 1);

		if (PlayerPrefs.HasKey ("DeCara") == false) 
			PlayerPrefs.SetInt ("DeCara", 0);

		if (PlayerPrefs.HasKey ("Bobman") == false) 
			PlayerPrefs.SetInt ("Bobman", 0);

		if (PlayerPrefs.HasKey ("Hoaxer") == false) 
			PlayerPrefs.SetInt ("Hoaxer", 0);

		if (PlayerPrefs.HasKey ("GarelioST") == false) 
			PlayerPrefs.SetInt ("GarelioST", 0);

		if (PlayerPrefs.HasKey ("ZetaX") == false) 
			PlayerPrefs.SetInt ("ZetaX", 0);

		if (PlayerPrefs.HasKey ("XpeedRX") == false) 
			PlayerPrefs.SetInt ("XpeedRX", 0);

		if (PlayerPrefs.HasKey ("car") == false) 
			PlayerPrefs.SetString ("car", "Lyner");

		if (PlayerPrefs.HasKey ("title") == false) 
			PlayerPrefs.SetString ("title", "Beginner");

		if (PlayerPrefs.HasKey ("LastReward") == false) {
			PlayerPrefs.SetInt ("DailyReward", 1);
		} else {
			lastReward = ulong.Parse (PlayerPrefs.GetString("LastReward"));
			ulong diff = ((ulong)System.DateTime.Now.Ticks - lastReward);
			diff = diff / System.TimeSpan.TicksPerSecond;
			if(diff > 86400)
				PlayerPrefs.SetInt ("DailyReward", 1);
		}

		//Always start the game with speaker ON
		PlayerPrefs.SetInt ("Speaker", 1);

		//Always show 'loyalsheep presents' when the game starts
		PlayerPrefs.SetInt ("loyalsheep", 1);
		
		StartCoroutine (LoadAsynchro ());
	}

	IEnumerator LoadAsynchro (){
		operation = SceneManager.LoadSceneAsync (1);
		operation.allowSceneActivation = false;

		while (progress != 1f && !operation.isDone){
			progress = Mathf.Clamp01 (operation.progress / 0.9f);
			slider.GetComponent<Slider>().value = progress;
			yield return null;
		}

		if (PlayerPrefs.HasKey ("nick") == false) {
			slider.SetActive (false);
			NickPanel.SetActive (true);
		} else {
			operation.allowSceneActivation = true;
		}
	}

	public void LockNick () {
		nick = InputNick.GetComponent<InputField>().text;
		if (nick.Length < 3) {
			InvalidText.SetActive (true);
			InputNick.GetComponent<InputField> ().text = "";
		} else {
			InvalidText.SetActive (false);
			NickPanel.SetActive (false);
			SelectCountry ();
			CountryGreeting.SetActive (true);
			Countries.SetActive (true);
			//PlayerPrefs.SetString ("nick", nick);
			//Instructions.SetActive (true);
		}
	}

	public void SelectCountry(){
		TextAsset data = Resources.Load<TextAsset> ("Flags");
		rows = data.text.Split (new char[] {'\n'});
		int i;

		List<string> names = new List<string> (){};

		for (i = 0; i < rows.Length; i++) 
			names.Add (rows[i].Split(new char[] {','})[0]);
		
		Countries.GetComponent<Dropdown> ().AddOptions (names);
	}

	public void OnCountrySelect () {
		Sprite flag;
		int x;
		string code;
		if (Countries.GetComponent<Dropdown> ().value != 0) {
			x = Countries.GetComponent<Dropdown> ().value - 1;

			country = rows [x].Split (new char[] { ',' }) [1];
			country = country.Replace ("\r", "").Replace ("\n", "");

			code = "FlagsImages/" + rows [x].Split (new char[] { ',' }) [1];
			code = code.Replace ("\r", "").Replace ("\n", "");

			flag = Resources.Load<Sprite> (code);
			Flag.GetComponent<Image> ().sprite = flag;

			Flag.SetActive (true);
			Tick.SetActive (true);
		} else {
			Flag.SetActive (false);
			Tick.SetActive (false);
		}
	}

	public void Proceed () {
		PlayerPrefs.SetString ("nick", nick);
		PlayerPrefs.SetString ("country", country);
		CountryGreeting.SetActive (false);
		Countries.SetActive (false);
		Flag.SetActive (false);
		Tick.SetActive (false);
		Instructions.SetActive (true);
	}

	public void LoadMenu () {
		operation.allowSceneActivation = true;
	}
}