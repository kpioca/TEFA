using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class CardObject
{
    public GameObject baseCard;
    public GameObject mainElement;
    public GameObject stamp;
}
public class RouletteSpawnElements : MonoBehaviour
{

    [SerializeField] private GameObject[] enemyElements;
    [SerializeField] private GameObject[] trapsElements;
    [SerializeField] private GameObject[] bonusesElements;

    [SerializeField] private GridLayoutGroup enemiesLayoutGroup;
    [SerializeField] private GridLayoutGroup trapsLayoutGroup;
    [SerializeField] private GridLayoutGroup bonusesLayoutGroup;

    [SerializeField] private CardDatabase cardDatabase;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator animator;

    [SerializeField] TMP_Text multiplierText;

    private Vector2[] cardSizes = new Vector2[] { 
        new Vector2(169.9724f, 256.0403f), new Vector2(196.09f, 295.3787f), new Vector2(223.539f, 336.7311f), 
        new Vector2(296.7053f, 446.9461f), new Vector2(314.3586f, 473.5384f), new Vector2(473.14f, 712.725f) };

    private List<EnemyInfo> currentEnemy;
    private List<TrapInfo> currentTraps;
    private List<BonusInfo> currentBonuses;
    private List<Stamp> currentStamps;
    private int multiplier = 1;

    private Dictionary<TypeSpawnElement, Sprite> cardBases;

    public void startRoulette(List<EnemyInfo> currentEnemy, List<TrapInfo> currentTraps, List<BonusInfo> currentBonuses, List<Stamp> stamps, int multiplier)
    {
        this.currentEnemy = currentEnemy;
        this.currentTraps = currentTraps;
        this.currentBonuses = currentBonuses;
        currentStamps = stamps;
        this.multiplier = multiplier;


        if (currentEnemy.Count <= enemyElements.Length && currentBonuses.Count <= bonusesElements.Length && currentTraps.Count <= trapsElements.Length)
        {
            initialize();
        }
    }

    public IEnumerator startGameProcess()
    {
        animator.enabled = true;
        yield return new WaitForSeconds(0.5f);
        gameManager.allEnable();
        yield return new WaitForSeconds(0.5f);
        animator.enabled = false;
        animator.Rebind();
        gameObject.SetActive(false);
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

    public void startGame()
    {
        StartCoroutine(startGameProcess());
    }

    public void setCardSizes(int n_elements, GridLayoutGroup layoutGroup)
    {
        if (n_elements >= 13)
        {
            layoutGroup.cellSize = cardSizes[0];
        }
        else if (n_elements >= 9)
        {
            layoutGroup.cellSize = cardSizes[1];
            layoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            layoutGroup.constraintCount = 4;
        }
        else if (n_elements >= 7)
        {
            layoutGroup.cellSize = cardSizes[2];
            layoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            layoutGroup.constraintCount = 4;
        }
        else if (n_elements >= 5)
        {
            layoutGroup.cellSize = cardSizes[3];
            layoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            layoutGroup.constraintCount = 3;
        }
        else if (n_elements >= 2)
        {
            layoutGroup.cellSize = cardSizes[4];
            layoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            layoutGroup.constraintCount = 2;
        }
        else layoutGroup.cellSize = cardSizes[5];
    }
    public void initialize()
    {
        int n1 = currentEnemy.Count;
        CardObject card;
        cardBases = cardDatabase.cardBasesDict;

        multiplierText.text = multiplier + "X";

        for (int i = 0; i < n1; i++)
        {
            card = enemyElements[i].GetComponent<ContentCard>().Content_Card;
            card.baseCard.GetComponent<Image>().sprite = cardBases[currentEnemy[i].Type];
            card.mainElement.GetComponent<Image>().sprite = currentEnemy[i].Icon;

            setCardSizes(n1, enemiesLayoutGroup);
            enemyElements[i].SetActive(true);
        }

        n1 = currentTraps.Count;

        for (int i = 0; i < n1; i++)
        {
            card = trapsElements[i].GetComponent<ContentCard>().Content_Card;
            card.baseCard.GetComponent<Image>().sprite = cardBases[currentTraps[i].Type];
            card.mainElement.GetComponent<Image>().sprite = currentTraps[i].Icon;

            setCardSizes(n1, trapsLayoutGroup);
            trapsElements[i].SetActive(true);
        }

        n1 = currentBonuses.Count;

        for (int i = 0; i < n1; i++)
        {
            card = bonusesElements[i].GetComponent<ContentCard>().Content_Card;
            card.baseCard.GetComponent<Image>().sprite = cardBases[currentBonuses[i].Type];
            card.mainElement.GetComponent<Image>().sprite = currentBonuses[i].Icon;

            setCardSizes(n1, bonusesLayoutGroup);
            bonusesElements[i].SetActive(true);
        }

        n1 = currentStamps.Count;
        CannonInfo cannonInfo;
        int k = 0;

        for (int i = 0; i < n1; i++)
        {
            cannonInfo = currentStamps[i].cannonInfo;
            k = currentEnemy.FindIndex(enemy => enemy.Id == cannonInfo.Id);
            card = enemyElements[k].GetComponent<ContentCard>().Content_Card;

            if (currentStamps[i].Stamp_material != null)
                card.stamp.GetComponent<Image>().material = currentStamps[i].Stamp_material;
            card.stamp.GetComponent<Image>().sprite = currentStamps[i].Icon;
            card.stamp.SetActive(true);
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
