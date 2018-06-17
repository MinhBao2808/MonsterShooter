using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class GameManger : UIGamePlayController {
    public static GameManger instance;
    //[SerializeField] private Text scoreText;
    //[SerializeField] private Text coinText;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private Text scoreInGameText;
    [SerializeField] private Text coinInGameText;
    //[SerializeField] private Text yourScoreText;
    //[SerializeField] private Text yourHighScoreText;
    private int bestScore;
    private int gameScore;
    private int coinPoint;
    static int timeToPlay;
    private bool gameOver =  false;


    private void Awake() {
        if (instance == null) {
            instance = this;
        }
		if (!FB.IsInitialized) {
			FB.Init(InitCallback, OnHideUnity);
		}
		else {
			FB.ActivateApp();
		}
    }

    private void Start() {
        Invoke("CallAdsBanner", 0.01f);
        coinPoint = PlayerPrefs.GetInt("coinPoint", coinPoint);
        bestScore = PlayerPrefs.GetInt("bestScore", bestScore);
        coinInGameText.text = "" + coinPoint;
        //SetCoin(coinPoint);
    }

	private void InitCallback() {
		if (FB.IsInitialized)
		{
			// Signal an app activation App Event
			FB.ActivateApp();
			// Continue with Facebook SDK
			// ...
		}
		else
		{
			Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}

	private void OnHideUnity(bool isGameShown) {
		if (!isGameShown)
		{
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		}
		else
		{
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}

	public void FBShare() {
		FB.ShareLink(
			new System.Uri("https://itunes.apple.com/us/app/monster-shooter-pixel/id1273651198?ls=1&mt=8"),
			"Monster Shooter Pixel\n" + "Can you beat my best score " + bestScore + "?",
			"Can you beat my best score " + bestScore + "?",
			new System.Uri("http://i.imgur.com/chTBNYM.png"),
			ShareCallback);
	}

	private void ShareCallback(IShareResult result) {
		if (result.Cancelled || !string.IsNullOrEmpty(result.Error))
		{
			Debug.Log("ShareLink Error: " + result.Error);
		}
		else if (!string.IsNullOrEmpty(result.PostId))
		{
			// Print post identifier of the shared content
			Debug.Log(result.PostId);
		}
		else
		{
			// Share succeeded without postID
			Debug.Log("ShareLink success!");
		}
	}



	void CallAdsBanner() {
		AdsController.instance.HideBanner();
		AdsController.instance.RequestBanner();
	}

    public bool GameOver {
        get { return gameOver; }
    }

    public int GameScore {
        get { return gameScore; }
    }

    public void calculateScore(int score) {
        gameScore += score;
        if (gameScore > bestScore) {
			bestScore = gameScore;
			PlayerPrefs.SetInt("bestScore", bestScore);
		}
        scoreInGameText.text = "" + gameScore;
        //SetPoint(gameScore);
    }

    public void calculatorCoin(int coin) {
        coinPoint += coin;
        coinInGameText.text = "" + coinPoint;
        PlayerPrefs.SetInt("coinPoint", coinPoint);
    }

    public void tankGet0 () {
        gameOver = true;
        timeToPlay++;
        gameOverText.SetActive(true);
        //yourScoreText.text = "" + gameScore;
        //yourHighScoreText.text = "" + bestScore;
        SetYourScore(gameScore);
        SetYourBestScore(bestScore);
        SetYourEnemyKill(EventGameManager.instance.EnemyKillCount);
        SetCoin(coinPoint);
        LeaderBoardController.instance.ReportScore(bestScore);
        if (timeToPlay == 3) {
            AdsController.instance.ShowInterstitial();
            AdsController.instance.RequestInterstitial();
            timeToPlay = 0;
        }
        else {
            UnityAdsController.instance.ShowSkipAd();
        }
    }

    public void GotoGameMenu() {
        GotoScene("GamePlay");
    }

    public void GotoMenu() {
        GotoScene("GameMenu");
    }

    public void RatingGame() {
        Application.OpenURL("itms-apps://itunes.apple.com/app/id1273651198");
    }
}
