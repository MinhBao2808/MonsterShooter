using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    protected int wayShot = 8;
    protected float startTime;
    [SerializeField] protected float timeBetweenShot;
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected GameObject spawmBulletPoint;
    [SerializeField] protected Transform bossTransform;
}
