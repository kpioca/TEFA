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

    bool isDead = false;

    private int n_stages = 0;
    private LinkedList<int[]> activationDistances = new LinkedList<int[]>();
    public int PathScore => pathScore;

    private Coroutine PathCounterCoroutine;


    public LinkedListNode<int[]> currentStageDistance;
    [SerializeField] private float speedCount;

    [Header("Level View")]
    [SerializeField] private GameObject levelView;

    [Header("Level icons")]
    [SerializeField] private Sprite[] levelIcons;

    // Start is called before the first frame update
    void Start()
    {
        n_stages = database.stageParameters.Count;
        for (int i = 0; i < n_stages; i++)
        {
            activationDistances.AddLast(new int[2] { database.stageParameters[i].ActivationDistance_stage, i });
        }

        speedCount = gameManager.SpeedCount;
        currentStageDistance = activationDistances.First;

        GlobalEventManager.OnGameOver += GameOver;
    }

    void unSubscribe()
    {
        GlobalEventManager.OnGameOver -= GameOver;
    }


    public void stopPathCounter()
    {
        if (PathCounterCoroutine != null)
            StopCoroutine(PathCounterCoroutine);
    }

    public void startPathCounter()
    {
        if (!isDead)
        {
            if (PathCounterCoroutine != null)
                StopCoroutine(PathCounterCoroutine);
            PathCounterCoroutine = StartCoroutine(pathCounterCoroutine());
        }
    }

    
    IEnumerator pathCounterCoroutine()
    {
        int n_stage = 0;
        
        while (true)
        {
            if (currentStageDistance.Next != null)
            {
                if (currentStageDistance.Next.Value[0] == pathScore)
                {
                    n_stage = currentStageDistance.Next.Value[1];
                    if(levelIcons.Length > n_stage)
                        levelView.GetComponent<Image>().sprite = levelIcons[n_stage];
                    currentStageDistance = currentStageDistance.Next;
                }
                else n_stage = -1;
            }
            else n_stage = -1;

            if (n_stage != -1 && n_stage != 0)
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
        stopPathCounter();
        unSubscribe();
        isDead = true;
        this.enabled = false;
    }

    public void changeSpeedCount(float speedCount)
    {

        if (speedCount == 0)
        {
            stopPathCounter();
        }
        else if (this.speedCount == 0)
        {
            this.speedCount = speedCount;
            startPathCounter();
        }
        this.speedCount = speedCount;
    }
}
