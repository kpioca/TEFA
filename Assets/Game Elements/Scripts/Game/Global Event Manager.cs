using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobalEventManager
{
    public static Action OnPathWaySpawn;
    public static Action OnChangeStageGame;
    public static Action a;

    public static void Test()
    {
        if (a != null) a.Invoke();
    }
    public static void SpawnPathWay()
    {
        if (OnPathWaySpawn != null) OnPathWaySpawn.Invoke();
    }

    public static void ChangeStageGame()
    {
        if (OnChangeStageGame != null) OnChangeStageGame.Invoke();
    }
}
