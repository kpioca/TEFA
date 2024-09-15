using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MageBattleAngelInfo", menuName = "LevelProperties/Enemy/New MageBattleAngelInfo")]
public class MageBattleAngelInfo : MageBattleInfo
{
    public override Enemy createEnemy(GameObject mageObject, Stamp stamp, GameObject[] objParameters, out Dictionary<string, float> numParameters)
    {
        numParameters = null;
        MageBattleAngel mage = new MageBattleAngel(this, mageObject);
        return mage;
    }
}
