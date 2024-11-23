using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "FlyStandartCanInfo_Properties", menuName = "LevelProperties/Enemy/New FlyStandartCanInfo_Properties")]
public class FlyStandartCanInfo : CannonInfo
{
    [SerializeField] private float spinningSpeed = 10;
    public float SpinningSpeed => spinningSpeed;

    [SerializeField] private float smooth = 0.05f;
    public float Smooth => smooth;
    public override Enemy createEnemy(GameObject cannonObject, Stamp stamp, GameObject[] objParameters, out Dictionary<string, float> numParameters)
    {
        numParameters = null;
        FlyStandartCan cannon = new FlyStandartCan(this, cannonObject, stamp);
        return cannon;
    }
}
