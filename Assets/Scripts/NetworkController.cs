using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkController : NetworkBehaviour {
	NetworkManager NetMngr;
	public int connectedPlayers = 0;
	public GameObject controller;
	public GameObject opponent;
	public GameObject OpponentNick;
	public GameObject PersonalNick;
	public GameObject PersonalCar;
	public GameObject Personal_Car;
	public GameObject PersonalFlag;
	public GameObject OpponentCar;
	public GameObject Opponent_Car;
	public GameObject OpponentFlag;
	public GameObject Personal_Flag;
	public GameObject Opponent_Flag;
	public GameObject PersonalTitle;
	public GameObject OpponentTitle;
	public GameObject PersonalStatus;
	public GameObject OpponentStatus;
	public GameObject OppCollection;
	public GameObject Greeting;
	public GameObject HLine;
	public GameObject GreetPersonal;
	public GameObject GreetOpponent;
	public GameObject Search;
	public GameObject SearchText;
	public GameObject CountDown;
	public GameObject WinCounter;
	public GameObject Hotspot;
	public GameObject Balloons;
	public float oppProgress = 0f;
	public GameObject personal;
	public int isInit = 0;
	public int winStatus = 0;
	public int isGreetOpponentInit = 0;
	public int isHostnameInit = 0;
	public int isClientnameInit = 0;
	public int personalWins = 0;
	public int opponentWins = 0;
	public int num1;
	public int num2;
	public int num3;
	public int num4;
	public int num5;
	public Color c;
	public Color _c;
	public GameObject AudioManager;

	void Start () {
		NetMngr = GameObject.Find ("NetworkManager").GetComponent<Network_Manager> ();
		controller = GameObject.Find ("Controller");
		opponent = GameObject.Find ("Opponent");
		personal = GameObject.Find ("Personal");
		OpponentNick = GameObject.Find ("OpponentNick");
		PersonalNick = GameObject.Find ("PersonalNick");
		Greeting = GameObject.Find ("Greeting");
		HLine = GameObject.Find ("HLine");
		PersonalTitle = GameObject.Find ("GreetPersonalTitle");
		OpponentTitle = GameObject.Find ("GreetOpponentTitle");
		GreetPersonal = GameObject.Find ("GreetPersonal");
		GreetOpponent = GameObject.Find ("GreetOpponent");
		PersonalStatus = GameObject.Find ("PersonalStatus");
		OpponentStatus = GameObject.Find ("OpponentStatus");
		OppCollection = GameObject.Find ("OppCollection");
		Search = GameObject.Find ("Search");
		SearchText = GameObject.Find ("SearchText");
		CountDown = GameObject.Find ("CountDown");
		WinCounter = GameObject.Find ("WinCounter");
		PersonalCar = GameObject.Find ("PersonalCar");
		Personal_Car = GameObject.Find ("Personal_Car");
		PersonalFlag = GameObject.Find ("PersonalFlag");
		Personal_Flag = GameObject.Find ("Personal_Flag");
		OpponentCar = GameObject.Find ("OpponentCar");
		Opponent_Car = GameObject.Find ("Opponent_Car");
		OpponentFlag = GameObject.Find ("OpponentFlag");
		Opponent_Flag = GameObject.Find ("Opponent_Flag");
		AudioManager = GameObject.Find ("AudioManager");
		Balloons = GameObject.Find ("Balloons");

		Greeting.SetActive (true);
		
		if (isLocalPlayer) {
			OpponentFlag.SetActive (false);
			PersonalFlag.SetActive (false);
		}

		c.r = 1f;
		c.a = 1f;
		c.g = 1f;
		c.b = 1f;

		_c.r = 1f;
		_c.a = 0f;
		_c.g = 1f;
		_c.b = 1f;

		PersonalNick.GetComponent<Text>().text = PlayerPrefs.GetString ("nick");
		GreetPersonal.GetComponent<Text>().text = PlayerPrefs.GetString ("nick");
		PersonalTitle.GetComponent<Text>().text = PlayerPrefs.GetString ("title");
		PersonalCar.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Cars/" + PlayerPrefs.GetString ("car"));
		PersonalFlag.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("FlagsImages/" + PlayerPrefs.GetString ("country"));
		PersonalCar.GetComponent<Image> ().color = c;
		PersonalFlag.GetComponent<Image> ().color = c;
		PersonalCar.SetActive (true);
		PersonalFlag.SetActive (true);
		Personal_Flag.GetComponent<Image> ().sprite = PersonalFlag.GetComponent<Image> ().sprite;
		Personal_Car.GetComponent<Image> ().sprite = PersonalCar.GetComponent<Image> ().sprite;
		//Initialize the nums only on the server, pure clients' nums will always remain 0
		if (isLocalPlayer && isServer)
			controller.GetComponent<Controller> ().amIServer = true;
		else if(isLocalPlayer && !isServer)
			controller.GetComponent<Controller> ().amIServer = false;
	}

	void InitializeNums(){
		if (isLocalPlayer && isServer) {
			num1 = Random.Range (1000000, 10000000);
			num2 = Random.Range (1000000, 10000000);
			num3 = Random.Range (1000000, 10000000);
			num4 = Random.Range (1000000, 10000000);
			num5 = Random.Range (1000000, 10000000);
			Debug.Log ("Initialized nums coz I'm a Server!");
		}
	}

	//Call the RPC after a delay, for initializing the nums on client and host
	[Command]
	public void CmdGen () {
		Debug.Log ("Calling RPC");
		RpcGen (num1, num2, num3, num4, num5);
	}

	//for initializing nums textfields of all clients
	[ClientRpc]
	public void RpcGen(int num1, int num2, int num3, int num4, int num5){
		controller.GetComponent<Controller> ().nums [0] = num1;
		controller.GetComponent<Controller> ().nums [1] = num2;
		controller.GetComponent<Controller> ().nums [2] = num3;
		controller.GetComponent<Controller> ().nums [3] = num4;
		controller.GetComponent<Controller> ().nums [4] = num5;

		controller.GetComponent<Controller> ().setNumber ();
	}

	IEnumerator DelayUpdateHostNickname (string nick, string flag, string car, string title) {
		yield return new WaitForSeconds(2);
		CmdUpdateHostNickname (nick, flag, car, title);
	}

	[Command]
	public void CmdUpdateHostNickname (string nick, string flag, string car, string title) {
		RpcUpdateHostNickname (nick, flag, car, title);
	}

	[ClientRpc]
	public void RpcUpdateHostNickname (string nick, string flag, string car, string title){
		if (!isLocalPlayer) {
			OpponentNick.GetComponent<Text> ().text = nick;
			OpponentTitle.GetComponent<Text> ().text = title;
			Opponent_Car.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Cars/" + car);
			Opponent_Flag.GetComponent<Image>().sprite = Resources.Load<Sprite> ("FlagsImages/" + flag);
		}
	}

	IEnumerator DelayUpdateClientNickname (string nick, string flag, string car, string title) {
		yield return new WaitForSeconds(2);
		CmdUpdateClientNickname (nick, flag, car, title);
	}

	[Command]
	public void CmdUpdateClientNickname (string nick, string flag, string car, string title) {
		RpcUpdateClientNickname (nick, flag, car, title);
	}

	[ClientRpc]
	public void RpcUpdateClientNickname (string nick, string flag, string car, string title){
		if (!isLocalPlayer) {
			OpponentNick.GetComponent<Text> ().text = nick;
			OpponentTitle.GetComponent<Text> ().text = title;
			Opponent_Car.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Cars/" + car);
			Opponent_Flag.GetComponent<Image>().sprite = Resources.Load<Sprite> ("FlagsImages/" + flag);
		}
	}

	public void SetGreetOpponent () {
		Debug.Log ("I'm in the greet");
		if (PlayerPrefs.GetInt ("Friend") == 1)
			controller.GetComponent<Controller> ().DisableHotspot ();
		HLine.SetActive (false);
		Search.SetActive (false);
		SearchText.SetActive (false);
		GreetOpponent.GetComponent<Text> ().text = OpponentNick.GetComponent<Text> ().text;
		OpponentCar.GetComponent<Image> ().sprite = Opponent_Car.GetComponent<Image> ().sprite;
		OpponentFlag.GetComponent<Image> ().sprite = Opponent_Flag.GetComponent<Image> ().sprite;
		OpponentCar.GetComponent<Image> ().color = c;
		OpponentFlag.GetComponent<Image> ().color = c;
		OpponentFlag.SetActive (true);
		OpponentCar.SetActive (true);
		OppCollection.GetComponent<Animation> ().Play ("OppCollection");
		if (isLocalPlayer && isServer && NetMngr.numPlayers == 1) 
			StartCoroutine(HandleClientDisconnection ());
		else
			StartCoroutine (InitCountDown ());
	}

	IEnumerator InitCountDown () {
		yield return new WaitForSeconds (1);
		Debug.Log ("Countdown executing");
		PersonalStatus.GetComponent<Text> ().text = "";
		OpponentStatus.GetComponent<Text> ().text = "";
		GameObject.Find ("AudioManager").GetComponent<Audio> ().PlayCountDown ();
		CountDown.GetComponent<Text>().text = "3";
		yield return new WaitForSeconds (1);
		GameObject.Find ("AudioManager").GetComponent<Audio> ().PlayCountDown ();
		CountDown.GetComponent<Text>().text = "2";
		yield return new WaitForSeconds (1);
		GameObject.Find ("AudioManager").GetComponent<Audio> ().PlayCountDown ();
		CountDown.GetComponent<Text>().text = "1";
		yield return new WaitForSeconds (1);
		CountDown.GetComponent<Text>().text = "";
		if (isLocalPlayer && isServer && NetMngr.numPlayers == 1) {
			StartCoroutine (HandleClientDisconnection ());
		} else {
			GameObject.Find ("AudioManager").GetComponent<Audio> ().StartCar ();
			Greeting.SetActive (false);
		}
	}

	void Update () {
		if (isGreetOpponentInit == 0 && OpponentNick.GetComponent<Text> ().text != "" && isLocalPlayer) {
			isGreetOpponentInit = 1;
			SetGreetOpponent ();
		}

		if (isHostnameInit == 1 && NetMngr.numPlayers == 1)
			isHostnameInit = 0;
		
		if (isHostnameInit == 0 && isLocalPlayer && isServer && NetMngr.numPlayers == 2) {
			isHostnameInit = 1;
			StartCoroutine(DelayUpdateHostNickname (PersonalNick.GetComponent<Text>().text, PlayerPrefs.GetString ("country"), PlayerPrefs.GetString("car"), PlayerPrefs.GetString("title")));
		}
		if (isClientnameInit == 0 && isLocalPlayer && !isServer) {
			isClientnameInit = 1;
			StartCoroutine(DelayUpdateClientNickname (PersonalNick.GetComponent<Text>().text, PlayerPrefs.GetString ("country"), PlayerPrefs.GetString("car"), PlayerPrefs.GetString("title")));
		}
		//run only on server
		if (isLocalPlayer && isServer) {
			if (isInit == 1 && NetMngr.numPlayers == 1) {
				isInit = 0;
				StartCoroutine(HandleClientDisconnection ());
			}
		}
		if (isLocalPlayer && isServer) {
			if (isInit == 0 && NetMngr.numPlayers == 2) {
				Debug.Log ("Client Connected");
				InitializeNums ();
				StartCoroutine ("DelayedRPC");
				isInit = 1;
			}
			//Update progress of the host on the pure client
			if (controller.GetComponent<Controller>().round == 1 && oppProgress == 0) {
				oppProgress = 0.2f;
				CmdFromServer (oppProgress);
			} else if (controller.GetComponent<Controller>().round == 2 && oppProgress == 0.2f) {
				oppProgress = 0.4f;
				CmdFromServer (oppProgress);
			} else if (controller.GetComponent<Controller>().round == 3 && oppProgress == 0.4f) {
				oppProgress = 0.6f;
				CmdFromServer (oppProgress);
			} else if (controller.GetComponent<Controller>().round == 4 && oppProgress == 0.6f) {
				oppProgress = 0.8f;
				CmdFromServer (oppProgress);
			} else if (controller.GetComponent<Controller>().round == 5 && oppProgress == 0.8f) {
				oppProgress = 1.0f;
				CmdFromServer (oppProgress);
			}
		} 
		else if (isLocalPlayer && !isServer) {
			if (controller.GetComponent<Controller>().round == 1 && oppProgress == 0) {
				oppProgress = 0.2f;
				CmdFromClient (oppProgress);
			} else if (controller.GetComponent<Controller>().round == 2 && oppProgress == 0.2f) {
				oppProgress = 0.4f;
				CmdFromClient (oppProgress);
			} else if (controller.GetComponent<Controller>().round == 3 && oppProgress == 0.4f) {
				oppProgress = 0.6f;
				CmdFromClient (oppProgress);
			} else if (controller.GetComponent<Controller>().round == 4 && oppProgress == 0.6f) {
				oppProgress = 0.8f;
				CmdFromClient (oppProgress);
			} else if (controller.GetComponent<Controller>().round == 5 && oppProgress == 0.8f) {
				oppProgress = 1.0f;
				CmdFromClient (oppProgress);
			}
		}
		//Decide who won
		if(isLocalPlayer){
			if (winStatus == 0 && (personal.GetComponent<Slider> ().value == 1.0f || opponent.GetComponent<Slider> ().value == 1.0f)) {
				winStatus = 1;
				controller.GetComponent<Controller> ().PauseDisable ();
				//HLine.SetActive (true);
				controller.GetComponent<Controller> ().EnableRematch ();
				if (personal.GetComponent<Slider> ().value == 1.0f)
					PersonalWins ();
				else
					OpponentWins ();
			}
		}
		if(isLocalPlayer && controller.GetComponent<Controller> ().isRematch == 1 && !isServer) {
			controller.GetComponent<Controller> ().isRematch = 0;
			PersonalStatus.GetComponent<Text>().text = "I want a rematch!";
			CmdClientRematch ();
		}

		if(isLocalPlayer && controller.GetComponent<Controller> ().isRematch == 1 && isServer) {
			controller.GetComponent<Controller> ().isRematch = 0;
			PersonalStatus.GetComponent<Text>().text = "I want a rematch!";
			CmdServerRematch ();
		}

		if (isLocalPlayer && PersonalStatus.GetComponent<Text> ().text == "I want a rematch!" && OpponentStatus.GetComponent<Text> ().text == "I want a rematch!" && winStatus == 1) {
			if (isServer) {
				InitializeNums ();
				CmdGen ();
			}
			winStatus = 0;
			oppProgress = 0;
			
			controller.GetComponent<Controller> ().DisableRematch ();
			//PersonalStatus.GetComponent<Text> ().text = "";
			//OpponentStatus.GetComponent<Text> ().text = "";
			HLine.SetActive (false);
			//CountDown.SetActive (true);
			StartCoroutine (InitCountDown ());
		}
	}

	public void PersonalWins(){
		PersonalStatus.GetComponent<Text>().text = "You won!"; 
		OpponentStatus.GetComponent<Text>().text = "Loser!"; 
		personalWins++;
		WinCounter.GetComponent<Text> ().text = personalWins.ToString () + "-" + opponentWins.ToString ();
		if (PlayerPrefs.GetInt ("Friend") == 0) {
			PlayerPrefs.SetInt ("GamesWon", PlayerPrefs.GetInt ("GamesWon") + 1);
			PlayGames.AddScoreToLeaderboard (GPGSIds.leaderboard_global_ranking, PlayerPrefs.GetInt ("GamesWon"));
			if(PlayerPrefs.GetInt ("GamesWon") > 2 && PlayerPrefs.GetInt ("rate") == 0)
				PlayerPrefs.SetInt ("isLoadScene", 0);
		}
		Greeting.SetActive (true);
		Balloons.GetComponent<ParticleSystem> ().Play ();
		//Balloons.GetComponent<ParticleSystem> ().emission.enabled = true;
	}

	public void OpponentWins(){
		PersonalStatus.GetComponent<Text>().text = "You lose!"; 
		OpponentStatus.GetComponent<Text>().text = "Winner!";
		opponentWins++;
		WinCounter.GetComponent<Text> ().text = personalWins.ToString () + "-" + opponentWins.ToString ();
		//Rematch.SetActive (true);
		Greeting.SetActive (true);
	}

	IEnumerator DelayedRPC () {
		yield return new WaitForSeconds(2);
		CmdGen ();
	}

	[Command]
	public void CmdServerRematch () {
		RpcServerRematch();
	}

	[ClientRpc]
	public void RpcServerRematch () {
		if (!isLocalPlayer)
			OpponentStatus.GetComponent<Text>().text = "I want a rematch!";
	}

	[Command]
	public void CmdClientRematch () {
		RpcClientRematch();
	}

	[ClientRpc]
	public void RpcClientRematch () {
		if (!isLocalPlayer)
			OpponentStatus.GetComponent<Text>().text = "I want a rematch!";
	}

	[Command]
	public void CmdFromServer (float val) {
		RpcFromServer(val);
	}

	[ClientRpc]
	public void RpcFromServer (float val) {
		if (!isLocalPlayer) {
			Debug.Log ("Changing host's progress");
			Opponent_Car.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (val * controller.GetComponent<Controller>().width, controller.GetComponent<Controller>().posY_);
			opponent.GetComponent<Slider> ().value = val;
		}
	}

	[Command]
	public void CmdFromClient (float val) {
		RpcFromClient (val);
	}

	[ClientRpc]
	public void RpcFromClient (float val) {
		if (!isLocalPlayer) {
			Debug.Log ("Changing clients's progress");
			Opponent_Car.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (val * controller.GetComponent<Controller>().width, controller.GetComponent<Controller>().posY_);
			opponent.GetComponent<Slider> ().value = val;
		}
	}

	IEnumerator HandleClientDisconnection () {
		if (PlayerPrefs.GetInt ("Friend") == 1)
			controller.GetComponent<Controller> ().EnableHotspot ();

		controller.GetComponent<Controller> ().PauseDisable ();
		controller.GetComponent<Controller> ().DisableRematch ();
		OpponentNick.GetComponent<Text> ().text = "";
		Greeting.SetActive (true);
		yield return new WaitForSeconds (0.5f);
		OppCollection.GetComponent<Animation> ().Play ("OppCollectionEnd");
		yield return new WaitForSeconds (0.2f);
		GreetOpponent.GetComponent<Text> ().text = "";
		OpponentFlag.GetComponent<Image> ().color = _c;
		OpponentStatus.GetComponent<Text>().text = "";
		PersonalStatus.GetComponent<Text>().text = "";
		controller.GetComponent<Controller> ().HandleClientDisconnection ();
		HLine.SetActive (true);
		Search.SetActive (true);
		SearchText.SetActive (true);
		isGreetOpponentInit = 0;
		winStatus = 0;
		oppProgress = 0;
		personalWins = 0;
		opponentWins = 0;
		WinCounter.GetComponent<Text> ().text = "";
	}
}