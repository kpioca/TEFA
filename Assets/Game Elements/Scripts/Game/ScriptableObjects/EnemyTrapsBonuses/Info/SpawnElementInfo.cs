using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeSpawnElement
{
    Forest,
    Water,
    Sky
}

public class SpawnElementTypeComparer : IComparer<SpawnElementInfo>
{
    public int Compare(SpawnElementInfo x, SpawnElementInfo y)
    {
        return x.Type.CompareTo(y.Type);
    }
}

public class SpawnElementInfo : ScriptableObject, IComparable<SpawnElementInfo>
{
    [SerializeField] private protected string id;
    public string Id => id;
    [Header("Type")]
    [SerializeField] private protected TypeSpawnElement type;
    public TypeSpawnElement Type => type;

    [Header("The rarity level ")]
    [SerializeField] private protected int levelOfCoolness = 1;
    public int LevelOfCoolness => levelOfCoolness;

    [Header("Reference")]
    [SerializeField] private protected GameObject prefab;
    public GameObject Prefab => prefab;

    [Header("ObjectPooling")]
    [SerializeField] private protected int sizeOfObjectPool = 20;
    public int SizeOfObjectPool => sizeOfObjectPool;

    private protected float chanceIfSpawnThisType = 0;
    public float ChanceIfSpawnThisType
    {
        get { return chanceIfSpawnThisType; }
        set { chanceIfSpawnThisType = value; }
    }

    public int CompareTo(SpawnElementInfo obj)
    {
        return levelOfCoolness.CompareTo(obj.levelOfCoolness);
    }


}
