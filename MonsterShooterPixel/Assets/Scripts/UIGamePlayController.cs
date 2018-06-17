using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGamePlayController : MonoBehaviour {
    [SerializeField] protected Text gameScoreText;
    [SerializeField] protected Text yourScoreText;
    [SerializeField] protected Text yourHighScoreText;
    [SerializeField] protected Text coinText;
    [SerializeField] protected Text enemyKillCountText;

    protected void SetCoin(int coinPoint) {
        coinText.text = "" + coinPoint;
    }

    protected void SetPoint(int gamePoint) {
        gameScoreText.text = "" + gamePoint;
    }

    protected void SetYourScore (int gamePoint) {
        yourScoreText.text = "" + gamePoint;
    }

    protected void SetYourBestScore(int bestScore) {
        yourHighScoreText.text = "" + bestScore;
    }

    protected void SetYourEnemyKill (int enemyKillCount) {
        enemyKillCountText.text = "" + enemyKillCount;
    }


    protected void GotoScene (string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
