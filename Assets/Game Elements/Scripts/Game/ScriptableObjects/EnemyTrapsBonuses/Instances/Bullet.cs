using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet
{
    [SerializeField] private protected string id;
    public string Id => id;

    [Header("Reference")]
    [SerializeField] private protected GameObject prefab;
    public GameObject Prefab => prefab;

    [Header("Projectile Info")]
    [SerializeField] private protected int speed = 3;
    public int Speed => speed;

    [SerializeField] private protected int damage = 1;
    public int Damage => damage;

    [SerializeField] private protected bool hasEffect = false;

    public bool HasEffect => hasEffect;

    [SerializeField] private protected bool isBreakInCollision = false;

    public bool IsBreakInCollision => isBreakInCollision;

    [SerializeField] private protected bool ignoreArmor = false;

    public bool IgnoreArmor => ignoreArmor;

    [SerializeField] private protected StatusEffectInfo effectInfo = null;

    public StatusEffectInfo EffectInfo => effectInfo;

    public Bullet(BulletInfo info, int damageIncrease = 0, float speedMultiplier = 1)
    {
        id = info.Id;
        prefab = info.Prefab;
        speed = (int)(info.Speed * speedMultiplier);
        damage = info.Damage + damageIncrease;
        hasEffect = info.HasEffect;
        isBreakInCollision = info.IsBreakInCollision;
        ignoreArmor = info.IgnoreArmor;
        effectInfo = info.EffectInfo;
    }

}
