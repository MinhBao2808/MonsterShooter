using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.GameCenter;
public class LeaderBoardController : MonoBehaviour
{
	public static LeaderBoardController instance;
	//leaderboard
	private const string _highScore = "msp_bestscore";
	//----------achievement-------------------
	//private const string _lv5Unlock = "OG_level5_unlock";
	//private const string _lv10Unlock = "OG_level10_unlock";
	//private const string _lv15Unlock = "OG_level15_unlock";
	//private const string _lv20Unlock = "OG_level20_unlock";
	//private const string _buyNoAds = "SG_buy_no_ads";
	//private const string _highScoreAchieve = "SG_achieved_10000_scores";

	//private string[] achievements_name =
	//{ _lv5Unlock, _lv10Unlock, _lv15Unlock, _lv20Unlock, _buyNoAds, _highScoreAchieve};
	//----------End achievement variable------------
	//[Tooltip("this score to check unlock achievement")]
	//public int HIGH_SCORE_TO_UNLOCK_ACHIEVEMENT;
	// Use this for initialization
	void Start()
	{
		MakeSingleton();
		//Show Achievement on Top screen when it was unlock.
		UnityEngine.SocialPlatforms.GameCenter.GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
	}

	// Update is called once per frame
	void Update()
	{

	}
	//Make Singleton
	void MakeSingleton()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}
	//Open Leaderboard UI
	public void OpenLeaderboards()
	{
		if (Social.localUser.authenticated)
		{
			GameCenterPlatform.ShowLeaderboardUI(_highScore, UnityEngine.SocialPlatforms.TimeScope.AllTime);
		}
	}
	//Open Achievements UI
	//public void OpenAchivements()
	//{
	//  if (Social.localUser.authenticated)
	//  {
	//      Social.ShowAchievementsUI();
	//  }
	//}
	//Method to login GameCenter with current user
	public void LoginGameCenter()
	{
		//Check if does not has account login before
		if (!Social.localUser.authenticated)
		{
			//Start login
			Social.localUser.Authenticate((bool success) =>
			{
				if (success)
				{
					Debug.Log("Login successful!!!");
				}
				else
				{
					Debug.Log("Can not login. Please input your account to game center.");
				}
			});
		}
	}
	//Report score to leaderboard
	public void ReportScore(int _score)
	{
		if (Social.localUser.authenticated)
		{
			Social.ReportScore(_score, _highScore, (bool success) =>
			{
				if (success)
				{
					Debug.Log("Changed HighScore");
				}
			});
		}
	}
	//Unlock achievements with index
	//void UnlockAchivements(int index)
	//{
	//  if (Social.localUser.authenticated)
	//  {
	//      //Unlock this achievement with 100 percent
	//      Social.ReportProgress(achievements_name[index], 100.0f, (bool success) =>
	//      {
	//          if (success)
	//          {
	//              Debug.Log("Achievement Unlock Successful!!!");
	//          }
	//      });
	//  }
	//}
	//Unlock achievements for level 5, 10, 15, 20
	//public void CheckUnlockLevelAchievement(int _levelUnlock)
	//{
	//  if (_levelUnlock >= 5)
	//  {
	//      UnlockAchivements(0);
	//  }
	//  if (_levelUnlock >= 10)
	//  {
	//      UnlockAchivements(1);
	//  }
	//  if (_levelUnlock >= 15)
	//  {
	//      UnlockAchivements(2);
	//  }
	//  if (_levelUnlock >= 20)
	//  {
	//      UnlockAchivements(3);
	//  }
	//}
	//Unlock buy noads achievement
	//public void UnlockBuyNoAdsAchievement()
	//{
	//  UnlockAchivements(4);
	//}
	//Unlock high Score achievement
	//public void UnlockHighScoreAchievement(int _dataHighScore)
	//{
	//  //Check total score of all level with HIGH_SCORE_TO_UNLOCK_ACHIEVEMENT variable
	//  if (HIGH_SCORE_TO_UNLOCK_ACHIEVEMENT <= _dataHighScore)
	//  {
	//      UnlockAchivements(5);
	//  }
	//}
}
