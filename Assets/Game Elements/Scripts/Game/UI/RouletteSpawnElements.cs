using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
struct rouletteElement
{
    public GameObject[] elements;
}
public class RouletteSpawnElements : MonoBehaviour
{
    [SerializeField] private DissolvingStartWindow dissolvingStartWindow;

    [SerializeField] private rouletteElement[] containers;
    [SerializeField] private rouletteElement[] roulettePlaces;

    [SerializeField] private rouletteElement[] enemyElements;
    [SerializeField] private rouletteElement[] trapsElements;
    [SerializeField] private rouletteElement[] bonusesElements;


    private List<float>[] chancesForRoulette;

    private List<EnemyInfo> allEnemy;
    private List<TrapInfo> allTraps;
    private List<BonusInfo> allBonuses;

    private List<EnemyInfo> currentEnemy;
    private List<TrapInfo> currentTraps;
    private List<BonusInfo> currentBonuses;

    private float startContainerRectPosY;
    private float endContainerRectPosY = 593;

    public void Start()
    {
        startContainerRectPosY = containers[0].elements[0].GetComponent<RectTransform>().anchoredPosition.y;
        Debug.Log(startContainerRectPosY);
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
    }

    public IEnumerator rouletteProcess()
    {
        int n = currentEnemy.Count;

        for(int i = 0; i < n; i++)
            StartCoroutine(MovementCoroutine(containers[0].elements[i], startContainerRectPosY, endContainerRectPosY, 2f));

        n = currentTraps.Count;
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < n; i++)
            StartCoroutine(MovementCoroutine(containers[1].elements[i], startContainerRectPosY, endContainerRectPosY, 2f));

        n = currentBonuses.Count;
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < n; i++)
            StartCoroutine(MovementCoroutine(containers[2].elements[i], startContainerRectPosY, endContainerRectPosY, 2f));

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
        int n2 = enemyElements[0].elements.Length;
        int num = 0;

        for(int i = 0; i < n1; i++)
        {
            roulettePlaces[0].elements[i].SetActive(true);
            for(int j = 0; j < n2-1; j++)
            {
                getRandomElementFromList(allEnemy, chancesForRoulette[0], out num);
                enemyElements[i].elements[j].GetComponent<Image>().sprite = allEnemy[num].Icon;
            }
            enemyElements[i].elements[n2-1].GetComponent<Image>().sprite = currentEnemy[i].Icon;
        }

        n1 = currentTraps.Count;
        n2 = trapsElements[0].elements.Length;

        for (int i = 0; i < n1; i++)
        {
            roulettePlaces[1].elements[i].SetActive(true);
            for (int j = 0; j < n2 - 1; j++)
            {
                getRandomElementFromList(allTraps, chancesForRoulette[1], out num);
                trapsElements[i].elements[j].GetComponent<Image>().sprite = allTraps[num].Icon;
            }
            trapsElements[i].elements[n2 - 1].GetComponent<Image>().sprite = currentTraps[i].Icon;
        }

        n1 = currentBonuses.Count;
        n2 = bonusesElements[0].elements.Length;

        for (int i = 0; i < n1; i++)
        {
            roulettePlaces[2].elements[i].SetActive(true);
            for (int j = 0; j < n2 - 1; j++)
            {
                getRandomElementFromList(allBonuses, chancesForRoulette[2], out num);
                bonusesElements[i].elements[j].GetComponent<Image>().sprite = allBonuses[num].Icon;
            }
            bonusesElements[i].elements[n2 - 1].GetComponent<Image>().sprite = currentBonuses[i].Icon;
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
