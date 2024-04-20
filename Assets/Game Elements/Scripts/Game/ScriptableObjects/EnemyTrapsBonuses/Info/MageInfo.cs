using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageInfo : EnemyInfo
{
    [Header("Spell Pull")]
    [SerializeField] MageSpellInfo[] spellPull;
    public MageSpellInfo[] SpellPull => spellPull;
    

}
