using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonInfo : EnemyInfo
{

    [Header("Projectile Reference")]
    [SerializeField] private protected BulletInfo[] bullets_Info;
    public BulletInfo[] bulletsInfo => bullets_Info;

    [Header("Projectile Pooling")]
    [SerializeField] private protected int sizeOfProjectilePool = 12;
    public int SizeOfProjectilePool => sizeOfProjectilePool;

    


}
