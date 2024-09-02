using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddTrapsModificatorInfo", menuName = "LevelProperties/Modificators/New AddTrapsModificatorInfo")]
public class AddTrapsModificatorInfo : ModificatorInfo
{
    [SerializeField] int[] increaseMinValueStages;
    [SerializeField] int[] increaseMaxValueStages;
    public override StagesSpawnParameters Action(StagesSpawnParameters spawnParameters)
    {
        base.Action(spawnParameters);
        int n = spawnParameters.min_traps.Length;

        for (int i = 0; i < n; i++)
        {
            spawnParameters.min_traps[i] += increaseMinValueStages[i];
            spawnParameters.max_traps[i] += increaseMaxValueStages[i];
        }

        return spawnParameters;
    }
}
