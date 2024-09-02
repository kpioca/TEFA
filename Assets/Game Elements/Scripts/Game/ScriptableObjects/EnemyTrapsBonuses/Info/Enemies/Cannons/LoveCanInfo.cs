using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LoveCannon_Properties", menuName = "LevelProperties/Enemy/New LoveCannon_Properties")]
public class LoveCanInfo : CannonInfo
{
    [Header("Features")]
    [Range(1, 5)]
    [SerializeField] private protected int increaseSize_Projectile;
    public int increaseSizeProjectile => increaseSize_Projectile;
}
