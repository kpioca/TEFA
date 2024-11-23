using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MushroomCan_Properties", menuName = "LevelProperties/Enemy/New MushroomCan_Properties")]
public class MushroomCanInfo : CannonInfo
{
    public override Enemy createEnemy(GameObject cannonObject, Stamp stamp, GameObject[] objParameters, out Dictionary<string, float> numParameters)
    {
        numParameters = null;
        MushroomCan cannon = new MushroomCan(this, cannonObject, stamp);
        return cannon;
    }
}
