using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tank_moving : SoundAndEffectController {
    public static Tank_moving instance = null;
    private Animator anim;
    //[SerializeField] private ParticleSystem smokeSystem;
    //[SerializeField] private AudioSource attackSound;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float speed;
    [SerializeField] private float rotationForce;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject[] itemDrop;
    [SerializeField] private Transform[] bulletSpawm;
    [SerializeField] private float fireForce;
    [SerializeField] private float fireRate;
    [SerializeField] private float cameraHeight = 10.0f;
    [SerializeField] private GameObject boombButton;
	[SerializeField] private float startingHealth;
	[SerializeField] private Slider tankSlider;
	[SerializeField] private Image fillImage;
	[SerializeField] private Color fullHealthColor = Color.red;
	[SerializeField] private Color zeroHealthColor = Color.red;
    [SerializeField] private GameObject boomPrefab;
	//[SerializeField] private GameObject explosionPlayerEffect;
	private float timeEffectItemInGame;
	private float currentHealth = 0.0f;
	private bool isDead;
	private bool isPlayerEatItem = false;
	private float delayTime = 10.0f;
    private float nextFire;
    private float forwardMoveAmount;
    private float turnForce;
    [SerializeField] private float navigationUpdate;
    private Transform player;
    private float navigationTime = 0;
    //private bool playerMeetCheckPoint = false;
    private bool isPlayerMoveToCheckPoint = false;
    //private int damePlayerMax = 50;
    private int playerCurrentDame;
    private int gameGun = 0;
    private int playerUseBomb = 0;
    private int playerEatBomb = 1;
    private float explosionRadius = 8.0f;
    Vector3 direction = Vector3.zero;
    Joystick js;
    float angles;

    void Awake() {
        js = FindObjectOfType(typeof(Joystick)) as Joystick;
        anim = this.GetComponent<Animator>();
        if (instance == null) {
            instance = this;
        }
        gameGun = PlayerPrefs.GetInt("gunName", gameGun);
    }
    void Start() {
        player = GetComponent<Transform>();
        //UpdateCamera();
        playerCurrentDame = 10;
		currentHealth = startingHealth;
		isDead = false;
		SetHeal();
    }

    public int PlayerCurrentDame {
        get {
            return playerCurrentDame;
        }
    }

	void TakeDamage(float damageAmount) {
        currentHealth -= damageAmount;
        Debug.Log(currentHealth);
		if (currentHealth <= 0.0f) {
			GameManger.instance.tankGet0();
			SetExplosionPlayerEffect();
			//Instantiate(explosionPlayerEffect,transform.position,transform.rotation);
			Destroy(gameObject);
		}

		SetHeal();
	}

	void SetHeal() {
		tankSlider.value = currentHealth;
		fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentHealth / startingHealth);
	}

	// Update is called once per frame
	void Update () {
        if (EventGameManager.instance.BossKill == false) {
            isPlayerMoveToCheckPoint = false;
        }
		if (isPlayerEatItem == true) {
			timeEffectItemInGame += Time.deltaTime;
		}
		if (timeEffectItemInGame > 10.0f) {
			isPlayerEatItem = false;
			timeEffectItemInGame = 0.0f;
		}
        forwardMoveAmount = Input.GetAxis("Vertical") * speed;
        turnForce = Input.GetAxis("Horizontal") * rotationForce;
        transform.Rotate(0, 0, -turnForce);
        transform.position += transform.right * forwardMoveAmount * Time.deltaTime;
        if (Input.GetKeyDown("f")) {
            Shoot();
        }
	    GetDirection();
		if (direction != Vector3.zero) {
			var ang = Vector2.Angle(new Vector2(1, 0), (Vector2)direction);
			angles = (direction.y >= 0) ? ang : -ang;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angles), 0.5f);
			transform.parent.Translate(direction * speed * Time.deltaTime);
            anim.SetBool("isRunning",true);
		}
        else {
            anim.SetBool("isRunning", false);
        }

	}
    void GetDirection(){
        var x = js.GetXPos();
        var y = js.GetYPos();
        direction = new Vector3(x, y, 0);
    }

    public void Shoot() {
        if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
            if (gameGun == 0) {
				Instantiate(bulletPrefab, bulletSpawm[1].position, bulletSpawm[1].rotation);
            }
            if (gameGun == 1) {
                Instantiate(bulletPrefab, bulletSpawm[0].position, bulletSpawm[0].rotation);
                Instantiate(bulletPrefab, bulletSpawm[2].position, bulletSpawm[2].rotation);
            }
            if (gameGun == 2) {
                float angles = transform.eulerAngles.z - 45;
                for (int i = 0; i < 3; i++)
                {
                    var qAngle = Quaternion.Euler(0, 0, angles);
                    GameObject bullet = Instantiate(bulletPrefab, bulletSpawm[1].transform.position, qAngle);
					angles += 45;
                }
            }
            if (gameGun == 3) {
                float angles = transform.eulerAngles.z + 45;
                for (int i = 0; i < 4; i++) {
                    var qAngle = Quaternion.Euler(0, 0, angles);
                    if (i == 0 || i == 1) {
                        Instantiate(bulletPrefab, bulletSpawm[0].transform.position, qAngle);
                        angles -= 45;
                    }
                    if (i == 2 || i == 3) {
                        Instantiate(bulletPrefab, bulletSpawm[2].transform.position, qAngle);
                        angles += 45;
                    }
                }
            }
            if (gameGun ==  4) {
                float angles = transform.eulerAngles.z - 60;
				for (int i = 0; i < 5; i++)
				{
					var qAngle = Quaternion.Euler(0, 0, angles);
					Instantiate(bulletPrefab, bulletSpawm[1].transform.position, qAngle);
					angles += 30;
				}
            }
            if (gameGun == 5) {
                float angles = transform.eulerAngles.z + 60;
                for (int i = 0; i < 6; i++) {
					var qAngle = Quaternion.Euler(0, 0, angles);
					if (i == 0 || i == 1 || i == 2) {
						Instantiate(bulletPrefab, bulletSpawm[0].transform.position, qAngle);
						angles -= 30;
					}
					if (i == 3 || i == 4 || i == 5) {
						Instantiate(bulletPrefab, bulletSpawm[2].transform.position, qAngle);
						angles -= 30;
					}
                }
            }
            if (gameGun == 6) {
                float angles = transform.eulerAngles.z - 60 ;
                for (int i = 0; i < 7; i++) {
					var qAngle = Quaternion.Euler(0, 0, angles);
					Instantiate(bulletPrefab, bulletSpawm[1].transform.position, qAngle);
					angles += 15;
                }
            }
            if (attackSound != null) {
                SetAttackSound();
            }
        }
    }

    public bool IsPlayerMoveToCheckPoint {
        get {
            return isPlayerMoveToCheckPoint;
        }
    }

    void LateUpdate() {
        UpdateCamera();
    } 

    void UpdateCamera() {
        if (mainCamera == null) {
            return;
        }
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -30.0f);
    }

    public void Boom() {
        Instantiate(boomPrefab, bulletSpawm[1].position, bulletSpawm[1].rotation);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var col in colliders) {
            if (col.gameObject.tag == "Enemy") {
                SetExplosionEnemyEffect(col.gameObject.transform);
                Destroy(col.gameObject);
                int index = Random.Range(0, 4);
                Instantiate(itemDrop[index],col.gameObject.transform.position,itemDrop[index].transform.rotation);
                GameManger.instance.calculateScore(1);
            }
        }
        playerUseBomb--;
		if (playerUseBomb == 0) {
			playerEatBomb = 1;
			boombButton.SetActive(false);
		}
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "checkPointNextLevel" && EventGameManager.instance.BossKill == true) {
			isPlayerMoveToCheckPoint = true;//player move to next level completely
        }
        if (other.tag == "bulletPowerItem") {
            playerEatBomb++;
			if (playerEatBomb % 5 == 0) {
				playerUseBomb++;
				boombButton.SetActive(true);
			}
            Destroy(other.gameObject);
        }
        if (other.tag == "Coin") {
            GameManger.instance.calculatorCoin(1);
            Destroy(other.gameObject);
        }
		if (other.tag == "healthItem") {
			currentHealth += 10;
			SetHeal();
			Destroy(other.gameObject);
		}
		if (other.tag == "gainDefense") {
			isPlayerEatItem = true;
			Destroy(other.gameObject);
		}
    }

    private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "ememyBullet") {
			if (isPlayerEatItem == true) {
				TakeDamage(0.0f);
				Destroy(other.gameObject);
			}
			else {
				TakeDamage(10.0f);
				Destroy(other.gameObject);
			}
		}
    }
}