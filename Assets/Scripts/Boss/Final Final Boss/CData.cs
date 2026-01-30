using System;
using UnityEngine;

[Serializable]
public class CData 
{
    public CientificForceField ff;
    public Transform leftMost;
    public Transform middle;
    public Transform rightMost;
    public RayCannon rayCannon;
    public Rigidbody2D rb2d;
    public float runSpeed;
    public float speedCap;
    public GameObject laserBullet;
    public Transform kkPos;
    public Transform shootPoint;
    public int laserGunShots;
    public float laserGunCooldown;
    public float vulnerableTime;
    public int lives;
    public int targetPosition;
    public float targetTolerance;
    public BoxSpawner boxSpawner;
}
