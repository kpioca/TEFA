using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MageTransformAngelInfo", menuName = "LevelProperties/Enemy/New MageTransformAngelInfo")]
public class MageTransformAngelInfo : MageInfo
{
    public override Enemy createEnemy(GameObject mageObject, Stamp stamp, GameObject[] objParameters, out Dictionary<string, float> numParameters)
    {
        numParameters = null;
        MageTransformAngel mage = new MageTransformAngel(this, mageObject);
        return mage;
    }
}
