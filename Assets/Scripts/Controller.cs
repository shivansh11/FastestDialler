using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
public class Controller : MonoBehaviour {
	private NetworkManager netMngr;
	public Canvas canvas;
	public float width;
	public RectTransform crt;
	public GameObject personal;
	public GameObject personalCar;
	public GameObject opponentCar;
	public Vector2 PersonalCarVector;
	public float posY;
	public Vector2 OpponentCarVector;
	public float posY_;
	public GameObject NetManager;
	public GameObject PauseScreen;
	public GameObject opponent;
	public GameObject Rematch;
	public GameObject number;
	public GameObject Rounds;
	private string numberCopy;
	private string legitCopy = "";
	public int[] nums;
	private int i = 0;
	private int x = 0;
	private int count = 0;
	private int red = 0;
	public int round = 0;
	public int isRematch = 0;
	public int gf = 0;
	public GameObject Hotspot;
	public bool amIServer;
	public GameObject GodlyFingerImage;

	void Start () {
		crt = canvas.GetComponent<RectTransform> ();
		width = crt.rect.width;
		PersonalCarVector = personalCar.GetComponent<RectTransform> ().anchoredPosition;
		OpponentCarVector = opponentCar.GetComponent<RectTransform> ().anchoredPosition;
		posY = PersonalCarVector.y;
		posY_ = OpponentCarVector.y;
		Rematch.SetActive (false);

		if (PlayerPrefs.GetInt ("Friend") == 1)
			Hotspot.SetActive (true);
		
		PauseScreen.SetActive (false);
		netMngr = NetworkManager.singleton;
		NetManager = GameObject.Find ("NetworkManager");
		nums = new int[5];
		number = GameObject.Find("Number");
		personal = GameObject.Find("Personal");
		number.GetComponent<Text>().text = "";
		if (PlayerPrefs.GetInt ("IsGodly") == 1 && PlayerPrefs.GetInt ("Godlies") > 0) 
			GodlyFingerImage.SetActive (true);
		else
			GodlyFingerImage.SetActive (false);
	}

	public void EnableRematch(){
		Rematch.SetActive (true);
	}

	public void DisableRematch(){
		Rematch.SetActive (false);
	}

	public void EnableHotspot(){
		Hotspot.SetActive (true);
	}

	public void DisableHotspot(){
		Hotspot.SetActive (false);
	}

