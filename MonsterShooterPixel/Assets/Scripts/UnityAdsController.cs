using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
//using UnityEngine.Advertisements;
public class UnityAdsController : MonoBehaviour {
    public static UnityAdsController instance;
    [HideInInspector]
	public string gameID;
	public string gameID_IOS = "";
	public string gameID_Android = "";
	public bool enableTestMode;
    public delegate void ContinueGame(bool success);
    public ContinueGame continues;
	//  public int AdType;
	void Start()
	{
		MakeInstance();
#if UNITY_ANDROID
        gameID = gameID_Android;
#elif UNITY_IPHONE
		gameID = gameID_IOS;
#endif
		if (Advertisement.isSupported)
		{
			Advertisement.Initialize(gameID, enableTestMode);
		}
	}
	void MakeInstance()
	{
		if (instance == null)
		{
			instance = this;
		}
	}
	public void ShowAd()
	{
		if (Advertisement.IsReady("rewardedVideo"))
		{
			ShowOptions options = new ShowOptions();
			options.resultCallback = HandleShowResult;
			Advertisement.Show("rewardedVideo", options);

		}
		else
		{

			Debug.Log("Ad not loaded");

		}
	}
    public void ShowSkipAd(){
		if (Advertisement.IsReady("video"))
		{
			Advertisement.Show("video");

		}
    }
	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
			case ShowResult.Finished:
                continues(true);
				break;
			case ShowResult.Skipped:
                continues(false);
				break;
			case ShowResult.Failed:
				Debug.LogError("Video failed to show.");
				break;
		}
	}
}
