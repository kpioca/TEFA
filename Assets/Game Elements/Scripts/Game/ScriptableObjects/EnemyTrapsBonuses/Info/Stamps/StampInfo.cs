using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampInfo : ScriptableObject
{
    [SerializeField] private protected Material enemy_Material;
    public Material enemyMaterial => enemy_Material;

    [SerializeField] private protected Material bullet_Material;
    public Material bulletMaterial => bullet_Material;

}
