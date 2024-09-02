using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoTrapsModificatorInfo", menuName = "LevelProperties/Modificators/New NoTrapsModificatorInfo")]
public class NoTrapsModificatorInfo : ModificatorInfo
{
    public override StagesSpawnParameters Action(StagesSpawnParameters spawnParameters)
    {
        base.Action(spawnParameters);

        int n = spawnParameters.min_traps.Length;

        for(int i = 0; i < n; i++)
        {
            spawnParameters.min_traps[i] = 0;
            spawnParameters.max_traps[i] = 0;
        }

        return spawnParameters;
    }
}
