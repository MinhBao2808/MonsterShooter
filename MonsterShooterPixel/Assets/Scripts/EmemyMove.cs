using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemyMove : MonoBehaviour {
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawm;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private LayerMask layer;
    [SerializeField] private AudioSource enemyAttackSound;
    private Animator anim;
    private float range = 15.0f; 
    private float distance = 2.0f;
    private bool isEnemyFindPlayer = false;
    private bool isThereAnyThing = false;
    private Quaternion targetRotation = Quaternion.identity;
    private float objectSizeX;
    private float objectSizeY;
    private bool isFindContruction = false;

    void Awake() {
        anim = GetComponent<Animator>();
    }

    //// Use this for initialization
    void Start () {
        //StartCoroutine(EnemyShoot());
        isEnemyFindPlayer = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManger.instance.GameOver == false) {
            var target = player.transform.position;
           
            if (isEnemyFindPlayer == true && isFindContruction == false) {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, AngleBetweenVector2(transform.position, player.transform.position)), 0.5f);
                target.x += distance;
                target.y -= distance;
                Debug.Log(transform.position);
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
            //if (isFindContruction == true && isEnemyFindPlayer == false) {
            //    //var target = new Vector2();
            //    //target.x = objectSizeX - 3.0f;
            //    //target.y = objectSizeY;
            //    target.x = objectSizeX;
            //    target.y = objectSizeY;
            //    transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            //    if (Vector3.Distance(transform.position, target) < 1.0f)
            //    {
            //        isEnemyFindPlayer = true;
            //        isFindContruction = false;
            //    }
            //}
        }
	}

    IEnumerator  EnemyShoot() {
        if (GameManger.instance.GameOver == false) {
            yield return new WaitForSeconds(0.1f);
            if (enemyAttackSound != null) {
                var sound = (AudioSource)Instantiate(enemyAttackSound, transform.position, transform.rotation);
				sound.Play();
				Destroy(sound.gameObject, sound.clip.length);
			}
            Instantiate(bulletPrefab, bulletSpawm.position, bulletSpawm.rotation);
            yield return new WaitForSeconds(2.0f);
            StartCoroutine(EnemyShoot());
        }
    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2) {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "playerBullet") {
			//Instantiate(explosionEnemyEffect, transform.position, transform.rotation);
			//SetExplosionEnemyEffect();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (GameManger.instance.GameOver == false) {
            if (other.gameObject.tag == "Player") {
                anim.SetBool("isRunning",true);
                isEnemyFindPlayer = true;
                if (isFindContruction == false) {
                    StartCoroutine(EnemyShoot());
                }
            }
            //if (other.gameObject.tag == "Contruction") {
            //    objectSizeX = other.gameObject.transform.position.x;
            //    objectSizeY = other.gameObject.transform.position.y;
            //    if (transform.eulerAngles.z >= 90 && transform.eulerAngles.z < 180) {
            //        transform.rotation = Quaternion.Euler(0, 0, AngleBetweenVector2(transform.position, other.gameObject.transform.position) + 40.0f);
            //        objectSizeX = objectSizeX - 3.0f;
            //    }
            //    if (transform.eulerAngles.z >= 180 && transform.eulerAngles.z < 270) {
            //        transform.rotation = Quaternion.Euler(0, 0, AngleBetweenVector2(transform.position, other.gameObject.transform.position) + 50.0f);
            //        objectSizeY = objectSizeY - 3.0f;
            //    }
            //    if (transform.eulerAngles.z >= 270 && transform.eulerAngles.z < 360) {
            //        transform.rotation = Quaternion.Euler(0, 0, AngleBetweenVector2(transform.position, other.gameObject.transform.position) + 60.0f);
            //        objectSizeX = objectSizeX + 3.0f;
            //    }
            //    if (transform.rotation.z >= 0 && transform.eulerAngles.z < 90) {
            //        transform.rotation = Quaternion.Euler(0, 0, AngleBetweenVector2(transform.position, other.gameObject.transform.position) + 60.0f);
            //        objectSizeX = objectSizeX - 3.0f;
            //    }
            //    isFindContruction = true;
            //    isEnemyFindPlayer = false;

            //}
        }
    }
	private void OnDestroy() {
		EventGameManager.instance.PlayerKillEnemy();
	}
}
