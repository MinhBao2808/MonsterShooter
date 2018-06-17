using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManager : UIGamePlayController {
    public static ShopManager instance = null;
    private int cointPoint = 0;
    private int TwoGunCost = 50;
    private int ThreeGunCost = 100;
    private int FourWayCost = 150;
    private int FiveWayCost = 200;
    private int SixWayCost = 250;
    private int SevenWayCost = 300;
    private int gunName;
    [SerializeField] GameObject[] clickViewGun;
    //[SerializeField] private Text coinText;
    private string TwoGunBuy, ThreeGunBuy, FourWayBuy, FiveWayBuy, SixWayBuy, SevenWayBuy;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    // Use this for initialization
    void Start () {
        cointPoint = PlayerPrefs.GetInt("coinPoint", cointPoint);
        //cointPoint = 1000;
        coinText.text = "" + cointPoint;
        SetCoin(cointPoint);
        TwoGunBuy = PlayerPrefs.GetString("twoGunBuy", TwoGunBuy);
        ThreeGunBuy = PlayerPrefs.GetString("threeGunBuy", ThreeGunBuy);
        FourWayBuy = PlayerPrefs.GetString("fourWayBuy", FourWayBuy);
        FiveWayBuy = PlayerPrefs.GetString("fiveWayBuy", FiveWayBuy);
        SixWayBuy = PlayerPrefs.GetString("sixWayBuy", SixWayBuy);
        SevenWayBuy = PlayerPrefs.GetString("sevenWayBuy", SevenWayBuy);
	}

    public void ChooseTwoGun() {
        Debug.Log("b");
        if (TwoGunCost <= cointPoint && TwoGunBuy != "yes") {
            TwoGunBuy = "yes";
            PlayerPrefs.SetString("twoGunBuy",TwoGunBuy);
            cointPoint -= TwoGunCost;
            //coinText.text = "" + cointPoint;
            SetCoin(cointPoint);
            PlayerPrefs.SetInt("coinPoint",cointPoint);
			//SceneManager.LoadScene("GamePlay")
        }
        for (int i = 0; i < clickViewGun.Length; i++ ) {
            if (i==0) {
                clickViewGun[i].SetActive(true);
            }
            else {
                clickViewGun[i].SetActive(false);
            }

        }
        if (TwoGunBuy == "yes") {
			gunName = 1;
			PlayerPrefs.SetInt("gunName", gunName);
			//SceneManager.LoadScene("GamePlay");
			GotoScene("GamePlay");
        }

    }

    public void ChooseThreeGun() {
        Debug.Log("a");
        if (ThreeGunCost <= cointPoint && ThreeGunBuy != "yes") {
            ThreeGunBuy = "yes";
            PlayerPrefs.SetString("threeGunBuy", ThreeGunBuy);
            cointPoint -= ThreeGunCost;
            //coinText.text = "" + cointPoint;
            SetCoin(cointPoint);
            PlayerPrefs.SetInt("coinPoint",cointPoint);
        }
        for (int i = 0; i < clickViewGun.Length;i++) {
            if (i==1) {
                clickViewGun[i].SetActive(true);
            }
            else {
                clickViewGun[i].SetActive(false);
            }
        }
        if (ThreeGunBuy == "yes") {
			gunName = 2;
			PlayerPrefs.SetInt("gunName", gunName);
			//SceneManager.LoadScene("GamePlay");
			GotoScene("GamePlay");
        }

    }

    public void ChooseFourWay () {
        //clickViewGun[2].SetActive(true);
        if (FourWayBuy != "yes" && FourWayCost <= cointPoint) {
            FourWayBuy = "yes";
            PlayerPrefs.SetString("fourWayBuy",FourWayBuy);
            cointPoint -= FourWayCost;
            //coinText.text = "" + cointPoint;
            SetCoin(cointPoint);
            PlayerPrefs.SetInt("coinPoint",cointPoint);
			//gunName = 3;
			//PlayerPrefs.SetInt("gunName", gunName);
			////SceneManager.LoadScene("GamePlay");
			//GotoScene("GamePlay");
        }
        for (int i = 0; i < clickViewGun.Length; i++) {
            if (i == 2) {
                clickViewGun[i].SetActive(true);
            }
            else {
                clickViewGun[i].SetActive(false);
            }
        }
        if (FourWayBuy == "yes") {
			gunName = 3;
			PlayerPrefs.SetInt("gunName", gunName);
			//SceneManager.LoadScene("GamePlay");
			GotoScene("GamePlay");
        }

    }

    public void Choose5Way() {
        if (FiveWayBuy != "yes" && FiveWayCost <= cointPoint) {
            FiveWayBuy = "yes";
            PlayerPrefs.SetString("fiveWayBuy",FiveWayBuy);
            cointPoint -= FiveWayCost;
            //coinText.text = "" + cointPoint;
            SetCoin(cointPoint);
            PlayerPrefs.SetInt("coinPoint",cointPoint);
        }
        for (int i = 0; i < clickViewGun.Length; i++) {
            if (i == 3) {
                clickViewGun[i].SetActive(true);
            }
            else {
                clickViewGun[i].SetActive(false);
            }
        }
        if (FiveWayBuy == "yes") {
			gunName = 4;
			PlayerPrefs.SetInt("gunName", gunName);
			//SceneManager.LoadScene("GamePlay");
			GotoScene("GamePlay");
        }

    }

    public void Choose6Way() {
        if (SixWayBuy != "yes" && SixWayCost <= cointPoint) {
            SixWayBuy = "yes";
            PlayerPrefs.SetString("sixWayBuy",SixWayBuy);
            cointPoint -= SixWayCost;
            //coinText.text = "" + cointPoint;
            SetCoin(cointPoint);
            PlayerPrefs.SetInt("coinPoint",cointPoint);
        }
        for (int i = 0; i < clickViewGun.Length; i++) {
            if (i == 4) {
                clickViewGun[i].SetActive(true);
            }
            else {
                clickViewGun[i].SetActive(false);
            }
        }
        if (SixWayBuy == "yes") {
			gunName = 5;
			PlayerPrefs.SetInt("gunName", gunName);
			//SceneManager.LoadScene("GamePlay");
			GotoScene("GamePlay");
        }

    }
    public void Choose7Way() {
        if (SevenWayBuy != "yes" && SevenWayCost <= cointPoint) {
            SevenWayBuy = "yes";
			PlayerPrefs.SetString("sevenWayBuy", SixWayBuy);
			cointPoint -= SixWayCost;
			//coinText.text = "" + cointPoint;
			SetCoin(cointPoint);
			PlayerPrefs.SetInt("coinPoint", cointPoint);
		}
        for (int i = 0; i < clickViewGun.Length; i++ ) {
            if (i == 5) {
                clickViewGun[i].SetActive(true);
            }
            else {
                clickViewGun[i].SetActive(false);
            }
        }
        if (SevenWayBuy == "yes") {
            gunName = 6;
			PlayerPrefs.SetInt("gunName", gunName);
			//SceneManager.LoadScene("GamePlay");
			GotoScene("GamePlay");
        }

    }

    public void ReturnToGameMenu() {
        GotoScene("GameMenu");
    }

	public void OnWatchVideoRewarded() {
		//FirebaseAnalyticsEventLog.instance.LogEventAwardedAds(TypeFirebase.UnityShowAds);
		UnityAdsController.instance.continues = WatchvideoDelegates;
		UnityAdsController.instance.ShowAd();
	}
	[HideInInspector]
	public void WatchvideoDelegates(bool success) {
		if (success) {
			cointPoint = cointPoint + 20;
			coinText.text = "" + cointPoint;
			PlayerPrefs.SetInt("coinPoint", cointPoint);
		}
	}
}
