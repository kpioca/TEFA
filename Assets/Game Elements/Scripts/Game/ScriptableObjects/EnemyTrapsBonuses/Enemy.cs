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
    public virtual void Attack() { }
}
