using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkinProperties : ObjectProperties
{
    public SkinProperties(string id, GameObject prefab)
    {
        this.id = id;
        this.prefab = prefab;
    }

    public SkinProperties(string id)
    {
        this.id = id;
    }
}
