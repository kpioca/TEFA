using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarrotCannon_Properties", menuName = "LevelProperties/Enemy/New CarrotCannon_Properties")]
public class CarrotCanInfo : CannonInfo
{
    [Header("Features")]
    [Range(0f, 1f)]
    [SerializeField] private protected float chance_bulletCollapse;
    public float chance_bullet_Collapse => chance_bulletCollapse;
}
