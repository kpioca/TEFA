using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoBonusesModificatorInfo", menuName = "LevelProperties/Modificators/New NoBonusesModificatorInfo")]
public class NoBonusesModificatorInfo : ModificatorInfo
{
    public override StagesSpawnParameters Action(StagesSpawnParameters spawnParameters)
    {
        base.Action(spawnParameters);

        int n = spawnParameters.min_bonuses.Length;

        for (int i = 0; i < n; i++)
        {
            spawnParameters.min_bonuses[i] = 0;
            spawnParameters.max_bonuses[i] = 0;
        }

        return spawnParameters;
    }
}
