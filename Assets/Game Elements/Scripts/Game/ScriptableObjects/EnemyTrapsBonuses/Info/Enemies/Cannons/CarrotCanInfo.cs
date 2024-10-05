using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarrotCannon_Properties", menuName = "LevelProperties/Enemy/New CarrotCannon_Properties")]
public class CarrotCanInfo : CannonInfo
{
    [Header("Features")]
    [Range(0f, 1f)]
    [SerializeField] private protected float chance_bulletCollapse;
    public float chance_bullet_Collapse => chance_bulletCollapse;

    [SerializeField] GameObject particlesBulletDestruction;
    public GameObject ParticlesBulletDestruction => particlesBulletDestruction;

    public override Enemy createEnemy(GameObject cannonObject, Stamp stamp, GameObject[] objParameters, out Dictionary<string, float> numParameters)
    {
        numParameters = null;
        CarrotCan cannon = new CarrotCan(this, cannonObject, stamp);
        return cannon;
    }
}
