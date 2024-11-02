using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinDatabase", menuName = "LevelProperties/New SkinDatabase")]
public class SkinDatabase : ScriptableObject
{
    [field: SerializeField] public List<CatSkin> CatSkins { get; private set; }
}
