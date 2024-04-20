using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBattleInfo : MageInfo
{
    [Header("Projectile Reference")]
    [SerializeField] private protected BulletInfo bullet_Info;
    public BulletInfo bulletInfo => bullet_Info;

    [Header("Projectile Pooling")]
    [SerializeField] private protected int sizeOfProjectilePool = 8;
    public int SizeOfProjectilePool => sizeOfProjectilePool;


}
