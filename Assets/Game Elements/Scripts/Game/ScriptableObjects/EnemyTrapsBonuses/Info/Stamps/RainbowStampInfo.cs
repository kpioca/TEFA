using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RainbowStampInfo", menuName = "LevelProperties/Stamps/New RainbowStamp_Properties")]
public class RainbowStampInfo : StampInfo
{
    [Header("Features")]
    [Range(1, 5)]
    [SerializeField] int damage_Increase = 1;
    public int damageIncrease => damage_Increase;

    [Range(1f, 5f)]
    [SerializeField] float speed_Multiplier = 1.5f;
    public float speedMultiplier => speed_Multiplier;

    [Range(1f, 5f)]
    [SerializeField] float size_Multiplier = 1.5f;
    public float sizeMultiplier => size_Multiplier;

}
