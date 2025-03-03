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

    [SerializeField] private protected float attackStopZ = -5f;
    public float AttackStopZ => attackStopZ;

    [Header("Spawn Island")]
    [SerializeField] private protected bool hasSpawnIsland = true;
    public bool HasSpawnIsland => hasSpawnIsland;

    private protected Stamp stamp;
    public GameObject instance { get; set; }

    public Enemy(EnemyInfo info, GameObject instance, Stamp stamp = null) : base(info)
    {
        haveAttackDamage = info.HasAttackDamage;
        visionZoneX = info.VisionZoneX;
        visionZoneY = info.VisionZoneY;
        visionZoneZ = info.VisionZoneZ;
        attackZoneX = info.AttackZoneX;
        attackZoneY = info.AttackZoneY;
        attackZoneZ = info.AttackZoneZ;
        hasSpawnIsland = info.HasSpawnIsland;
        attackStopZ = info.AttackStopZ;

        this.instance = instance;
        this.stamp = stamp;
    }

    public virtual void Attack(Vector3 target) { }
}
