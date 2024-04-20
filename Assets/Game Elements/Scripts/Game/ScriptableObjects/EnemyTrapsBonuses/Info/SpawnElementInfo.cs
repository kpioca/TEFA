using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnElementInfo : ScriptableObject
{
    [SerializeField] private protected string id;
    public string Id => id;
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

}
