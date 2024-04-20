using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddTrapsModificatorInfo", menuName = "LevelProperties/Modificators/New AddTrapsModificatorInfo")]
public class AddTrapsModificatorInfo : ModificatorInfo
{
    [SerializeField] int increaseMinValueStage1 = 1;
    [SerializeField] int increaseMaxValueStage1 = 1;

    [SerializeField] int increaseMinValueStage2 = 1;
    [SerializeField] int increaseMaxValueStage2 = 1;

    [SerializeField] int increaseMinValueStage3 = 1;
    [SerializeField] int increaseMaxValueStage3 = 1;
    public override StagesSpawnParameters Action(StagesSpawnParameters spawnParameters)
    {
        base.Action(spawnParameters);
        spawnParameters.min_traps1 += increaseMinValueStage1;
        spawnParameters.min_traps2 += increaseMinValueStage2;
        spawnParameters.min_traps3 += increaseMinValueStage3;

        spawnParameters.max_traps1 += increaseMaxValueStage1;
        spawnParameters.max_traps2 += increaseMaxValueStage2;
        spawnParameters.max_traps3 += increaseMaxValueStage3;

        return spawnParameters;
    }
}
