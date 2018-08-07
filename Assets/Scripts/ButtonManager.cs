using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
	public GameObject leaderboard;
	public GameObject loyalsheep;
	public GameObject tutorial;
	public GameObject Player;
	public GameObject Profile;
	public GameObject PlayerName;
	public GameObject PlayerTitle;
	public GameObject Flag;
	public GameObject Matter;
	public GameObject Shop;
	public GameObject skill;
	public GameObject friend;
	public GameObject random;
	public GameObject ExitDialog;
	public GameObject CreditsDialog;
	public GameObject Instructions;
	public GameObject NetManager;
	public GameObject SetQuote;
	public GameObject SetStatus;
	public GameObject NoInternet;
	public GameObject HostLeft;
	public GameObject Title;
	public GameObject Speaker;
	public GameObject ForLoyalsheep;
	public GameObject DailyRewardBlocker;
	public GameObject DailyRewardPanel;
	public GameObject RatePanel;
	public GameObject GFinger;
	public GameObject GFingerlabel;
	public GameObject Car;
	public GameObject ShopPanel;

	public Button LynerButton;
	public Button DeCaraButton;
	public Button BobmanButton;
	public Button HoaxerButton;
	public Button GarelioSTButton;
	public Button ZetaXButton;
	public Button XpeedRXButton;

	public Text LynerText;
	public Text DeCaraText;
	public Text BobmanText;
	public Text HoaxerText;
	public Text GarelioSTText;
	public Text ZetaXText;
	public Text XpeedRXText;

	public void Start(){
		NetManager = GameObject.Find ("NetworkManager");
		PlayerPrefs.SetInt ("Friend", 0);
		PlayerName.GetComponent<Text>().text = PlayerPrefs.GetString ("nick");
		Flag.GetComponent<Image>().sprite = Resources.Load<Sprite> ("FlagsImages/" + PlayerPrefs.GetString ("country"));
		Car.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Cars/" + PlayerPrefs.GetString ("car"));
		PlayerTitle.GetComponent<Text> ().text = PlayerPrefs.GetString ("title");
		Matter.GetComponent<Text> ().text = "Games won : " + PlayerPrefs.GetInt ("GamesWon") + "\nGodly Fingers : " + PlayerPrefs.GetInt ("Godlies");
		if (GFinger.GetComponent<Toggle> ().isOn == true && PlayerPrefs.GetInt ("Godlies") == 0){
			GFinger.GetComponent<Toggle> ().isOn = false;
			PlayerPrefs.SetInt ("IsGodly", 0);
			GFingerlabel.GetComponent<Text> ().text = "Disabled";
		}
		if (PlayerPrefs.GetInt ("DailyReward") == 1) {
			DailyRewardBlocker.SetActive (true);
			StartCoroutine (PullGodly ());
		}
		if (PlayerPrefs.GetInt ("isLoadScene") == 0 && PlayerPrefs.GetInt ("rate") == 0) {
			PlayerPrefs.SetInt ("isLoadScene", 1);
			DailyRewardBlocker.SetActive (true);
			StartCoroutine (PullRate ());
		}
	}

	public void RateOK () {
		Application.OpenURL ("market://details?id=com.loyalsheep.FastestDialler");
		PlayerPrefs.SetInt ("rate", 1);
		Rate ();
	}

	public void RateNO () {
		PlayerPrefs.SetInt ("rate", 1);
		Rate ();
	}

	IEnumerator PullRate() {
		yield return new WaitForSeconds (0.5f);	
		Player.SetActive (false);
		Shop.SetActive (false);
		Speaker.SetActive (false);
		leaderboard.SetActive (false);
		tutorial.SetActive (false);
		loyalsheep.SetActive (false);
		Title.SetActive (false);
		skill.SetActive (false);
		friend.SetActive (false);
		random.SetActive (false);
		RatePanel.SetActive (true);
		RatePanel.GetComponent<Animation> ().Play ("LeftSide");
	}

	public void Rate () {
		GameObject.Find ("AudioManager").GetComponent<Audio> ().PlayClick ();
		StartCoroutine (PushRate());
	}

	IEnumerator PushRate() {
		RatePanel.GetComponent<Animation> ().Play ("LeftSide_");
		yield return new WaitForSeconds (0.3f);
		RatePanel.SetActive (false);
		DailyRewardBlocker.SetActive (false);
		Player.SetActive (true);
		Shop.SetActive (true);
		Speaker.SetActive (true);
		leaderboard.SetActive (true);
		tutorial.SetActive (true);
		loyalsheep.SetActive (true);
		Title.SetActive (true);
		skill.SetActive (true);
		friend.SetActive (true);
		random.SetActive (true);
	}

	/// <summary>
	/// Exit overlaps!!!!!! Please call functions for Leaderboard and Shop too!!!!!!!!!!!!!!!!!!!!!
	/// </summary>
	public void Update () {
		if (ForLoyalsheep.activeSelf == true)
			return;
		if (Input.GetKeyDown (KeyCode.Escape)){
			if (CreditsDialog.activeSelf == true)
				Credits ();
			else if (Profile.activeSelf == true)
				ProfileButton ();
			else if (Instructions.activeSelf == true)
				Tutorial ();
			else if (DailyRewardPanel.activeSelf == true)
				Godly ();
			else if (RatePanel.activeSelf == true)
				Rate ();
			else if (ShopPanel.activeSelf == true)
				ShopButton ();
			else
				Exit ();
		}
	}

	IEnumerator PullGodly() {
		yield return new WaitForSeconds (9.5f);	
		Player.SetActive (false);
		Shop.SetActive (false);
		Speaker.SetActive (false);
		leaderboard.SetActive (false);
		tutorial.SetActive (false);
		loyalsheep.SetActive (false);
		Title.SetActive (false);
		skill.SetActive (false);
		friend.SetActive (false);
		random.SetActive (false);
		PlayerPrefs.SetInt ("Godlies", PlayerPrefs.GetInt("Godlies") + 2);
		PlayerPrefs.SetInt ("DailyReward", 0);
		PlayerPrefs.SetString ("LastReward", System.DateTime.Now.Ticks.ToString());
		DailyRewardPanel.SetActive (true);
		DailyRewardPanel.GetComponent<Animation> ().Play ("LeftSide");
	}

	public void Godly () {
		GameObject.Find ("AudioManager").GetComponent<Audio> ().PlayClick ();
		StartCoroutine (PushGodly());
	}

	IEnumerator PushGodly() {
		DailyRewardPanel.GetComponent<Animation> ().Play ("LeftSide_");
		yield return new WaitForSeconds (0.3f);
		DailyRewardPanel.SetActive (false);
		DailyRewardBlocker.SetActive (false);
		Player.SetActive (true);
		Shop.SetActive (true);
		Speaker.SetActive (true);
		leaderboard.SetActive (true);
		tutorial.SetActive (true);
		loyalsheep.SetActive (true);
		Title.SetActive (true);
		skill.SetActive (true);
		friend.SetActive (true);
		random.SetActive (true);
	}

	public void Exit(){
		if (ExitDialog.activeSelf == false) {
			GameObject.Find ("AudioManager").GetComponent<Audio> ().PlayClick ();
			Player.SetActive (false);
			Shop.SetActive (false);
			Speaker.SetActive (false);
			leaderboard.SetActive (false);
			tutorial.SetActive (false);
			loyalsheep.SetActive (false);
			skill.SetActive (false);
			friend.SetActive (false);
			random.SetActive (false);
			ExitDialog.SetActive (true);
			StartCoroutine (PullExit ());
		} else { //ExitNO
			GameObject.Find ("AudioManager").GetComponent<Audio> ().PlayClick ();
			StartCoroutine (PushExit ());
		}
	}

	public void ExitYes(){
		Application.Quit();
	}

	IEnumerator PullExit() {
		ExitDialog.GetComponent<Animation> ().Play ("ExitDialog");
		yield return null;
	}

	IEnumerator PushExit() {
		ExitDialog.GetComponent<Animation> ().Play ("ExitDialog_");
		yield return new WaitForSeconds (0.3f);
		ExitDialog.SetActive (false);
		Player.SetActive (true);
		Shop.SetActive (true);
		Speaker.SetActive (true);
		leaderboard.SetActive (true);
		tutorial.SetActive (true);
		loyalsheep.SetActive (true);
		skill.SetActive (true);
		friend.SetActive (true);
		random.SetActive (true);
	}

	public void Credits(){
		if (CreditsDialog.activeSelf == false) {
			GameObject.Find ("AudioManager").GetComponent<Audio> ().PlayClick ();
			Speaker.SetActive (false);
			leaderboard.SetActive (false);
			tutorial.SetActive (false);
			Player.SetActive (false);
			Shop.SetActive (false);
			skill.SetActive (false);
			friend.SetActive (false);
			random.SetActive (false);
			CreditsDialog.SetActive (true);
			StartCoroutine (PullCredits ());
		} else {
			GameObject.Find ("AudioManager").GetComponent<Audio> ().PlayClick ();
			StartCoroutine (PushCredits ());
		}
	}

	IEnumerator PullCredits() {
		CreditsDialog.GetComponent<Animation> ().Play ("RightSide");
		yield return null;
	}

	IEnumerator PushCredits() {
		CreditsDialog.GetComponent<Animation> ().Play ("RightSide_");
		yield return new WaitForSeconds (0.3f);
		CreditsDialog.SetActive (false);
		Speaker.SetActive (true);
		leaderboard.SetActive (true);
		tutorial.SetActive (true);
		Player.SetActive (true);
		Shop.SetActive (true);
		skill.SetActive (true);
		friend.SetActive (true);
		random.SetActive (true);
	}

	public void LeaderBoard(){
		PlayGames.ShowLeaderboardsUI ();
	}

	public void ProfileButton(){
		if (Profile.activeSelf == false) {
			GameObject.Find ("AudioManager").GetComponent<Audio> ().PlayClick ();
			Speaker.SetActive (false);
			tutorial.SetActive (false);
			Title.SetActive (false);
			leaderboard.SetActive (false);
			loyalsheep.SetActive (false);
			Shop.SetActive (false);
			skill.SetActive (false);
			friend.SetActive (false);
			random.SetActive (false);
			PlayerTitle.GetComponent<Text> ().text = PlayerPrefs.GetString ("title");
			Car.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Cars/" + PlayerPrefs.GetString ("car"));
			Matter.GetComponent<Text> ().text = "Games won : " + PlayerPrefs.GetInt ("GamesWon") + "\nGodly Fingers : " + PlayerPrefs.GetInt ("Godlies");

			if (GFinger.GetComponent<Toggle> ().isOn == true && PlayerPrefs.GetInt ("Godlies") == 0){
				GFinger.GetComponent<Toggle> ().isOn = false;
				PlayerPrefs.SetInt ("IsGodly", 0);
				GFingerlabel.GetComponent<Text> ().text = "Disabled";
				return;
			}

			if (PlayerPrefs.GetInt ("IsGodly") == 1) {
				GFinger.GetComponent<Toggle> ().isOn = true;
				GFingerlabel.GetComponent<Text> ().text = "Enabled";
			} else {
				GFinger.GetComponent<Toggle> ().isOn = false;
				GFingerlabel.GetComponent<Text> ().text = "Disabled";
			}
			Profile.SetActive (true);
			StartCoroutine (PullProfile ());
		} else {
			GameObject.Find ("AudioManager").GetComponent<Audio> ().PlayClick ();
			StartCoroutine (PushProfile());
		}
	}


	IEnumerator PullProfile() {
		Profile.GetComponent<Animation> ().Play ("LeftSide");
		yield return null;
	}

	IEnumerator PushProfile() {
		Profile.GetComponent<Animation> ().Play ("LeftSide_");
		yield return new WaitForSeconds (0.3f);
		Profile.SetActive (false);
		Speaker.SetActive (true);
		Title.SetActive (true);
		leaderboard.SetActive (true);
		loyalsheep.SetActive (true);
		Player.SetActive (true);
		Shop.SetActive (true);
		tutorial.SetActive (true);
		skill.SetActive (true);
		friend.SetActive (true);
		random.SetActive (true);
	}

	public void GodlyValue () {
		if (GFinger.GetComponent<Toggle> ().isOn == true && PlayerPrefs.GetInt ("Godlies") == 0){
			GFinger.GetComponent<Toggle> ().isOn = false;
			PlayerPrefs.SetInt ("IsGodly", 0);
			GFingerlabel.GetComponent<Text> ().text = "Disabled";
			return;
		}
		if (GFinger.GetComponent<Toggle> ().isOn == true) {
			PlayerPrefs.SetInt ("IsGodly", 1);
			GFingerlabel.GetComponent<Text> ().text = "Enabled";
		} else {
			PlayerPrefs.SetInt ("IsGodly", 0);
			GFingerlabel.GetComponent<Text> ().text = "Disabled";
		}
	}

	public void Tutorial(){
		if (Instructions.activeSelf == false) {
			GameObject.Find ("AudioManager").GetComponent<Audio> ().PlayClick ();
			Speaker.SetActive (false);
			Title.SetActive (false);
			leaderboard.SetActive (false);
			loyalsheep.SetActive (false);
			Player.SetActive (false);
			Shop.SetActive (false);
			skill.SetActive (false);
			friend.SetActive (false);
			random.SetActive (false);
			Instructions.SetActive (true);
			StartCoroutine (PullTutorial ());
		} else {
			GameObject.Find ("AudioManager").GetComponent<Audio> ().PlayClick ();
			StartCoroutine (PushTutorial ());
		}
	}

	IEnumerator PullTutorial() {
		Instructions.GetComponent<Animation> ().Play ("Instructions");
		yield return null;
	}

	IEnumerator PushTutorial() {
		Instructions.GetComponent<Animation> ().Play ("Instructions_");
		yield return new WaitForSeconds (0.3f);
		Instructions.SetActive (false);
		Speaker.SetActive (true);
		Title.SetActive (true);
		leaderboard.SetActive (true);
		loyalsheep.SetActive (true);
		Player.SetActive (true);
		Shop.SetActive (true);
		skill.SetActive (true);
		friend.SetActive (true);
		random.SetActive (true);
	}

	public void Skill () {
		GameObject.Find ("AudioManager").GetComponent<Audio> ().PlayClick ();
		SceneManager.LoadScene ("SkillGame");
	}

	public void Friend () {
		GameObject.Find ("AudioManager").GetComponent<Audio> ().PlayClick ();
		PlayerPrefs.SetInt ("Friend", 1);
		NetManager.GetComponent<Network_Manager> ().Friend ();
	}

	public void Random () {
		GameObject.Find ("AudioManager").GetComponent<Audio> ().PlayClick ();
		NetManager.GetComponent<Network_Manager> ().Random ();
	}

	public void EnableSetQuote () {
		SetQuote.SetActive (true);
	}

	public void EnableNoInternete () {
		NoInternet.SetActive (true);
	}

	public void EnableHostLeft () {
		HostLeft.SetActive (true);
	}

	public void SomethingWentWrong () {
		SetStatus.GetComponent<Text> ().text = "Uh oh!\nSomething went wrong.";
		StartCoroutine (Restart ());
	}

	IEnumerator Restart () {
		yield return new WaitForSeconds (3f);
		Destroy (NetManager);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		yield return null;
	}

	public void MenuRestart () {
		GameObject.Find ("AudioManager").GetComponent<Audio> ().PlayClick ();
		Destroy (NetManager);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void ShopButton () {
		if (ShopPanel.activeSelf == false) {
			GameObject.Find ("AudioManager").GetComponent<Audio> ().PlayClick ();
			Speaker.SetActive (false);
			tutorial.SetActive (false);
			Title.SetActive (false);
			leaderboard.SetActive (false);
			loyalsheep.SetActive (false);
			Shop.SetActive (false);
			skill.SetActive (false);
			friend.SetActive (false);
			random.SetActive (false);
			Player.SetActive (false);
			ShopPanel.SetActive (true);
			StartCoroutine (PullShopPanel ());
		} else {
			GameObject.Find ("AudioManager").GetComponent<Audio> ().PlayClick ();
			StartCoroutine (PushShopPanel ());
		}
	}

	IEnumerator PullShopPanel() {
		PrepareShopItems ();
		ShopPanel.GetComponent<Animation> ().Play ("BottomSide");
		yield return null;
	}

	IEnumerator PushShopPanel() {
		ShopPanel.GetComponent<Animation> ().Play ("BottomSide_");
		yield return new WaitForSeconds(0.3f);
		Speaker.SetActive (true);
		tutorial.SetActive (true);
		Title.SetActive (true);
		leaderboard.SetActive (true);
		loyalsheep.SetActive (true);
		Shop.SetActive (true);
		skill.SetActive (true);
		friend.SetActive (true);
		random.SetActive (true);
		Player.SetActive (true);
		ShopPanel.SetActive (false);
	}

	public void PrepareShopItems () {
		if (PlayerPrefs.GetInt ("Lyner") == 1) {
			SetUse (LynerButton, LynerText);
			if (PlayerPrefs.GetString ("car") == "Lyner")
				SetUsing (LynerButton, LynerText);
		}

		if (PlayerPrefs.GetInt ("DeCara") == 1) {
			SetUse (DeCaraButton, DeCaraText);
			if (PlayerPrefs.GetString ("car") == "DeCara")
				SetUsing (DeCaraButton, DeCaraText);
		}

		if (PlayerPrefs.GetInt ("Bobman") == 1) {
			SetUse (BobmanButton, BobmanText);
			if (PlayerPrefs.GetString ("car") == "Bobman")
				SetUsing (BobmanButton, BobmanText);
		}

		if (PlayerPrefs.GetInt ("Hoaxer") == 1) {
			SetUse (HoaxerButton, HoaxerText);
			if (PlayerPrefs.GetString ("car") == "Hoaxer")
				SetUsing (HoaxerButton, HoaxerText);
		}

		if (PlayerPrefs.GetInt ("GarelioST") == 1) {
			SetUse (GarelioSTButton, GarelioSTText);
			if (PlayerPrefs.GetString ("car") == "GarelioST")
				SetUsing (GarelioSTButton, GarelioSTText);
		}

		if (PlayerPrefs.GetInt ("ZetaX") == 1) {
			SetUse (ZetaXButton, ZetaXText);
			if (PlayerPrefs.GetString ("car") == "ZetaX")
				SetUsing (ZetaXButton, ZetaXText);
		}

		if (PlayerPrefs.GetInt ("XpeedRX") == 1) {
			SetUse (XpeedRXButton, XpeedRXText);
			if (PlayerPrefs.GetString ("car") == "XpeedRX")
				SetUsing (XpeedRXButton, XpeedRXText);
		}
	}

	public void SetUse(Button b, Text t){
		t.text = "Use";
		b.GetComponent<Image> ().color = new Color32 (72, 114, 234, 255);
	}

	public void SetUsing(Button b, Text t) {
		t.text = "Using";
		b.GetComponent<Image> ().color = new Color32 (64, 169, 50, 255);
	}
}
