using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestPlayerClient : NetworkDiscovery {
	public bool hostDiscovered {
		get;
		private set;
	}
	void Start () {
		Initialize ();
		StartAsClient ();
		Debug.Log ("trying client");
		hostDiscovered = false;
		this.showGUI = false;
		this.useNetworkManager = true;
	}
	
	public override void OnReceivedBroadcast(string fromAddress, string data){
		Debug.Log ("broadcast received, starting client");
		StopBroadcast ();
		Debug.Log (fromAddress);

		//if (fromAddress [0] == ':')
		NetworkManager.singleton.networkAddress = fromAddress.Substring (7);
		//else
			//NetworkManager.singleton.networkAddress = fromAddress;
		NetworkServer.Reset ();
		NetworkManager.singleton.StartClient();
		hostDiscovered = true;
	}
}
