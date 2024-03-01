using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class LevelSkinSet
{
    [Header("Skin Properties")]
    [SerializeField] private List<SkinProperties> islands;
    [SerializeField] private List<SkinProperties> roadParts;
    [SerializeField] private List<SkinProperties> enemy;
    [SerializeField] private List<SkinProperties> traps;
    [SerializeField] private List<SkinProperties> bonuses;

    private List<Enemy> enemy_properties;
    private List<Trap> traps_properties;
    private List<Bonus> bonuses_properties;
    private List<ObjectProperties> islands_properties;
    private List<ObjectProperties> roadParts_properties;

    public void startInitSkinSet(LevelPropertiesDatabase database)
    {
        islands = new List<SkinProperties>();
        roadParts = new List<SkinProperties>();
        enemy = new List<SkinProperties>();
        traps = new List<SkinProperties>();
        bonuses = new List<SkinProperties>();

        int i = 0;

        enemy_properties = database.Enemy_properties;
        traps_properties = database.Traps_properties;
        bonuses_properties = database.Bonuses_properties;
        islands_properties = database.Islands_properties;
        roadParts_properties = database.RoadParts_properties;

        int n_i = islands_properties.Count;
        int n_r = roadParts_properties.Count;
        int n_e = enemy_properties.Count;
        int n_t = traps_properties.Count;
        int n_b = bonuses_properties.Count;

        SkinProperties temp;

        for(i = 0; i < n_i; i++)
            islands.Add(new SkinProperties(islands_properties[i].Id, islands_properties[i].Prefab));
        for (i = 0; i < n_r; i++)
            roadParts.Add(new SkinProperties(roadParts_properties[i].Id, roadParts_properties[i].Prefab));
        for (i = 0; i < n_e; i++)
            enemy.Add(new SkinProperties(enemy_properties[i].Id, enemy_properties[i].Prefab));
        for (i = 0; i < n_t; i++)
            traps.Add(new SkinProperties(traps_properties[i].Id, traps_properties[i].Prefab));
        for (i = 0; i < n_b; i++)
            bonuses.Add(new SkinProperties(bonuses_properties[i].Id, bonuses_properties[i].Prefab));
    }
}

[CreateAssetMenu(fileName = "LevelSkinSetDatabase", menuName = "LevelProperties/Databases/New LevelSkinSetDatabase")]
public class LevelSkinSetDatabase : ScriptableObject
{
    [Serializable] 
    public class SkinSetData
    {
        [Header("Level Skin sets")]
        [SerializeField]private List<LevelSkinSet> skinSets;
        public List<LevelSkinSet> SkinSets => skinSets;

        [Header("Level database")]
        public LevelPropertiesDatabase database;
    }

    [SerializeField] private SkinSetData skinSetData;

    public SkinSetData _skinSetData => skinSetData;

    public void startInitAllSkinSets()
    {
        for(int i = 0; i < skinSetData.SkinSets.Count;i++) skinSetData.SkinSets[i].startInitSkinSet(skinSetData.database);
    }

}
