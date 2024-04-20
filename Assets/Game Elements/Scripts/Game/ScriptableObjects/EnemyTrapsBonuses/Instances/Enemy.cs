using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SpawnElement
{

    [Header("Attack damage")]
    [SerializeField] private protected bool haveAttackDamage = true;
    public bool HasAttackDamage => haveAttackDamage;
    [Header("Vision zone parameters")]
    [SerializeField] private protected float visionZoneX = 18f;
    public float VisionZoneX => visionZoneX;
    [SerializeField] private protected float visionZoneY = 7f;
    public float VisionZoneY => visionZoneY;
    [SerializeField] private protected float visionZoneZ = 43f;
    public float VisionZoneZ => visionZoneZ;

    [Header("Attack zone parameters")]
    [SerializeField] private protected float attackZoneX = 18f;
    public float AttackZoneX => attackZoneX;
    [SerializeField] private protected float attackZoneY = 7f;
    public float AttackZoneY => attackZoneY;
    [SerializeField] private protected float attackZoneZ = 24f;
    public float AttackZoneZ => attackZoneZ;

    public Enemy(EnemyInfo info) : base(info)
    {
        this.haveAttackDamage = info.HasAttackDamage;
        this.visionZoneX = info.VisionZoneX;
        this.visionZoneY = info.VisionZoneY;
        this.visionZoneZ = info.VisionZoneZ;
        this.attackZoneX = info.AttackZoneX;
        this.attackZoneY = info.AttackZoneY;
        this.attackZoneZ = info.AttackZoneZ;
    }

    public virtual void Attack(Vector3 target) { }
}
