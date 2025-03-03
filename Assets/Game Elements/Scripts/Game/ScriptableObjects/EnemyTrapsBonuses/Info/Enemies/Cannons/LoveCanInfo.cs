using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LoveCannon_Properties", menuName = "LevelProperties/Enemy/New LoveCannon_Properties")]
public class LoveCanInfo : CannonInfo
{
    [Header("Features")]
    [Range(1, 5)]
    [SerializeField] private protected int increaseSize_Projectile;
    public int increaseSizeProjectile => increaseSize_Projectile;

    public override Enemy createEnemy(GameObject cannonObject, Stamp stamp, GameObject[] objParameters, out Dictionary<string, float> numParameters)
    {
        numParameters = null;
        LoveCan cannon = new LoveCan(this, cannonObject, stamp);
        return cannon;
    }
}
