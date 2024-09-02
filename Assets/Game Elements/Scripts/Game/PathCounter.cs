using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PathCounter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] LevelPropertiesDatabase database;
    [SerializeField] GameManager gameManager;

    [SerializeField] private TMP_Text pathCounterText;
    [SerializeField] private int pathScore;

    private int n_stages = 0;
    private List<int> activationDistances = new List<int>() { -1 };
    public int PathScore => pathScore;

    public Coroutine PathCounterCoroutine;

    [SerializeField] private float speedCount;
    // Start is called before the first frame update
    void Start()
    {
        n_stages = database.stageParameters.Count;
        for(int i = 1; i < n_stages; i++)
            activationDistances.Add(database.stageParameters[i].ActivationDistance_stage);

        speedCount = gameManager.SpeedCount;

        GlobalEventManager.OnGameOver += GameOver;
        GlobalEventManager.OnUnSubscribe += unSubscribe;
    }

    void unSubscribe()
    {
        GlobalEventManager.OnGameOver -= GameOver;
        GlobalEventManager.OnUnSubscribe -= unSubscribe;
    }


    public void stopPathCounter()
    {
        if (PathCounterCoroutine != null)
            StopCoroutine(PathCounterCoroutine);
    }

    public void startPathCounter()
    {
        if (PathCounterCoroutine != null)
            StopCoroutine(PathCounterCoroutine);
        PathCounterCoroutine = StartCoroutine(pathCounterCoroutine());
    }

    IEnumerator pathCounterCoroutine()
    {
        while (true)
        {
            int n_stage = activationDistances.FindIndex(delegate (int activDist)
            {
                return activDist == pathScore;
            }
            );

            if (n_stage != -1)
            {
                GlobalEventManager.ChangeStageGame(n_stage);
            }

            pathScore++;
            pathCounterText.text = pathScore.ToString();
            yield return new WaitForSeconds(1 / speedCount);
        }
    }

    void GameOver()
    {
        StopCoroutine(PathCounterCoroutine);
    }

    public void changeSpeedCount(float speedCount)
    {
        this.speedCount = speedCount;

        if (speedCount == 0)
        {
            StopCoroutine(PathCounterCoroutine);
        }
        else if (this.speedCount == 0)
        {
            PathCounterCoroutine = StartCoroutine(pathCounterCoroutine());
        }
    }
}
