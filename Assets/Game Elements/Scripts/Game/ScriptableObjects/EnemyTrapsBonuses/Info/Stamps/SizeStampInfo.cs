using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SizeStampInfo", menuName = "LevelProperties/Stamps/New SizeStamp_Properties")]
public class SizeStampInfo : StampInfo
{
    [Header("Features")]
    [Range(1f, 5f)]
    [SerializeField] float size_Multiplier = 1.5f;
    public float sizeMultiplier => size_Multiplier;

}
