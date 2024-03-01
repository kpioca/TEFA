using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PathCounter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] LevelPropertiesDatabase database;
    
    [SerializeField] private TMP_Text pathCounterText;
    [SerializeField] private int pathScore;
    public int PathScore => pathScore;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(pathCounterCoroutine());
    }

    IEnumerator pathCounterCoroutine()
    {
        while (true)
        {
            if (pathScore == database.ActivationDistance_stage2 || pathScore == database.ActivationDistance_stage3)
                GlobalEventManager.ChangeStageGame();
            pathScore++;
            pathCounterText.text = pathScore.ToString();
            yield return new WaitForSeconds(1);
        }
    }

}
