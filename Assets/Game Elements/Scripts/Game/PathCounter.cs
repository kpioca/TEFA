using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PathCounter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] LevelPropertiesDatabase database;
    [SerializeField] GameManager gameManager;

    [SerializeField] private TMP_Text pathCounterText;
    [SerializeField] private int pathScore;

    private int activDist1;
    private int activDist2;
    private int activDist3;
    public int PathScore => pathScore;

    [SerializeField] private float speedCount;
    // Start is called before the first frame update
    void Start()
    {
        activDist2 = database.ActivationDistance_stage2;
        activDist3 = database.ActivationDistance_stage3;
        speedCount = gameManager.SpeedCount;
        StartCoroutine(pathCounterCoroutine());

    }

    IEnumerator pathCounterCoroutine()
    {
        while (true)
        {
            if (pathScore == activDist2)
                GlobalEventManager.ChangeStageGame(2);
            else if (pathScore == activDist3)
                GlobalEventManager.ChangeStageGame(3);

            pathScore ++;
            pathCounterText.text = pathScore.ToString();
            yield return new WaitForSeconds(1 / speedCount);
        }
    }

}
