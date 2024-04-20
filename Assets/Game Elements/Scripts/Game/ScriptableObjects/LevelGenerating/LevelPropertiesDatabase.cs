using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "LevelPropertiesDatabase", menuName = "LevelProperties/Databases/New LevelPropertiesDatabase")]
[Serializable]
public class LevelPropertiesDatabase : ScriptableObject
{
    [Header("Enemy Properties")]
    [SerializeField] private List<EnemyInfo> enemy_properties;
    public List<EnemyInfo> Enemy_properties => enemy_properties;

    private Dictionary<string, EnemyInfo> enemy_propertiesDict;
    public Dictionary<string, EnemyInfo> Enemy_propertiesDict
    {
        get
        {
            enemy_propertiesDict = new Dictionary<string, EnemyInfo>();
            int n = enemy_properties.Count;
            for(int i = 0; i < n; i++)
                enemy_propertiesDict.Add(enemy_properties[i].Id, enemy_properties[i]);
            return enemy_propertiesDict;
        }
    }

    [Header("Traps Properties")]
    [SerializeField] private List<TrapInfo> traps_properties;
    public List<TrapInfo> Traps_properties => traps_properties;

    private Dictionary<string, TrapInfo> traps_propertiesDict;
    public Dictionary<string, TrapInfo> Traps_propertiesDict
    {
        get
        {
            traps_propertiesDict = new Dictionary<string, TrapInfo>();
            int n = traps_properties.Count;
            for (int i = 0; i < n; i++)
                traps_propertiesDict.Add(traps_properties[i].Id, traps_properties[i]);
            return traps_propertiesDict;
        }
    }

    [Header("Bonuses Properties")]
    [SerializeField] private List<BonusInfo> bonuses_properties;
    public List<BonusInfo> Bonuses_properties => bonuses_properties;

    private Dictionary<string, BonusInfo> bonuses_propertiesDict;
    public Dictionary<string, BonusInfo> Bonuses_propertiesDict
    {
        get
        {
            bonuses_propertiesDict = new Dictionary<string, BonusInfo>();
            int n = bonuses_properties.Count;
            for (int i = 0; i < n; i++)
                bonuses_propertiesDict.Add(bonuses_properties[i].Id, bonuses_properties[i]);
            return bonuses_propertiesDict;
        }
    }

    [Header("Islands Properties")]
    [SerializeField] private List<ObjectProperties> islands_properties;
    public List<ObjectProperties> Islands_properties => islands_properties;

    private Dictionary<string, ObjectProperties> islands_propertiesDict;
    public Dictionary<string, ObjectProperties> Islands_propertiesDict
    {
        get
        {
            islands_propertiesDict = new Dictionary<string, ObjectProperties>();
            int n = islands_properties.Count;
            for (int i = 0; i < n; i++)
                islands_propertiesDict.Add(islands_properties[i].Id, islands_properties[i]);
            return islands_propertiesDict;
        }
    }

    [Header("RoadParts Properties")]
    [SerializeField] private List<ObjectProperties> roadParts_properties;
    public List<ObjectProperties> RoadParts_properties => roadParts_properties;

    private Dictionary<string, ObjectProperties> roadParts_propertiesDict;
    public Dictionary<string, ObjectProperties> RoadParts_propertiesDict
    {
        get
        {
            roadParts_propertiesDict = new Dictionary<string, ObjectProperties>();
            int n = roadParts_properties.Count;
            for (int i = 0; i < n; i++)
                roadParts_propertiesDict.Add(roadParts_properties[i].Id, roadParts_properties[i]);
            return roadParts_propertiesDict;
        }
    }

    [Header("Misc Properties")]
    [SerializeField] private List<ObjectProperties> misc_properties;
    public List<ObjectProperties> Misc_properties => misc_properties;

    private Dictionary<string, ObjectProperties> misc_propertiesDict;
    public Dictionary<string, ObjectProperties> Misc_propertiesDict
    {
        get
        {
            misc_propertiesDict = new Dictionary<string, ObjectProperties>();
            int n = misc_properties.Count;
            for (int i = 0; i < n; i++)
                misc_propertiesDict.Add(misc_properties[i].Id, misc_properties[i]);
            return misc_propertiesDict;
        }
    }

    [Header("Modificators")]

    [SerializeField] private List<ModificatorInfo> modificators = new List<ModificatorInfo>();
    public List<ModificatorInfo> Modificators => modificators;

    [Range(0, 1)]
    [SerializeField] private float chanceModificator = 0.05f;
    public float ChanceModificator => chanceModificator;

    [Header("Game sets")]

    [SerializeField] private List<GameSet> gameSets = new List<GameSet>();
    public List<GameSet> GameSets => gameSets;

    [Range(0, 1)]
    [SerializeField] private float chanceSpawnGameSet = 0.1f;
    public float ChanceSpawnGameSet => chanceSpawnGameSet;


