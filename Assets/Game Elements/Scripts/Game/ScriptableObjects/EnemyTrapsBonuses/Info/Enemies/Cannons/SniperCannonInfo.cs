using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SniperCannon_Properties", menuName = "LevelProperties/Enemy/New SniperCannon_Properties")]
public class SniperCannonInfo : CannonInfo
{
    public override Enemy createEnemy(GameObject cannonObject, Stamp stamp, GameObject[] objParameters, out Dictionary<string, float> numParameters)
    {
        numParameters = null;
        SniperCannon cannon = new SniperCannon(this, cannonObject, stamp);
        return cannon;
    }
}
