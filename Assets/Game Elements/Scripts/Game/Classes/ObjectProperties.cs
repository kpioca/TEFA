using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectProperties
{
    [SerializeField] private protected string id;
    public string Id => id;

    [SerializeField] private protected int sizeOfObjectPool;
    public int SizeOfObjectPool => sizeOfObjectPool;

    [SerializeField] private protected GameObject prefab;
    public GameObject Prefab => prefab;

    public ObjectProperties() { }
    public ObjectProperties(string id, int size_objectPool, GameObject prefab)
    {
        this.id = id;
        this.prefab = prefab;
        this.sizeOfObjectPool = size_objectPool;
    }

    public ObjectProperties(string id, int size_objectPool)
    {
        this.id = id;
        this.sizeOfObjectPool = size_objectPool;
    }
}
