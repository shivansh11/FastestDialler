using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkillController : MonoBehaviour {
	float timer = 0f; 
	float timeLimit  = 0f;
	float timeInterval  = 0f;
	public GameObject Greeting;
	public GameObject Rounds;
	public Sprite Go;
	public Sprite Retry;
	public Sprite Back;
	public Button Action;
	public Text telling;
	public Text Reward;
	public GameObject Pop;
	public Text Nick;
	public Slider Timer;
	public Slider Personal;
	public GameObject number;
	private string numberCopy;
	private string legitCopy = "";
	public int[] nums;
	private int i = 0;
	private int red = 0;
	private int x = 0;
	private int count = 0;
	public int round = 0;
	public Text clock;
	void Start () {
		if(PlayerPrefs.GetInt ("isGameWon") == 1)
			Action.GetComponent<Image> ().sprite = Go;
		else
			Action.GetComponent<Image> ().sprite = Retry;

		Greeting.SetActive (true);
		prepare ();
	}
	void prepare(){
		nums = new int[5];
		nums[0] = Random.Range (1000000, 10000000);
		nums[1] = Random.Range (1000000, 10000000);
		nums[2] = Random.Range (1000000, 10000000);
		nums[3] = Random.Range (1000000, 10000000);
		nums[4] = Random.Range (1000000, 10000000);
		number.GetComponent<Text>().text = "";
		setNumber ();
		timeLimit = PlayerPrefs.GetInt ("skill");
		timeInterval = timeLimit / 5;
		telling.text = "Complete the match in " + timeLimit + " seconds.";
		DecideReward ();
		Nick.text = PlayerPrefs.GetString ("nick");
	}

	public void DecideReward () {
		if(timeLimit == 25)
			Reward.text = "Reward: Godly Fingers";
		else if (timeLimit == 24)
			Reward.text = "Reward: Godly Fingers";
		else if (timeLimit == 23)
			Reward.text = "Reward: Title";
		else if (timeLimit == 22)
			Reward.text = "Reward: Godly Fingers";
		else if (timeLimit == 21)
			Reward.text = "Reward: Title";
		else if (timeLimit == 20)
			Reward.text = "Reward: Godly Fingers";
		else if (timeLimit == 19)
			Reward.text = "Reward: Title";
		else if (timeLimit == 18)
			Reward.text = "Reward: Car";
		else if (timeLimit == 17)
			Reward.text = "Reward: Title";
		else if (timeLimit == 16)
			Reward.text = "Reward: Car";
		else if (timeLimit == 15)
			Reward.text = "Reward: Godly Fingers";
		else if (timeLimit == 14)
			Reward.text = "Reward: Car";
		else if (timeLimit == 13)
			Reward.text = "Reward: Car";
		else if (timeLimit == 12)
			Reward.text = "Reward: Car";
		else if (timeLimit == 11)
			Reward.text = "Reward: Title";
		else if (timeLimit == 10)
			Reward.text = "Reward: Car";
		else
			Reward.text = "Reward: Contentment";
	}

	public void Unlock () {
		if (PlayerPrefs.GetInt ("skill") == 24) {
			PlayerPrefs.SetInt ("Godlies", PlayerPrefs.GetInt ("Godlies") + 3);
			PopUp ("You received 3 Godly Fingers!");
		} else if (PlayerPrefs.GetInt ("skill") == 23) {
			PlayerPrefs.SetInt ("Godlies", PlayerPrefs.GetInt ("Godlies") + 4);
			PopUp ("You received 4 Godly Fingers!");
		} else if (PlayerPrefs.GetInt ("skill") == 22) {
			PlayGames.UnlockAchievement (GPGSIds.achievement_fastest_dialler_aspirant);
			PlayerPrefs.SetString ("title", "Fastest Dialler Aspirant!");
			PopUp (PlayerPrefs.GetString ("title"));
		} else if (PlayerPrefs.GetInt ("skill") == 21) {
			PlayGames.UnlockAchievement (GPGSIds.achievement_you_received_5_godly_fingers);
			PlayerPrefs.SetInt ("Godlies", PlayerPrefs.GetInt ("Godlies") + 5);
			PopUp ("You received 5 Godly Fingers!");
		} else if (PlayerPrefs.GetInt ("skill") == 20) {
			PlayGames.UnlockAchievement (GPGSIds.achievement_hardworker);
			PlayerPrefs.SetString ("title", "Hardworker!");
			PopUp (PlayerPrefs.GetString ("title"));
		} else if (PlayerPrefs.GetInt ("skill") == 19) {
			PlayGames.UnlockAchievement (GPGSIds.achievement_you_received_10_godly_fingers);
			PlayerPrefs.SetInt ("Godlies", PlayerPrefs.GetInt ("Godlies") + 10);
			PopUp ("You received 10 Godly Fingers!");
		} else if (PlayerPrefs.GetInt ("skill") == 18) {
			PlayGames.UnlockAchievement (GPGSIds.achievement_fully_dedicated);
			PlayerPrefs.SetString ("title", "Fully Dedicated!");
			PopUp (PlayerPrefs.GetString ("title"));
		} else if (PlayerPrefs.GetInt ("skill") == 17) {
			PlayGames.UnlockAchievement (GPGSIds.achievement_new_car_unlocked);
			PlayerPrefs.SetInt ("DeCara", 1);
			PopUp ("You unlocked DeCara!");
		} else if (PlayerPrefs.GetInt ("skill") == 16) {
			PlayGames.UnlockAchievement (GPGSIds.achievement_born_to_be_the_best);
			PlayerPrefs.SetString ("title", "Born to be the best!");
			PopUp (PlayerPrefs.GetString ("title"));
		} else if (PlayerPrefs.GetInt ("skill") == 15) {
			PlayGames.UnlockAchievement (GPGSIds.achievement_new_car_yay);
			PlayerPrefs.SetInt ("Bobman", 1);
			PopUp ("You unlocked Bobman!");
		} else if (PlayerPrefs.GetInt ("skill") == 14) {
			PlayGames.UnlockAchievement (GPGSIds.achievement_you_received_20_godly_fingers);
			PlayerPrefs.SetInt ("Godlies", PlayerPrefs.GetInt ("Godlies") + 20);
			PopUp ("You received 20 Godly Fingers!");
		} else if (PlayerPrefs.GetInt ("skill") == 13) {
			PlayGames.UnlockAchievement (GPGSIds.achievement_new_ride_marvellous);
			PlayerPrefs.SetInt ("Hoaxer", 1);
			PopUp ("You unlocked Hoaxer!");
		} else if (PlayerPrefs.GetInt ("skill") == 12) {
			PlayGames.UnlockAchievement (GPGSIds.achievement_new_car_sassy);
			PlayerPrefs.SetInt ("GarelioST", 1);
			PopUp ("You unlocked GarelioST");
		} else if (PlayerPrefs.GetInt ("skill") == 11) {
			PlayGames.UnlockAchievement (GPGSIds.achievement_new_ride_so_classy);
			PlayerPrefs.SetInt ("ZetaX", 1);
			PopUp ("You unlocked ZetaX!");
		}
		else if (PlayerPrefs.GetInt ("skill") == 10) {
			PlayGames.UnlockAchievement (GPGSIds.achievement_master_sooo_great);
			PlayerPrefs.SetString ("title", "Master!");
			PopUp (PlayerPrefs.GetString ("title"));
		} else if (PlayerPrefs.GetInt ("skill") == 9) {
			PlayGames.UnlockAchievement (GPGSIds.achievement_you_unlocked_the_car);
			PlayerPrefs.SetInt ("XpeedRX", 1);
			PopUp ("You unlocked XpeedRX!");
		}else {
			//Unlock contentment :D, no such thing... chill out.
		}

	}

	public void PopUp (string message) {
		Pop.GetComponent<Text> ().text = message;
		StartCoroutine (PlayPopUp());
	}

	IEnumerator PlayPopUp () {
		Pop.GetComponent<Animation> ().Play ("PopUp");
		yield return new WaitForSeconds (2.5f);
	}

	public void reset (){
		if(PlayerPrefs.GetInt ("isGameWon") == 1)
			Action.GetComponent<Image> ().sprite = Go;
		else
			Action.GetComponent<Image> ().sprite = Retry;
		timer = 0f;
		Timer.value = 0f;
		Personal.value = 0f;
		timeLimit  = 0f;
		timeInterval  = 0f;
		legitCopy = "";
		i = 0;
		x = 0;
		count = 0;
		round = 0;
		prepare ();
		red = 0;
	}

	void Update () {
		if (Greeting.activeSelf)
			return;
		if (Input.GetKeyDown (KeyCode.Escape))
			pause ();
		if (Greeting.activeSelf == false && timer <= timeLimit) {
			if(Mathf.Floor(timer) < 10)
				clock.text = "00:0" + (Mathf.Floor(timer)).ToString ();
			else 
				clock.text = "00:" + (Mathf.Floor(timer)).ToString ();
			
			timer += Time.deltaTime;
			Timer.value = Mathf.Floor (timer / timeInterval) / 5;
		}
		if (timer >= timeLimit) {
			clock.text = "00:" + timeLimit;
		}
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
		//Choose winner
		if (Personal.value == 1f || Timer.value == 1f) {
			if (Personal.value == 1f && Timer.value < 1f) {
				if (PlayerPrefs.GetInt ("skill") > 0) {
					PlayerPrefs.SetInt ("skill", PlayerPrefs.GetInt ("skill") - 1);
					Unlock ();
				}
				PlayerPrefs.SetInt ("isGameWon", 1);
			} else {
				PlayerPrefs.SetInt ("isGameWon", 0);
			}
			reset ();
			Greeting.SetActive (true);
		}
	}

	void progress (){
		Personal.value = round * 0.2f;
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
		if (count < 7 && red == 0){
			if (number.GetComponent<Text>().text [i].ToString ().CompareTo ("0") == 0) {
				right ();
			} else {
				number.GetComponent<Animation> ().Play ("Number");
				wrong ();
			}
			count++;
			legitCopy += 0.ToString ();
		}
	}

	public void one(){
		if (count < 7 && red == 0) {
			if (number.GetComponent<Text>().text [i].ToString ().CompareTo ("1") == 0) {
				right ();
			} else {
				number.GetComponent<Animation> ().Play ("Number");
				wrong ();
			}
			count++;
			legitCopy += 1.ToString ();
		}
	}

	public void two(){
		if (count < 7 && red == 0) {
			if (number.GetComponent<Text>().text [i].ToString ().CompareTo ("2") == 0) {
				right ();
			} else {
				number.GetComponent<Animation> ().Play ("Number");
				wrong ();
			}
			count++;
			legitCopy += 2.ToString ();
		}
	}

	public void three(){
		if (count < 7 && red == 0) {
			if (number.GetComponent<Text>().text [i].ToString ().CompareTo ("3") == 0) {
				right ();
			} else {
				number.GetComponent<Animation> ().Play ("Number");
				wrong ();
			}
			count++;
			legitCopy += 3.ToString ();
		}
	}

	public void four(){
		if (count < 7 && red == 0) {
			if (number.GetComponent<Text>().text [i].ToString ().CompareTo ("4") == 0) {
				right ();
			} else {
				number.GetComponent<Animation> ().Play ("Number");
				wrong ();
			}
			count++;
			legitCopy += 4.ToString ();
		}
	}

	public void five(){
		if (count < 7 && red == 0) {
			if (number.GetComponent<Text>().text [i].ToString ().CompareTo ("5") == 0) {
				right ();
			} else {
				number.GetComponent<Animation> ().Play ("Number");
				wrong ();
			}
			count++;
			legitCopy += 5.ToString ();
		}
	}

	public void six(){
		if (count < 7 && red == 0) {
			if (number.GetComponent<Text>().text [i].ToString ().CompareTo ("6") == 0) {
				right ();
			} else {
				number.GetComponent<Animation> ().Play ("Number");
				wrong ();
			}
			count++;
			legitCopy += 6.ToString ();
		}
	}

	public void seven(){
		if (count < 7 && red == 0) {
			if (number.GetComponent<Text>().text [i].ToString ().CompareTo ("7") == 0) {
				right ();
			} else {
				number.GetComponent<Animation> ().Play ("Number");
				wrong ();
			}
			count++;
			legitCopy += 7.ToString ();
		}
	}

	public void eight(){
		if (count < 7 && red == 0) {
			if (number.GetComponent<Text>().text [i].ToString ().CompareTo ("8") == 0) {
				right ();
			} else {
				number.GetComponent<Animation> ().Play ("Number");
				wrong ();
			}
			count++;
			legitCopy += 8.ToString ();
		}
	}

	public void nine(){
		if (count < 7 && red == 0) {
			if (number.GetComponent<Text>().text [i].ToString ().CompareTo ("9") == 0) {
				right ();
			} else {
				number.GetComponent<Animation> ().Play ("Number");
				wrong ();
			}
			count++;
			legitCopy += 9.ToString ();
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
		PlayerPrefs.SetInt ("isGameWon", 1);
		Destroy (GameObject.Find ("NetworkManager"));
		SceneManager.LoadScene ("Genesis");
	}

	public void go () {
		GameObject.Find ("AudioManager").GetComponent<Audio> ().PlayClick ();
		Greeting.SetActive (false);
	}

	public void pause () {
		GameObject.Find ("AudioManager").GetComponent<Audio> ().PlayClick ();
		Action.GetComponent<Image> ().sprite = Back;
		Greeting.SetActive (true);
	}
}
