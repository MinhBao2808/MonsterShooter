using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventGameManager : MonoBehaviour {
    public static EventGameManager instance = null;
    //[SerializeField] private GameObject player;
    [SerializeField] private GameObject[] gameLv;
    [SerializeField] private GameObject bossHealthBar;
    [SerializeField] private Transform[] bossSpawnPoint;
	[SerializeField] private Slider bossSlider;
	[SerializeField] private Image fillImage;
	[SerializeField] private Color fullHealthColor = Color.green;
	[SerializeField] private Color zeroHealthColor = Color.red;
    [SerializeField] private float startingHealth;
    [SerializeField] private GameObject warningBoss;
    //[SerializeField] private GameObject checkPointToNextLevel;
    [SerializeField] private GameObject[] bossGame;
    [SerializeField] private GameObject[] gameLvObject;
    [SerializeField] private float timeBetweenToShotOfBossBullet;
    //[SerializeField] private GameObject[] spawnPoint3;

    private int enemyCount;
    private bool bossKill = false;
    private int killEnemyToHaveBoss = 50;
    private int enemyKill = 0;
    private int enemyKillCount = 0;
    private bool isBossSpawn = false;
	private float currentHealth;
	private bool isDead;
    private bool doorOpen = false;
    private int bossLevel = 0;
    private int gameLevelObjectIndex = 0;


    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        timeBetweenToShotOfBossBullet = 5.0f;
        //gameLvObject[0].SetActive(true);
        //gameLvObject[1].SetActive(true);
        //for (int i = 2; i < gameLvObject.Length;i++) {
        //    gameLvObject[i].SetActive(false);
        //}
    }

    public void PlayerKillEnemy() {
        enemyKill += 1;
        enemyCount += 1;
        //int a = 0;
        //a++;
        //if (a >= 1) {
        //    a = 1;

        //    Debug.Log(enemyKill);
        //}
    }

    public int EnemyKillCount {
        get {
            return enemyCount;
        }
    }

	// Use this for initialization
	void Start() {
		currentHealth = startingHealth;
		isDead = false;
	}

	public void SetHeal() {
		bossSlider.value = currentHealth;
		fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentHealth / startingHealth);
	}

    public float CurrentHealth {
        get {
            return currentHealth;
        }
    }

    public void BossGetDame(int dame) {
        currentHealth -= dame;
        SetHeal();
    }

    public float TimeToShotBetweenToShotOfBossBullet {
        get {
            return timeBetweenToShotOfBossBullet;
        }
    }

    public void BossIsDead() {
        bossKill = true;
    }

    public bool BossKill {
        get {
            return bossKill;
        }
    }

    public bool DoorOpen {
        get {
            return doorOpen;
        }
    }

    //public int GetGameLevel {
    //    get {
    //        return gameLevel;
    //    }
    //}

	// Update is called once per frame
	void Update () {
        if (enemyKill >= killEnemyToHaveBoss && isBossSpawn == false) {
            //spawn boss
            CallBoss();
        }
        if (bossKill == true) {
            bossHealthBar.SetActive(false);
            //open door
            //doorToNextLevel[gameLevel - 1].SetActive(false);
            if (Tank_moving.instance.IsPlayerMoveToCheckPoint == true) {
                //doorToCloseToBattle[gameLevel - 1].SetActive(true);
                gameLevelObjectIndex++;
                //doorToNextLevel[gameLevel - 1].SetActive(true);
                //doorToCloseToBattle[gameLevel - 1].SetActive(false);
                //checkPointToNextLevel.SetActive(false);
                bossLevel += 1;
                enemyKill = 0;
                startingHealth += 100;
                bossKill = false;
                isBossSpawn = false;
                currentHealth = startingHealth;
                bossSlider.maxValue = startingHealth;
                GameManger.instance.calculateScore(0);
                timeBetweenToShotOfBossBullet -= 1.0f;
                gameLvObject[gameLevelObjectIndex - 1].SetActive(false);
                gameLvObject[gameLevelObjectIndex + 1].SetActive(true);
            }
        }
	}

    void CallBoss() {
        StartCoroutine(WaitTimeToCallBoss());
    }

    IEnumerator WaitTimeToCallBoss() {
        warningBoss.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        warningBoss.SetActive(false);
        if (isBossSpawn == false) {
            int index = Random.Range(0, 4);
            Instantiate(bossGame[index], bossSpawnPoint[bossLevel].position, bossSpawnPoint[bossLevel].rotation);
            bossHealthBar.SetActive(true);
            SetHeal();
        }
        isBossSpawn = true;
    }
}
