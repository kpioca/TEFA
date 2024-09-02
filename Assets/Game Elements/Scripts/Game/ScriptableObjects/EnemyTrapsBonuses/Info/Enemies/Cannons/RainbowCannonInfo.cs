using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RainbowCannon_Properties", menuName = "LevelProperties/Enemy/New RainbowCannon_Properties")]
public class RainbowCannonInfo : CannonInfo
{

    [SerializeField] float intervalBetweenShots = 0.5f;
    public float IntervalBetweenShots => intervalBetweenShots;


}
