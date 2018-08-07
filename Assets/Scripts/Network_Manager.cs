using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;

public class Network_Manager : NetworkManager {
	//List<string> roomList = new List<string> ();
	public MatchInfo hosted_room_match;
	private uint roomSize = 2;
	private string roomName = "default";
	private NetworkDiscovery serverNetwork;
	private TestPlayerClient clientNetwork;
	public bool serverStarted = false;
	public bool hostDiscovered = false;
	public bool clientDisconnected = false;
	public bool serverDisconnected = false;
	//public GameObject SetQuote;
	//public GameObject SetStatus;
	//public GameObject NoInternet;
	public string url = "https://www.google.com";

	public void Start () {
		//clientNetwork = gameObject.AddComponent<TestPlayerClient> ();
		if(NetworkManager.singleton.matchMaker == null)
			NetworkManager.singleton.StartMatchMaker();
	}

	IEnumerator CheckServer () {
		clientNetwork = gameObject.AddComponent<TestPlayerClient> ();
		Debug.Log ("Checking for server");
		yield return new WaitForSeconds(1f);
		hostDiscovered = clientNetwork.hostDiscovered;

		if(clientNetwork != null && !hostDiscovered) {
			Debug.Log ("AWWW! Tragedy! Starting as Host");
			clientNetwork.StopBroadcast ();
			Destroy (clientNetwork);
			yield return null;
		}
		Debug.Log ("The Server is " + serverStarted);
		if (!hostDiscovered && !serverStarted) {
			Debug.Log ("No Server found. Starting as Host");
			NetworkServer.Reset ();
			NetworkManager.singleton.StartHost ();
			serverNetwork = gameObject.AddComponent<NetworkDiscovery> ();
			serverNetwork.useNetworkManager = true;
			serverNetwork.GetComponent<NetworkDiscovery> ().showGUI = false;
			serverNetwork.Initialize ();
			serverNetwork.StartAsServer ();
			serverStarted = true;
		}
	}

	public void Friend () {
		if (Network.peerType == NetworkPeerType.Disconnected) { //Not connected
			//Connecting to the server
			StartCoroutine ("CheckServer");
			//NetworkManager.singleton.StartClient();
			//NetworkManager.singleton.StartHost();
		}
	}

	IEnumerator MatchCreator () {
		yield return new WaitForSeconds(5);
		NetworkManager.singleton.matchMaker.ListMatches(0, 5, "", true, 0, 0, NetworkManager.singleton.OnMatchList);
	}

	IEnumerator Signal () {
		yield return new WaitForSeconds(5f);
		if (SceneManager.GetActiveScene ().buildIndex == 1)
			GameObject.Find ("ButtonManager").GetComponent<ButtonManager> ().SomethingWentWrong ();
	}
		
	IEnumerator checkInternet(){
		WWW www = new WWW (url);
		yield return www;
		if (www.isDone && www.bytesDownloaded > 0) {
			Debug.Log ("You are connected to the internet");
			GameObject.Find("ButtonManager").GetComponent<ButtonManager>().EnableSetQuote();
			NetworkServer.Reset ();

			if(NetworkManager.singleton.matchMaker == null)
				NetworkManager.singleton.StartMatchMaker();
			
			StartCoroutine (MatchCreator ());
		} else { 
			Debug.Log ("No Internet connection");
			GameObject.Find("ButtonManager").GetComponent<ButtonManager>().EnableNoInternete();
			//NoInternet will take care of SetActive (false) itself.
		}
	}

	public void Random () {
		StartCoroutine (checkInternet ());
	}
		
	public override void OnMatchList (bool success, string extendedInfo, List<MatchInfoSnapshot> matchList) {
		if (success) {
			if (NetworkManager.singleton.matchInfo == null) {
				if (matchList.Count == 0) {
					NetworkManager.singleton.matchMaker.CreateMatch (roomName, roomSize, true, "", "", "", 0, 0, NetworkManager.singleton.OnMatchCreate);
				} else {
					NetworkManager.singleton.matchMaker.JoinMatch (matchList [0].networkId, "", "", "", 0, 0, NetworkManager.singleton.OnMatchJoined);
					StartCoroutine(Signal ());
				}
			}
		} else
			StartCoroutine (Signal());
	}

	void Update(){
		//Debug.Log (NetworkManager.singleton.matches);
		//RefreshRoomList ();
		//Debug.Log (NetworkManager.singleton.matches.Count == 0);
		//Debug.Log(numPlayers);
	}

	//<Disconnects>
	public override void OnClientDisconnect (NetworkConnection conn) {
		base.OnClientDisconnect (conn);
		Debug.Log ("Server GOT Disconnected!");
		StartCoroutine (EnableHostDisconnect ());
		serverDisconnected = true;
		serverStarted = false;
		hostDiscovered = false;
		if (gameObject.GetComponent<TestPlayerClient> ())
			Destroy (gameObject.GetComponent<TestPlayerClient> ());
	}

	IEnumerator EnableHostDisconnect(){
		yield return new WaitForSeconds (1f);
		GameObject.Find("ButtonManager").GetComponent<ButtonManager>().EnableHostLeft();
	}

	public override void OnServerDisconnect (NetworkConnection conn) {
		base.OnServerDisconnect (conn);
		Debug.Log ("Client GOT Disconnected!");
		clientDisconnected = true;
	}
	//</Disconnects>

	public void HostWillNotWait () {
		NetworkManager.singleton.matchMaker.SetMatchAttributes (matchInfo.networkId, false, matchInfo.domain, OnSetMatchAttributes);
	}

	public override void OnSetMatchAttributes (bool success, string extendedMatchInfo) {
		if (success) {
			NetworkManager.singleton.matchMaker.DropConnection (matchInfo.networkId, matchInfo.nodeId, 0, NetworkManager.singleton.OnDropConnection);
			NetworkManager.singleton.StopHost ();
		} else {
			GameObject.Find ("Controller").GetComponent<Controller>().OffensiveDestruction();
		}
	}
}