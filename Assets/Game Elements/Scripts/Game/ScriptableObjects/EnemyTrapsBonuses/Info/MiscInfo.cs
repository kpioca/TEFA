using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Misc_Properties", menuName = "LevelProperties/Misc/New Misc_Properties")]
public class MiscInfo: ScriptableObject
{
    [SerializeField] private protected string id;
    public string Id => id;

    [SerializeField] private protected int sizeOfObjectPool;
    public int SizeOfObjectPool => sizeOfObjectPool;

    [SerializeField] private protected GameObject prefab;
    public GameObject Prefab => prefab;

    [Range(0f, 1f)]
    [SerializeField] private protected float chance;
    public float Chance => chance;

}
