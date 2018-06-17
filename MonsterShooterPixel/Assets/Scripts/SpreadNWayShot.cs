using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadNWayShot : BulletController {
    //private int wayShot = 8;
    //private float startTime;
    //[SerializeField] private float timeBetweenShot;
    //[SerializeField] private GameObject bullet;
    //[SerializeField] private GameObject spawmBulletPoint;
    //[SerializeField] private Transform bossTransform;
    private void Awake() {
        startTime = Time.time;
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(ShotBullet()); 
	}

    IEnumerator ShotBullet() {
        if (GameManger.instance.GameOver == false) {
            yield return new WaitForSeconds(0.5f);
            float angles = bossTransform.eulerAngles.z;
            for (int i = 0; i < wayShot; i++) {
                angles -= 23;
                var qAngle = Quaternion.Euler(0, 0, angles);
                GameObject bulletPrefab = Instantiate(bullet, spawmBulletPoint.transform.position, qAngle);
                //bulletPrefab.GetComponent<Rigidbody2D>().velocity = bulletPrefab.transform.forward;
                //indexShot++;
            }
            yield return new WaitForSeconds(EventGameManager.instance.TimeToShotBetweenToShotOfBossBullet);
            StartCoroutine(ShotBullet());
        }
    }
}
