using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BigCannon_Properties", menuName = "LevelProperties/Enemy/New BigCannon_Properties")]
public class BigCannon : Cannon
{
    public override void Attack()
    {
        test();
    }
    public void test()
    {
        Debug.Log("moloko");
    }
}
