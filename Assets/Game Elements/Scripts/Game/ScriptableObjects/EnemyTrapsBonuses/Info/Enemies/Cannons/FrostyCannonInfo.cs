using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FrostyCannon_Properties", menuName = "LevelProperties/Enemy/New FrostyCannon_Properties")]
public class FrostyCannonInfo : CannonInfo
{
    public override Enemy createEnemy(GameObject cannonObject, Stamp stamp, GameObject[] objParameters, out Dictionary<string, float> numParameters)
    {
        numParameters = null;
        FrostyCannon cannon = new FrostyCannon(this, cannonObject, stamp);
        return cannon;
    }
}
