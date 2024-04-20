using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoBonusesModificatorInfo", menuName = "LevelProperties/Modificators/New NoBonusesModificatorInfo")]
public class NoBonusesModificatorInfo : ModificatorInfo
{
    public override StagesSpawnParameters Action(StagesSpawnParameters spawnParameters)
    {
        base.Action(spawnParameters);
        spawnParameters.min_bonuses1 = 0;
        spawnParameters.min_bonuses2 = 0;
        spawnParameters.min_bonuses3 = 0;

        spawnParameters.max_bonuses1 = 0;
        spawnParameters.max_bonuses2 = 0;
        spawnParameters.max_bonuses3 = 0;

        return spawnParameters;
    }
}
