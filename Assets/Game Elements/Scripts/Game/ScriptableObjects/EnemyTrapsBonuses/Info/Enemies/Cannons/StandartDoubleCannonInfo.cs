using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StandartDoubleCannon_Properties", menuName = "LevelProperties/Enemy/New StandartDoubleCannon_Properties")]
public class StandartDoubleCannonInfo : CannonInfo
{

    [SerializeField] float intervalBetweenShots = 1f;
    public float IntervalBetweenShots => intervalBetweenShots;

    public override Enemy createEnemy(GameObject cannonObject, Stamp stamp, GameObject[] objParameters, out Dictionary<string, float> numParameters)
    {
        numParameters = new Dictionary<string, float>();
        float intervalBetweenShots = 0;
        StandartDoubleCannon cannon = new StandartDoubleCannon(this, out intervalBetweenShots, cannonObject, stamp);
        numParameters["intervalBetweenShots"] = intervalBetweenShots;
        numParameters["n_shots"] = 2;
        return cannon;
    }
}
