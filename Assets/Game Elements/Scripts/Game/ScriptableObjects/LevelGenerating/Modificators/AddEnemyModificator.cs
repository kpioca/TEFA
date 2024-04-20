using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddEnemyModificatorInfo", menuName = "LevelProperties/Modificators/New AddEnemyModificatorInfo")]
public class AddEnemyModificatorInfo : ModificatorInfo
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
        spawnParameters.min_enemies1 += increaseMinValueStage1;
        spawnParameters.min_enemies2 += increaseMinValueStage2;
        spawnParameters.min_enemies3 += increaseMinValueStage3;

        spawnParameters.max_enemies1 += increaseMaxValueStage1;
        spawnParameters.max_enemies2 += increaseMaxValueStage2;
        spawnParameters.max_enemies3 += increaseMaxValueStage3;

        return spawnParameters;

    }
}
