using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StandartDoubleCannon_Properties", menuName = "LevelProperties/Enemy/New StandartDoubleCannon_Properties")]
public class StandartDoubleCannonInfo : CannonInfo
{

    [SerializeField] float intervalBetweenShots = 1f;
    public float IntervalBetweenShots => intervalBetweenShots;

}