    [Header("Other Info")]
    [SerializeField] private float distanceZ_between_roads = 15.98f;
    public float DistanceZ_between_roads => distanceZ_between_roads;

    [Range(0, 6)]
    [SerializeField] private int min_extraIslands = 1;
    public int Min_extraIslands => min_extraIslands;
    [Range(0, 6)]
    [SerializeField] private int max_extraIslands = 6;
    public int Max_extraIslands => max_extraIslands;


    [Space(30)]
    [Header("Start of generation")]
    [Range(0, 10)]
    [SerializeField] private int num_securePlatformsAtBegin = 2;
    public int Num_securePlatformsAtBegin => num_securePlatformsAtBegin;
    [Range(0, 10)]
    [SerializeField] private int num_allSpawnedPlatforms = 4;
    public int Num_allSpawnedPlatforms => num_allSpawnedPlatforms;
    [Header("Enemy limits for generation")]
    [Range(0, 30)]
    [SerializeField] private int min_typeEnemy = 2;
    public int Min_typeEnemy => min_typeEnemy;
    [Range(0, 30)]
    [SerializeField] private int max_typeEnemy = 5;
    public int Max_typeEnemy => max_typeEnemy;

    [Header("Traps limits for generation")]
    [Range(0, 30)]
    [SerializeField] private int min_typeTraps = 1;
    public int Min_typeTraps => min_typeTraps;
    [Range(0, 30)]
    [SerializeField] private int max_typeTraps = 1;
    public int Max_typeTraps => max_typeTraps;

    [Header("Bonuses limits for generation")]
    [Range(0, 30)]
    [SerializeField] private int min_typeBonuses = 1;
    public int Min_typeBonuses => min_typeBonuses;
    [Range(0, 30)]
    [SerializeField] private int max_typeBonuses = 1;
    public int Max_typeBonuses => max_typeBonuses;

    [Space(30)]

    [Header("Stage 1")]
    [Header("Stage parameters")]

    [Header("Enemy parameters")]
    [Range(0, 6)]
    [SerializeField] private int min_Enemy_stage1 = 0;
    public int Min_Enemy_stage1 => min_Enemy_stage1;
    [Range(0, 6)]
    [SerializeField] private int max_Enemy_stage1 = 3;
    public int Max_Enemy_stage1 => max_Enemy_stage1;

    [Header("Traps parameters")]
    [Range(0, 8)]
    [SerializeField] private int min_Traps_stage1 = 0;
    public int Min_Traps_stage1 => min_Traps_stage1;
    [Range(0, 8)]
    [SerializeField] private int max_Traps_stage1 = 4;
    public int Max_Traps_stage1 => max_Traps_stage1;

    [Header("Bonuses parameters")]
    [Range(0, 2)]
    [SerializeField] private int min_Bonuses_stage1 = 0;
    public int Min_Bonuses_stage1 => min_Bonuses_stage1;
    [Range(0, 2)]
    [SerializeField] private int max_Bonuses_stage1 = 1;
    public int Max_Bonuses_stage1 => max_Bonuses_stage1;
    [Range(0, 1)]
    [SerializeField] private float chance_Bonuses_stage1 = 0.25f;
    public float Chance_Bonuses_stage1 => chance_Bonuses_stage1;

    [Header("Fish money parameters")]
    [Range(0, 12)]
    [SerializeField] private int min_fishMoney_stage1 = 0;
    public int Min_fishMoney_stage1 => min_fishMoney_stage1;
    [Range(0, 12)]
    [SerializeField] private int max_fishMoney_stage1 = 5;
    public int Max_fishMoney_stage1 => max_fishMoney_stage1;

    [Header("Other parameters")]
    [Range(0, 1)]
    [SerializeField] private float part_openItems_stage1 = 0.25f;
    public float Part_openItems_stage1 => part_openItems_stage1;
    [Range(0, 300)]
    [SerializeField] private int activationDistance_stage1 = 0;
    public int ActivationDistance_stage1 => activationDistance_stage1;

    [Space(10)]

    [Header("Stage 2")]
    [Header("Stage parameters")]

    [Header("Enemy parameters")]
    [Range(0, 6)]
    [SerializeField] private int min_Enemy_stage2 = 1;
    public int Min_Enemy_stage2 => min_Enemy_stage2;
    [Range(0, 6)]
    [SerializeField] private int max_Enemy_stage2 = 4;
    public int Max_Enemy_stage2 => max_Enemy_stage2;

    [Header("Traps parameters")]
    [Range(0, 8)]
    [SerializeField] private int min_Traps_stage2 = 2;
    public int Min_Traps_stage2 => min_Traps_stage2;
    [Range(0, 8)]
    [SerializeField] private int max_Traps_stage2 = 6;
    public int Max_Traps_stage2 => max_Traps_stage2;

