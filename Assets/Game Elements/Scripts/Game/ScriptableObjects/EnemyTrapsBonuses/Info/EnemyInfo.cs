using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfo : SpawnElementInfo
{

    [Header("Icon")]
    [SerializeField] private protected Sprite icon;
    public Sprite Icon => icon;

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

    [Header("Spawn parameters")]
    [SerializeField] private protected bool isCoordYChange = false;
    public bool IsCoordYChange => isCoordYChange;
    [SerializeField] private protected float newCoordY;
    public float NewCoordY => newCoordY;

    [Header("Spawn Island")]
    [SerializeField] private protected bool hasSpawnIsland = true;
    public bool HasSpawnIsland => hasSpawnIsland;

    public virtual Enemy createEnemy(GameObject enemyObject, Stamp stamp, GameObject[] objParameters, out Dictionary<string, float> numParameters)
    {
        numParameters = null;
        return null;
    }

    public virtual void setEnemyInContent(GameObject cannonObject, Stamp stamp, ContentEnemy contentEnemy)
    {
    }
    public virtual void setAttackEnemyZone(Enemy enemy, BoxCollider attackBox, GameObject attackZoneObj, ContentEnemy contentEnemy)
    {
        
    }


}
