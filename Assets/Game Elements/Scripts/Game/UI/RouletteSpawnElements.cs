using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RouletteSpawnElements : MonoBehaviour
{
    [SerializeField] private DissolvingStartWindow dissolvingStartWindow;

    [SerializeField] private GameObject[] containers;
    [SerializeField] private GameObject[] roulettePlaces;

    [SerializeField] private GameObject[] enemyElements;
    [SerializeField] private GameObject[] trapsElements;
    [SerializeField] private GameObject[] bonusesElements;

    [SerializeField] private GameObject[] stampsElements;
    [SerializeField] private Material stampMaterial;


    private List<float>[] chancesForRoulette;

    private List<EnemyInfo> allEnemy;
    private List<TrapInfo> allTraps;
    private List<BonusInfo> allBonuses;

    private List<EnemyInfo> currentEnemy;
    private List<TrapInfo> currentTraps;
    private List<BonusInfo> currentBonuses;

    public void Start()
    {

    }
    public void startRoulette(List<float>[] chancesForRoulette, List<EnemyInfo> currentEnemy, List<TrapInfo> currentTraps, List<BonusInfo> currentBonuses, List<EnemyInfo> allEnemy, List<TrapInfo> allTraps, List<BonusInfo> allBonuses)
    {
        this.chancesForRoulette = chancesForRoulette;
        this.currentEnemy = currentEnemy;
        this.currentTraps = currentTraps;
        this.currentBonuses = currentBonuses;
        this.allEnemy = allEnemy;
        this.allTraps = allTraps;
        this.allBonuses = allBonuses;

        
        if (currentEnemy.Count <= 6)
        {
            initialize();
            StartCoroutine(rouletteProcess());
        }
        else
        {
            dissolvingStartWindow.dissolveAnimation();
        }
    }

    public IEnumerator rouletteProcess()
    {

        yield return new WaitForSeconds(2f);
        dissolvingStartWindow.dissolveAnimation();

        
    }

    private protected virtual IEnumerator MovementCoroutine(GameObject obj, float startContainerRectPosY, float endContainerRectPosY, float time)
    {
        RectTransform rectTrans = obj.GetComponent<RectTransform>();
        Vector2 start_pos = new Vector2(rectTrans.anchoredPosition.x, startContainerRectPosY);
        Vector2 end_pos = new Vector2(rectTrans.anchoredPosition.x, endContainerRectPosY);

        float runningTime = 0;
        float totalRunningTime = time;

        while (runningTime < totalRunningTime)
        {
            runningTime += Time.deltaTime;
            rectTrans.anchoredPosition = Vector2.Lerp(start_pos, end_pos, runningTime / totalRunningTime);
            yield return 1;
        }
    }

    public void initialize()
    {
        int n1 = currentEnemy.Count;

        for (int i = 0; i < n1; i++)
        {
            enemyElements[i].GetComponent<Image>().sprite = currentEnemy[i].Icon;
            enemyElements[i].SetActive(true);
        }

        n1 = currentTraps.Count;

        for (int i = 0; i < n1; i++)
        {
            trapsElements[i].GetComponent<Image>().sprite = currentTraps[i].Icon;
            trapsElements[i].SetActive(true);
        }

        n1 = currentBonuses.Count;

        for (int i = 0; i < n1; i++)
        {
            bonusesElements[i].GetComponent<Image>().sprite = currentBonuses[i].Icon;
            bonusesElements[i].SetActive(true);
        }
    }

    private T getRandomElementFromList<T>(List<T> list, List<float> chances, out int numInList) where T : SpawnElementInfo
    {
        float total_probability = 0;

        for (int i = 0; i < list.Count; i++)
        {
            total_probability += chances[i];
        }

        float randomPoint = UnityEngine.Random.value * total_probability;

        for (int i = 0; i < list.Count; i++)
        {
            if (randomPoint < chances[i])
            {
                numInList = i;
                return list[numInList];
            }
            else
            {
                randomPoint -= chances[i];
            }
        }
        numInList = list.Count - 1;
        return list[numInList];
    }
}
