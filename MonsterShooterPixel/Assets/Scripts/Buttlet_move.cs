using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttlet_move : SoundAndEffectController {
    [SerializeField] private float speed;
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private GameObject[] itemDrop;//index = 0 is heal, idex = 1 is boom, index= 2 defense and idex = 3 is coin
    //[SerializeField] private GameObject explosionContructionEffect;
    //[SerializeField] private GameObject explosionEnemyEffect;
    private float startTime;

    private void Awake() {
        startTime = Time.time;
    }

    // Use this for initialization
    void Update () {
        transform.Translate(speed * Time.deltaTime,0,0);
        if (Time.time - startTime > bulletLifeTime) {
            Destroy(gameObject);
        }
	}

    private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Contruction")
		{
			//Instantiate(explosionContructionEffect, transform.position, transform.rotation);
            SetExplosionContructionEffect();
			Destroy(gameObject);
		}
		if (other.gameObject.tag == "boundary") {
            Debug.Log("a");
            //Instantiate(explosionContructionEffect,transform.position,transform.rotation);
            SetExplosionContructionEffect();
			Destroy(gameObject);
		}
        if (other.gameObject.tag == "Door") {
            //Instantiate(explosionContructionEffect,transform.position,transform.rotation);
            SetExplosionContructionEffect();
            Destroy(gameObject);
        }

		if (other.gameObject.tag == "Enemy") {
			GameManger.instance.calculateScore(1);
            int a = 0;
            a++;
            //Instantiate(explosionEnemyEffect, transform.position, transform.rotation);
            SetExplosionEnemyEffect(other.gameObject.transform);
			//Destroy(other.gameObject);
			Destroy(gameObject);
            int index = Random.Range(0, 4);
            Instantiate(itemDrop[index], other.gameObject.transform.position, itemDrop[index].transform.rotation);
			//EventGameManager.instance.PlayerKillEnemy();
		}
    }

	//private void OnTriggerEnter2D(Collider2D other) {
	//    if (other.gameObject.tag == "Contruction") {
	//        GameManger.instance.calculateScore(3);
	//        Instantiate(explosionContructionEffect, transform.position, transform.rotation);
	//        Destroy(gameObject);
	//    }
	//    if (other.gameObject.tag == "boundary") {
	//        Destroy(gameObject);
	//    }
	//    if (other.gameObject.tag == "Enemy") {
	//        GameManger.instance.calculateScore(5);
	//        Destroy(other.gameObject);
	//        Instantiate(explosionEnemyEffect,transform.position,transform.rotation);
	//        Destroy(gameObject);
	//        EventGameManager.instance.PlayerKillEnemy();
	//    }
	//}
	//private void OnDestroy() {
	//	EventGameManager.instance.PlayerKillEnemy();
	//}
}
