using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChthAngelCannon_Properties", menuName = "LevelProperties/Enemy/New ChthAngelCannon_Properties")]
public class ChthAngelCanInfo : CannonInfo
{

    [SerializeField] private float spinningSpeed = 10;
    public float SpinningSpeed => spinningSpeed;

    public override Enemy createEnemy(GameObject cannonObject, Stamp stamp, GameObject[] objParameters, out Dictionary<string, float> numParameters)
    {
        numParameters = null;
        ChthAngelCan cannon = new ChthAngelCan(this, cannonObject, stamp);
        return cannon;
    }
}
