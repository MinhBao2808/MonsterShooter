using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_health : MonoBehaviour {

	[SerializeField] private GameObject explosionPlayerEffect;

    private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "playerBullet") {
            Debug.Log(Tank_moving.instance.PlayerCurrentDame);
            EventGameManager.instance.BossGetDame(Tank_moving.instance.PlayerCurrentDame);
			if (EventGameManager.instance.CurrentHealth <= 0) {
				Instantiate(explosionPlayerEffect, transform.position, transform.rotation);
				Destroy(gameObject);
				EventGameManager.instance.BossIsDead();
			}

			EventGameManager.instance.SetHeal();
			Destroy(other.gameObject);
		}
    }

 //   private void OnTriggerEnter2D(Collider2D collision)
	//{
	//	if (collision.gameObject.tag == "playerBullet")
	//	{
	//		EventGameManager.instance.BossGetDame(10);
 //           if (EventGameManager.instance.CurrentHealth <= 0) {
	//			Instantiate(explosionPlayerEffect, transform.position, transform.rotation);
	//			Destroy(gameObject);
 //               EventGameManager.instance.BossIsDead();
	//		}

	//		EventGameManager.instance.SetHeal();
	//		Destroy(collision.gameObject);
	//	}
	//}
}
