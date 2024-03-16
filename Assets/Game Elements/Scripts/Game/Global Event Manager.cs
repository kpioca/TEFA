using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobalEventManager
{
    public static Action OnPathWaySpawn;
    public static Action<int> OnChangeStageGame;
    public static Action<int> a;

    public static void Test(int b)
    {
        if (a != null) a.Invoke(b);
    }
    public static void SpawnPathWay()
    {
        if (OnPathWaySpawn != null) OnPathWaySpawn.Invoke();
    }

    public static void ChangeStageGame(int n_stage)
    {
        if (OnChangeStageGame != null) OnChangeStageGame.Invoke(n_stage);
    }
}
