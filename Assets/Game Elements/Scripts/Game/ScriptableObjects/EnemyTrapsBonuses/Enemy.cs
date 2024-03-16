using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SpawnElement
{
    public enum enemyType
    {
        cannon,
        marine
    };


    [Header("Attack damage")]
    [SerializeField] private protected bool haveAttackDamage = true;
    public bool HasAttackDamage => haveAttackDamage;
    [Header("Type of enemy")]
    [SerializeField] private protected enemyType type = enemyType.cannon;
    public enemyType Type => type;
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
    public virtual void Attack(Vector3 target) { }

}
