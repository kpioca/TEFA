using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageStampInfo", menuName = "LevelProperties/Stamps/New DamageStamp_Properties")]
public class DamageStampInfo : StampInfo
{
    [Header("Features")]
    [Range(1, 5)]
    [SerializeField] int damage_Increase = 1;
    public int damageIncrease => damage_Increase;

}
