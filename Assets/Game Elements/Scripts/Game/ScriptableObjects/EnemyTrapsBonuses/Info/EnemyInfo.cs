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

}
