using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CorAngelCannon_Properties", menuName = "LevelProperties/Enemy/New CorAngelCannon_Properties")]
public class CorAngelCannonInfo : CannonInfo
{
    public override Enemy createEnemy(GameObject cannonObject, Stamp stamp, GameObject[] objParameters, out Dictionary<string, float> numParameters)
    {
        numParameters = null;
        CorAngelCannon cannon = new CorAngelCannon(this, cannonObject, stamp);
        return cannon;
    }
}
