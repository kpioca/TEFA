using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Enemy
{
    [Header("Projectile Instance")]
    [SerializeField] private protected GameObject prefabProjectile;
    public GameObject PrefabProjectile => prefabProjectile;

    [Header("Projectile Pooling")]
    [SerializeField] private protected int sizeOfProjectilePool = 12;
    public int SizeOfProjectilePool => sizeOfProjectilePool;
}
