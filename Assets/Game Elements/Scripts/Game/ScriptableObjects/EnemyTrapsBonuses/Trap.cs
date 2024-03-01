using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : SpawnElement
{
    [Header("Attack damage")]
    [SerializeField] private protected bool haveAttackDamage;
    public bool HasAttackDamage => haveAttackDamage;

    public virtual void Action() { }
}
