using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "StandartCannon_Properties", menuName = "LevelProperties/Enemy/New StandartCannon_Properties")]
public class StandartCannonInfo : CannonInfo
{
    public override Enemy createEnemy(GameObject cannonObject, Stamp stamp, GameObject[] objParameters, out Dictionary<string, float> numParameters)
    {
        numParameters = null;
        StandartCannon cannon = new StandartCannon(this, cannonObject, stamp);
        return cannon;
    }
}
