using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapInfo : SpawnElementInfo
{
    [Header("Icon")]
    [SerializeField] private protected Sprite icon;
    public Sprite Icon => icon;

    [Header("Attack damage")]
    [SerializeField] private protected bool haveAttackDamage;
    public bool HasAttackDamage => haveAttackDamage;

    [SerializeField] private protected int attackDamage;
    public int AttackDamage => attackDamage;

    [Header("Effects")]
    [SerializeField] private protected bool hasEffect;
    public bool HasEffect => hasEffect;

    [SerializeField] private protected bool ignoreArmor = false;

    public bool IgnoreArmor => ignoreArmor;


    [SerializeField] protected StatusEffectInfo effectInfo;
    public StatusEffectInfo EffectInfo => effectInfo;

    [Header("Other")]
    [SerializeField] private protected bool doHoleInRoad;
    public bool DoHoleInRoad => doHoleInRoad;
}
