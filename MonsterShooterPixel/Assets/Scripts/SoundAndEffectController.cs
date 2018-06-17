using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAndEffectController : MonoBehaviour {
    [SerializeField] protected AudioSource attackSound;
    [SerializeField] protected GameObject explosionContructionEffect;
    [SerializeField] protected GameObject explosionEnemyEffect;
    [SerializeField] protected GameObject explosionPlayerEffect;

    protected void SetAttackSound() {
		if (attackSound != null) {
			var sound = (AudioSource)Instantiate(attackSound, transform.position, transform.rotation);
			sound.Play();
			Destroy(sound.gameObject, sound.clip.length);
		}
    }

    protected void SetExplosionContructionEffect () {
        Destroy(Instantiate(explosionContructionEffect, transform.position, transform.rotation), 1.33f);
    }

    protected void SetExplosionEnemyEffect (Transform other) {
        Destroy(Instantiate(explosionEnemyEffect, other.position, other.rotation), 1.33f);
    }

    protected void SetExplosionPlayerEffect () {
        Destroy(Instantiate(explosionPlayerEffect, transform.position, transform.rotation), 1.33f);
    }

}