    [Header("Bonuses parameters")]
    [Range(0, 2)]
    [SerializeField] private int min_Bonuses_stage2 = 0;
    public int Min_Bonuses_stage2 => min_Bonuses_stage2;
    [Range(0, 2)]
    [SerializeField] private int max_Bonuses_stage2 = 1;
    public int Max_Bonuses_stage2 => max_Bonuses_stage2;
    [Range(0, 1)]
    [SerializeField] private float chance_Bonuses_stage2 = 0.35f;
    public float Chance_Bonuses_stage2 => chance_Bonuses_stage2;

    [Header("Fish money parameters")]
    [Range(0, 12)]
    [SerializeField] private int min_fishMoney_stage2 = 1;
    public int Min_fishMoney_stage2 => min_fishMoney_stage2;
    [Range(0, 12)]
    [SerializeField] private int max_fishMoney_stage2 = 7;
    public int Max_fishMoney_stage2 => max_fishMoney_stage2;

    [Header("Other parameters")]
    [Range(0, 1)]
    [SerializeField] private float part_openItems_stage2 = 0.5f;
    public float Part_openItems_stage2 => part_openItems_stage2;
    [Range(0, 300)]
    [SerializeField] private int activationDistance_stage2 = 40;
    public int ActivationDistance_stage2 => activationDistance_stage2;

    [Space(10)]

    [Header("Stage 3")]
    [Header("Stage parameters")]

    [Header("Enemy parameters")]
    [Range(0, 6)]
    [SerializeField] private int min_Enemy_stage3 = 2;
    public int Min_Enemy_stage3 => min_Enemy_stage3;
    [Range(0, 6)]
    [SerializeField] private int max_Enemy_stage3 = 5;
    public int Max_Enemy_stage3 => max_Enemy_stage3;

    [Header("Traps parameters")]
    [Range(0, 8)]
    [SerializeField] private int min_Traps_stage3 = 4;
    public int Min_Traps_stage3 => min_Traps_stage3;
    [Range(0, 8)]
    [SerializeField] private int max_Traps_stage3 = 8;
    public int Max_Traps_stage3 => max_Traps_stage3;

    [Header("Bonuses parameters")]
    [Range(0, 2)]
    [SerializeField] private int min_Bonuses_stage3 = 0;
    public int Min_Bonuses_stage3 => min_Bonuses_stage3;
    [Range(0, 2)]
    [SerializeField] private int max_Bonuses_stage3 = 2;
    public int Max_Bonuses_stage3 => max_Bonuses_stage3;
    [Range(0, 1)]
    [SerializeField] private float chance_Bonuses_stage3 = 0.5f;
    public float Chance_Bonuses_stage3 => chance_Bonuses_stage3;

    [Header("Fish money parameters")]
    [Range(0, 12)]
    [SerializeField] private int min_fishMoney_stage3 = 3;
    public int Min_fishMoney_stage3 => min_fishMoney_stage3;
    [Range(0, 12)]
    [SerializeField] private int max_fishMoney_stage3 = 9;
    public int Max_fishMoney_stage3 => max_fishMoney_stage3;

    [Header("Other parameters")]
    [Range(0, 1)]
    [SerializeField] private float part_openItems_stage3 = 1f;
    public float Part_openItems_stage3 => part_openItems_stage3;
    [Range(0, 300)]
    [SerializeField] private int activationDistance_stage3 = 80;
    public int ActivationDistance_stage3 => activationDistance_stage3;


    public void changeTypeEnemyLimits(int min, int max)
    {
        min_typeEnemy = min;
        max_typeEnemy = max;
    }

    public void changeTypeTrapsLimits(int min, int max)
    {
        min_typeTraps = min;
        max_typeTraps = max;
    }

    public void changeTypeBonusesLimits(int min, int max)
    {
        min_typeBonuses = min;
        max_typeBonuses = max;
    }

    public void changeEnemiesParameters(int minEnemy1, int maxEnemy1, int minEnemy2, int maxEnemy2, int minEnemy3, int maxEnemy3)
    {
        min_Enemy_stage1 = minEnemy1;
        max_Enemy_stage1 = maxEnemy1;

        min_Enemy_stage2 = minEnemy2;
        max_Enemy_stage2 = maxEnemy2;

        min_Enemy_stage3 = minEnemy3;
        max_Enemy_stage3 = maxEnemy3;
    }

    public void changeTrapsParameters(int minTraps1, int maxTraps1, int minTraps2, int maxTraps2, int minTraps3, int maxTraps3)
    {
        min_Traps_stage1 = minTraps1;
        max_Traps_stage1 = maxTraps1;

        min_Traps_stage2 = minTraps2;
        max_Traps_stage2 = maxTraps2;

        min_Traps_stage3 = minTraps3;
        max_Traps_stage3 = maxTraps3;

    }

}


