using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonInfo : EnemyInfo
{
    public struct Bullet
    {
        public GameObject prefab;
        public int speed;

        public Bullet(GameObject prefab, int speed)
        {
            this.prefab = prefab;
            this.speed = speed;
        }
    }

    [Header("Projectile Reference")]
    [SerializeField] private protected BulletInfo bullet_Info;
    public BulletInfo bulletInfo => bullet_Info;

    [Header("Projectile Pooling")]
    [SerializeField] private protected int sizeOfProjectilePool = 12;
    public int SizeOfProjectilePool => sizeOfProjectilePool;

    


}
