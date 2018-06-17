using UnityEngine;
using System.Collections;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;
//using ADInterstitialAd = UnityEngine.iOS.ADInterstitialAd;
public class AdsController : MonoBehaviour
{

    public static AdsController instance;

    public BannerView bannerView;
    private static RewardBasedVideoAd rewardBasedVideo;
    private static InterstitialAd interstitial;
    [SerializeField]
    AdsMobileAdvance iOSAdsInformation = new AdsMobileAdvance();
	[SerializeField]
	AdsMobileAdvance androidAdsInformation = new AdsMobileAdvance();
    //private static string outputMessage = "";
    //public static string OutputMessage
    //{
    //  set { outputMessage = value; }
    //}

    void Start()
    {
        MakeSingleton();
        RequestRewardBasedVideo();
        RequestInterstitial();
    }

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

            // Get singleton reward based video ad reference.
            rewardBasedVideo = RewardBasedVideoAd.Instance;

            // RewardBasedVideoAd is a singleton, so handlers should only be registered once.
            rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
            rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
            rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
            rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
            rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
            rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
            rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

        }
    }

    public void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = androidAdsInformation.bannerId;
#elif UNITY_IPHONE
        string adUnitId = iOSAdsInformation.bannerId; 
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        // Register for ad events.
        bannerView.OnAdLoaded += HandleAdLoaded;
        bannerView.OnAdFailedToLoad += HandleAdFailedToLoad;
        bannerView.OnAdLoaded += HandleAdOpened;
        bannerView.OnAdClosed += HandleAdClosed;
        bannerView.OnAdLeavingApplication += HandleAdLeftApplication;
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
    }

    public void RequestRewardBasedVideo()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
            string adUnitId = androidAdsInformation.videoAwardedId;
#elif UNITY_IPHONE
            string adUnitId = iOSAdsInformation.videoAwardedId;
#else
            string adUnitId = "unexpected_platform";
#endif

		AdRequest request = new AdRequest.Builder().Build();
        rewardBasedVideo.LoadAd(request, adUnitId);
    }
        public void RequestInterstitial()
    {

#if UNITY_ANDROID
            string adUnitId = androidAdsInformation.interstitialId;
#elif UNITY_IPHONE
		string adUnitId = iOSAdsInformation.interstitialId;
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create an interstitial.
        interstitial = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();

        // Register for ad events.
        interstitial.OnAdClosed += HandleInterstitialAdClosed;
        interstitial.OnAdFailedToLoad += HandleInterstitialAdFailedToLoad;
        interstitial.OnAdLeavingApplication += HandleInterstitialAdLeftApplication;
        interstitial.OnAdLoaded += HandleInterstitialAdLoaded;
        interstitial.OnAdOpening += HandleInterstitialAdOpening;
        // Load an interstitial ad.
        interstitial.LoadAd(request);
    }
    public void HideBanner()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }

    public void ShowRewardBasedVideo()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
        else
        {
            //          print("Reward based video ad is not ready yet.");
        }
    }
    public void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
        //Debug.Log ("Nó chưa load anh ơi! Ads");
    }
    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        //      print("Loaded event received.");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //      print("Failed event received.");
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
        //      print("Opened event received.");
    }

    void HandleAdClosing(object sender, EventArgs args)
    {
        //      print("Closing event received.");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        //      print("Closed event received.");
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        //      print("Left event received.");
    }

    #endregion
    #region RewardBasedVideo callback handlers

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        //      print("HandleRewardBasedVideoLoaded event received.");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //      print("HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        //      print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        //      print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        //      print("HandleRewardBasedVideoRewarded event received");
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        //      print("HandleRewardBasedVideoLeft event received");
    }

    #endregion
    #region Interstitials
    public void HandleInterstitialAdLoaded(object sender, EventArgs args)
    {
    }

    public void HandleInterstitialAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //      print("HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialAdOpening(object sender, EventArgs args)
    {
        //      print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleInterstitialAdStarted(object sender, EventArgs args)
    {
        //      print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleInterstitialAdClosed(object sender, EventArgs args)
    {
        RequestInterstitial();
    }

    public void HandleInterstitialAdRewarded(object sender, Reward args)
    {
    }

    public void HandleInterstitialAdLeftApplication(object sender, EventArgs args)
    {
        //      print("HandleRewardBasedVideoLeft event received");
    }
    #endregion
}
[Serializable] 
public class AdsMobileAdvance{
    public string bannerId;
    public string videoAwardedId;
    public string interstitialId;
}
