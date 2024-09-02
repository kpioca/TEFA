using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddEnemyModificatorInfo", menuName = "LevelProperties/Modificators/New AddEnemyModificatorInfo")]
public class AddEnemyModificatorInfo : ModificatorInfo
{
    [SerializeField] int[] increaseMinValueStages;
    [SerializeField] int[] increaseMaxValueStages;
    public override StagesSpawnParameters Action(StagesSpawnParameters spawnParameters)
    {
        base.Action(spawnParameters);
        int n = spawnParameters.min_enemies.Length;
        
        for(int i = 0; i < n; i++)
        {
            spawnParameters.min_enemies[i] += increaseMinValueStages[i];
            spawnParameters.max_enemies[i] += increaseMaxValueStages[i];
        }

        return spawnParameters;

    }
}