	public void rematch () {
		personalCar.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0f,posY);
		opponentCar.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0f,posY_);
		personal.GetComponent<Slider>().value = 0f;
		opponent.GetComponent<Slider>().value = 0f;
		legitCopy = "";
		i = 0;
		x = 0;
		count = 0;
		red = 0;
		round = 0;
		gf = 0;
		if (PlayerPrefs.GetInt ("IsGodly") == 1 && PlayerPrefs.GetInt ("Godlies") > 0)
			GodlyFingerImage.SetActive (true);
		else 
			GodlyFingerImage.SetActive (false);
		
		number.GetComponent<Text>().text = "";
		isRematch = 1;
	}

	public void HandleClientDisconnection () {
		personalCar.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0f,posY);
		opponentCar.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0f,posY_);
		personal.GetComponent<Slider>().value = 0f;
		opponent.GetComponent<Slider>().value = 0f;
		legitCopy = "";
		i = 0;
		x = 0;
		count = 0;
		round = 0;
		red = 0;
		number.GetComponent<Text>().text = "";
	}

	void Update () {
		if (legitCopy == numberCopy && round < 4) { //first round is already initialized, therefore, setNumber must be called for 4 times, not 5.
			Debug.Log ("Calling SetNumber()");
			round++;
			setNumber ();
			progress ();
		}
		//for completion of progress bar
		if (legitCopy == numberCopy && round == 4) {
			round++;
			progress ();
		}
	}

	void progress (){
		personalCar.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (round * 0.2f * width, posY);
		personal.GetComponent<Slider>().value = round * 0.2f;
	}

	public void setNumber(){
		number.GetComponent<Text>().text = nums[x].ToString ();
		x++;
		numberCopy = number.GetComponent<Text>().text;
		clear ();
		Rounds.GetComponent<Text> ().text = (round + 1).ToString ();
	}

	public void clear(){
		i = 0;
		count = 0;
		number.GetComponent<Text>().text = numberCopy;
		number.GetComponent<Text>().color = Color.white;
		legitCopy = "";
		red = 0;
	}

	public void back(){
		if (count > 0) {
			red = 0;
			i -= 24; //24 because each actual digit int eh number is surrounded by html for coloring. 
			number.GetComponent<Text>().text = number.GetComponent<Text>().text.Remove (i); //delete the ith digit
			count--;
			number.GetComponent<Text>().text = number.GetComponent<Text>().text.Insert (i, numberCopy.Substring (count, 7 - count));
			//Debug.Log (numberCopy.Substring (count, 7 - count));
			legitCopy = legitCopy.Remove(legitCopy.Length - 1);
			//Debug.Log (legitCopy);
		}
	}

	public void zero(){
		if (PlayerPrefs.GetInt ("IsGodly") == 1 && PlayerPrefs.GetInt ("Godlies") > 0 && gf < 5 && number.GetComponent<Text> ().text [i].ToString ().CompareTo ("0") != 0) {
			legitCopy += number.GetComponent<Text> ().text [i].ToString ();
			right ();
			count++;
			gf++;
			PlayerPrefs.SetInt ("Godlies", PlayerPrefs.GetInt ("Godlies") - 1);
			if (gf == 5 || PlayerPrefs.GetInt ("Godlies") == 0)
				GodlyFingerImage.SetActive (false);
			return;
		} else {
			if (count < 7 && red == 0) {
				if (number.GetComponent<Text> ().text [i].ToString ().CompareTo ("0") == 0) {
					right ();
				} else {
					number.GetComponent<Animation> ().Play ("Number");
					wrong ();
				}
				count++;
				legitCopy += 0.ToString ();
			}
		}
	}

	public void one(){
		if (PlayerPrefs.GetInt ("IsGodly") == 1 && PlayerPrefs.GetInt ("Godlies") > 0 && gf < 5 && number.GetComponent<Text> ().text [i].ToString ().CompareTo ("1") != 0) {
			legitCopy += number.GetComponent<Text> ().text [i].ToString ();
			right ();
			count++;
			gf++;
			PlayerPrefs.SetInt ("Godlies", PlayerPrefs.GetInt ("Godlies") - 1);
			if (gf == 5 || PlayerPrefs.GetInt ("Godlies") == 0)
				GodlyFingerImage.SetActive (false);
			return;
		} else {
			if (count < 7 && red == 0) {
				if (number.GetComponent<Text> ().text [i].ToString ().CompareTo ("1") == 0) {
					right ();
				} else {
					number.GetComponent<Animation> ().Play ("Number");
					wrong ();
				}
				count++;
				legitCopy += 1.ToString ();
			}
		}
	}

	public void two(){
		if (PlayerPrefs.GetInt ("IsGodly") == 1 && PlayerPrefs.GetInt ("Godlies") > 0 && gf < 5 && number.GetComponent<Text> ().text [i].ToString ().CompareTo ("2") != 0) {
			legitCopy += number.GetComponent<Text> ().text [i].ToString ();
			right ();
			count++;
			gf++;
			PlayerPrefs.SetInt ("Godlies", PlayerPrefs.GetInt ("Godlies") - 1);
			if (gf == 5 || PlayerPrefs.GetInt ("Godlies") == 0)
				GodlyFingerImage.SetActive (false);
			return;
		} else {
			if (count < 7 && red == 0) {
				if (number.GetComponent<Text> ().text [i].ToString ().CompareTo ("2") == 0) {
					right ();
				} else {
					number.GetComponent<Animation> ().Play ("Number");
					wrong ();
				}
				count++;
				legitCopy += 2.ToString ();
			}
		}
	}

	public void three(){
		if (PlayerPrefs.GetInt ("IsGodly") == 1 && PlayerPrefs.GetInt ("Godlies") > 0 && gf < 5 && number.GetComponent<Text> ().text [i].ToString ().CompareTo ("3") != 0) {
			legitCopy += number.GetComponent<Text> ().text [i].ToString ();
			right ();
			count++;
			gf++;
			PlayerPrefs.SetInt ("Godlies", PlayerPrefs.GetInt ("Godlies") - 1);
			if (gf == 5 || PlayerPrefs.GetInt ("Godlies") == 0)
				GodlyFingerImage.SetActive (false);
			return;
		} else {
			if (count < 7 && red == 0) {
				if (number.GetComponent<Text> ().text [i].ToString ().CompareTo ("3") == 0) {
					right ();
				} else {
					number.GetComponent<Animation> ().Play ("Number");
					wrong ();
				}
				count++;
				legitCopy += 3.ToString ();
			}
		}
	}

	public void four(){
		if (PlayerPrefs.GetInt ("IsGodly") == 1 && PlayerPrefs.GetInt ("Godlies") > 0 && gf < 5 && number.GetComponent<Text> ().text [i].ToString ().CompareTo ("4") != 0) {
			legitCopy += number.GetComponent<Text> ().text [i].ToString ();
			right ();
			count++;
			gf++;
			PlayerPrefs.SetInt ("Godlies", PlayerPrefs.GetInt ("Godlies") - 1);
			if (gf == 5 || PlayerPrefs.GetInt ("Godlies") == 0)
				GodlyFingerImage.SetActive (false);
			return;
		} else {
			if (count < 7 && red == 0) {
				if (number.GetComponent<Text> ().text [i].ToString ().CompareTo ("4") == 0) {
					right ();
				} else {
					number.GetComponent<Animation> ().Play ("Number");
					wrong ();
				}
				count++;
				legitCopy += 4.ToString ();
			}
		}
	}

	public void five(){
		if (PlayerPrefs.GetInt ("IsGodly") == 1 && PlayerPrefs.GetInt ("Godlies") > 0 && gf < 5 && number.GetComponent<Text> ().text [i].ToString ().CompareTo ("5") != 0) {
			legitCopy += number.GetComponent<Text> ().text [i].ToString ();
			right ();
			count++;
			gf++;
			PlayerPrefs.SetInt ("Godlies", PlayerPrefs.GetInt ("Godlies") - 1);
			if (gf == 5 || PlayerPrefs.GetInt ("Godlies") == 0)
				GodlyFingerImage.SetActive (false);
			return;
		} else {
			if (count < 7 && red == 0) {
				if (number.GetComponent<Text> ().text [i].ToString ().CompareTo ("5") == 0) {
					right ();
				} else {
					number.GetComponent<Animation> ().Play ("Number");
					wrong ();
				}
				count++;
				legitCopy += 5.ToString ();
			}
		}
	}

	public void six(){
		if (PlayerPrefs.GetInt ("IsGodly") == 1 && PlayerPrefs.GetInt ("Godlies") > 0 && gf < 5 && number.GetComponent<Text> ().text [i].ToString ().CompareTo ("6") != 0) {
			legitCopy += number.GetComponent<Text> ().text [i].ToString ();
			right ();
			count++;
			gf++;
			PlayerPrefs.SetInt ("Godlies", PlayerPrefs.GetInt ("Godlies") - 1);
			if (gf == 5 || PlayerPrefs.GetInt ("Godlies") == 0)
				GodlyFingerImage.SetActive (false);
			return;
		} else {
			if (count < 7 && red == 0) {
				if (number.GetComponent<Text> ().text [i].ToString ().CompareTo ("6") == 0) {
					right ();
				} else {
					number.GetComponent<Animation> ().Play ("Number");
					wrong ();
				}
				count++;
				legitCopy += 6.ToString ();
			}
		}
	}

	public void seven(){
		if (PlayerPrefs.GetInt ("IsGodly") == 1 && PlayerPrefs.GetInt ("Godlies") > 0 && gf < 5 && number.GetComponent<Text> ().text [i].ToString ().CompareTo ("7") != 0) {
			legitCopy += number.GetComponent<Text> ().text [i].ToString ();
			right ();
			count++;
			gf++;
			PlayerPrefs.SetInt ("Godlies", PlayerPrefs.GetInt ("Godlies") - 1);
			if (gf == 5 || PlayerPrefs.GetInt ("Godlies") == 0)
				GodlyFingerImage.SetActive (false);
			return;
		} else {
			if (count < 7 && red == 0) {
				if (number.GetComponent<Text> ().text [i].ToString ().CompareTo ("7") == 0) {
					right ();
				} else {
					number.GetComponent<Animation> ().Play ("Number");
					wrong ();
				}
				count++;
				legitCopy += 7.ToString ();
			}
		}
	}

	public void eight(){
		if (PlayerPrefs.GetInt ("IsGodly") == 1 && PlayerPrefs.GetInt ("Godlies") > 0 && gf < 5 && number.GetComponent<Text> ().text [i].ToString ().CompareTo ("8") != 0) {
			legitCopy += number.GetComponent<Text> ().text [i].ToString ();
			right ();
			count++;
			gf++;
			PlayerPrefs.SetInt ("Godlies", PlayerPrefs.GetInt ("Godlies") - 1);
			if (gf == 5 || PlayerPrefs.GetInt ("Godlies") == 0)
				GodlyFingerImage.SetActive (false);
			return;
		} else {
			if (count < 7 && red == 0) {
				if (number.GetComponent<Text> ().text [i].ToString ().CompareTo ("8") == 0) {
					right ();
				} else {
					number.GetComponent<Animation> ().Play ("Number");
					wrong ();
				}
				count++;
				legitCopy += 8.ToString ();
			}
		}
	}

	public void nine(){
		if (PlayerPrefs.GetInt ("IsGodly") == 1 && PlayerPrefs.GetInt ("Godlies") > 0 && gf < 5 && number.GetComponent<Text> ().text [i].ToString ().CompareTo ("9") != 0) {
			legitCopy += number.GetComponent<Text> ().text [i].ToString ();
			right ();
			count++;
			gf++;
			PlayerPrefs.SetInt ("Godlies", PlayerPrefs.GetInt ("Godlies") - 1);
			if (gf == 5 || PlayerPrefs.GetInt ("Godlies") == 0)
				GodlyFingerImage.SetActive (false);
			return;
		} else {
			if (count < 7 && red == 0) {
				if (number.GetComponent<Text> ().text [i].ToString ().CompareTo ("9") == 0) {
					right ();
				} else {
					number.GetComponent<Animation> ().Play ("Number");
					wrong ();
				}
				count++;
				legitCopy += 9.ToString ();
			}
		}
	}

	public void right(){
		number.GetComponent<Text>().text = number.GetComponent<Text>().text.Insert (i, "<color=#008400>");
		i += 16;
		number.GetComponent<Text>().text = number.GetComponent<Text>().text.Insert (i, "</color>");
		i += 8;
	}

	public void wrong(){
		if (red == 0) {
			red = 1;
			number.GetComponent<Text> ().text = number.GetComponent<Text> ().text.Insert (i, "<color=#A50000>");
			i += 16;
			number.GetComponent<Text> ().text = number.GetComponent<Text> ().text.Insert (i, "</color>");
			i += 8;
		}
	}

	public void menu () {
		GameObject.Find ("AudioManager").GetComponent<Audio> ().PlayClick ();

		if (PlayerPrefs.GetInt ("Friend") == 0 && !amIServer) { //Matchmaker disConnection for client only
			MatchInfo matchInfo = netMngr.matchInfo;
			netMngr.matchMaker.DropConnection (matchInfo.networkId, matchInfo.nodeId, 0, netMngr.OnDropConnection);
			netMngr.StopHost ();
		} else if (PlayerPrefs.GetInt ("Friend") == 0 && amIServer) { //Matchmaker disConnection for server only
			NetManager.GetComponent<Network_Manager> ().HostWillNotWait();
		} else { //Friend Code
			NetManager.GetComponent<Network_Manager> ().serverStarted = false;
			NetManager.GetComponent<Network_Manager> ().hostDiscovered = false;
			if (NetManager.GetComponent<TestPlayerClient> ())
				Destroy (NetManager.GetComponent<TestPlayerClient> ());
			if (NetManager.GetComponent<NetworkDiscovery> ())
				Destroy (NetManager.GetComponent<NetworkDiscovery> ());
			netMngr.StopHost ();
		}
	}

	public void PauseToggle () {
		GameObject.Find ("AudioManager").GetComponent<Audio> ().PlayClick ();
		PauseScreen.SetActive (!PauseScreen.activeSelf);
	}

	public void PauseDisable () {
		PauseScreen.SetActive (false);
	}

	public void OffensiveDestruction() {
		Destroy (GameObject.Find ("NetworkManager"));
		SceneManager.LoadScene ("Genesis");
	}
}
