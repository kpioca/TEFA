using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RPattern", menuName = "LevelProperties/Generation/New RoadGenerationPattern")]
public class RoadGenerationPattern : ScriptableObject
{
    [SerializeField] string id;
    public string Id => id;

    [Header("Traps")]
    [Range(1, 12)]
    [SerializeField] int[] trapNumSpawnPlaces;

    public int[] TrapNumSpawnPlaces => trapNumSpawnPlaces;

    [Header("Bonuses")]
    [Range(1, 12)]
    [SerializeField] int[] bonusNumSpawnPlaces;

    public int[] BonusNumSpawnPlaces => bonusNumSpawnPlaces;


}
