using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StandartCannon_Properties", menuName = "LevelProperties/Enemy/New StandartCannon_Properties")]
public class StandartCannon : Cannon
{
    public override void Attack()
    {
        test();
    }
    public void test()
    {
        Debug.Log("lox");
    }
}
