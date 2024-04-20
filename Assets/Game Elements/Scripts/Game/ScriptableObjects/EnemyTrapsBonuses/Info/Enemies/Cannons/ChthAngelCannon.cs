using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChthAngelCannon_Properties", menuName = "LevelProperties/Enemy/New ChthAngelCannon_Properties")]
public class ChthAngelCanInfo : CannonInfo
{
    public void xuy()
    {

    }

    [SerializeField] private float spinningSpeed = 10;
    public float SpinningSpeed => spinningSpeed;
}
