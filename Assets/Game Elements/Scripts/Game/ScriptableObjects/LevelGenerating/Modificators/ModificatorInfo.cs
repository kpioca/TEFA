using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ModificatorInfo : ScriptableObject
{
    [SerializeField] private string nameModificator = "";
    public string NameModificator => nameModificator;
    [SerializeField] private int levelOfCoolness = 1;

    public int LevelOfCoolness => levelOfCoolness;

    [Header("For Fish Multiplier")]
    [Range(0f, 10f)]
    [SerializeField] private protected float multiplier = 0;
    public float Multiplier => multiplier;
    public virtual StagesSpawnParameters Action(StagesSpawnParameters spawnParameters)
    {
        Debug.Log($"MODIFICATOR '{nameModificator}' IS ACTIVATED ");
        return spawnParameters;
    }
}
