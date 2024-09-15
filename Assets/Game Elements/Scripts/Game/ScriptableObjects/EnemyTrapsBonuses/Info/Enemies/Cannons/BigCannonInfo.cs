using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BigCannon_Properties", menuName = "LevelProperties/Enemy/New BigCannon_Properties")]
public class BigCannonInfo : CannonInfo
{
    public override Enemy createEnemy(GameObject cannonObject, Stamp stamp, GameObject[] objParameters, out Dictionary<string, float> numParameters)
    {
        numParameters = null;
        BigCannon cannon = new BigCannon(this, cannonObject, stamp);
        return cannon;
    }
}
