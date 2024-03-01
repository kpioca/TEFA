using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StandartDoubleCannon_Properties", menuName = "LevelProperties/Enemy/New StandartDoubleCannon_Properties")]
public class StandartDoubleCannon : Cannon
{
    public override void Attack()
    {
        test();
    }
    public void test()
    {
        Debug.Log("pizda");
    }
}
