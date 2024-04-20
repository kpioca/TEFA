using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoTrapsModificatorInfo", menuName = "LevelProperties/Modificators/New NoTrapsModificatorInfo")]
public class NoTrapsModificatorInfo : ModificatorInfo
{
    public override StagesSpawnParameters Action(StagesSpawnParameters spawnParameters)
    {
        base.Action(spawnParameters);
        spawnParameters.min_traps1 = 0;
        spawnParameters.min_traps2 = 0;
        spawnParameters.min_traps3 = 0;

        spawnParameters.max_traps1 = 0;
        spawnParameters.max_traps2 = 0;
        spawnParameters.max_traps3 = 0;

        return spawnParameters;
    }
}
