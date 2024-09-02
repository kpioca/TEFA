using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeedStampInfo", menuName = "LevelProperties/Stamps/New SpeedStamp_Properties")]
public class SpeedStampInfo : StampInfo
{
    [Header("Features")]
    [Range(1f, 5f)]
    [SerializeField] float speed_Multiplier = 1.5f;
    public float speedMultiplier => speed_Multiplier;

}
