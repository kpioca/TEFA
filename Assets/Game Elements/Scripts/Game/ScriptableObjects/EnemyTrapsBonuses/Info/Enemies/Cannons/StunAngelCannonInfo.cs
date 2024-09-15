using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StunAngelCannon_Properties", menuName = "LevelProperties/Enemy/New StunAngelCannon_Properties")]
public class StunAngelCannonInfo : CannonInfo
{
    public override Enemy createEnemy(GameObject cannonObject, Stamp stamp, GameObject[] objParameters, out Dictionary<string, float> numParameters)
    {
        numParameters = null;
        StunAngelCannon cannon = new StunAngelCannon(this, cannonObject, stamp);
        return cannon;
    }
}
