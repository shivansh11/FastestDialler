using GooglePlayGames;
using GooglePlayGames.BasicApi;

using UnityEngine;

public class PlayGames : MonoBehaviour {


	void Start () {
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder ().Build ();
		PlayGamesPlatform.InitializeInstance (config);
		PlayGamesPlatform.Activate ();
		SignIn ();
	}

	void SignIn(){
		Social.localUser.Authenticate (success => {});
	}

	//Achievements
	public static void UnlockAchievement(string id){
		Social.ReportProgress (id, 100, success => {});
	}

	//LeaderBoard
	public static void AddScoreToLeaderboard(string leaderboardId, long score){
		Social.ReportScore (score, leaderboardId , success => {});
	}

	public static void ShowLeaderboardsUI(){
		//Social.ShowLeaderboardUI ();
		((PlayGamesPlatform)Social.Active).ShowLeaderboardUI (GPGSIds.leaderboard_global_ranking);
	}
}
