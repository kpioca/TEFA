using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobalEventManager: MonoBehaviour 
{
    public static Action OnPathWaySpawn;
    public static Action<float> OnChangeSpeedRouteMovement;
    public static Action<int> OnChangeStageGame;
    public static Action OnGameOver;
    public static Action OnUnSubscribe;


    public void Start()
    {
    }


    public static void UnSubscribe()
    {
        if (OnUnSubscribe != null) OnUnSubscribe.Invoke();
    }
    public static void GameOver()
    {
        if (OnGameOver != null) OnGameOver.Invoke();
    }
    public static void SpawnPathWay()
    {
        if (OnPathWaySpawn != null) OnPathWaySpawn.Invoke();
    }

    public static void ChangeStageGame(int n_stage)
    {
        if (OnChangeStageGame != null) OnChangeStageGame.Invoke(n_stage);
    }

    public static void ChangeSpeedRouteMovement(float speed)
    {
        if(OnChangeSpeedRouteMovement != null) OnChangeSpeedRouteMovement.Invoke(speed);
    }
}
