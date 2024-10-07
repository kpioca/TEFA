using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultMenu : MonoBehaviour
{
    [Header("Path")]
    [SerializeField] TMP_Text pathCounter;
    [SerializeField] TMP_Text recordPathCounter;

    [Header("Fish and Food")]
    [SerializeField] TMP_Text fishCounter;
    [SerializeField] TMP_Text multiplier;
    [SerializeField] TMP_Text foodCounter;
    [SerializeField] TMP_Text totalFishCounter;

    private int allFish;
    private int allFood;

    public int pathValue;
    public int levelValue;
    public int recordPathValue;
    public int recordLevel;
    public void setPathCounter()
    {
        pathCounter.text = pathValue.ToString() + "<sprite=1>" + $"<sprite={levelValue+4}>";
    }

    public void setRecordPath()
    {
        recordPathCounter.text = recordPathValue.ToString() + "<sprite=0>" + $"<sprite={recordLevel+4}>";
    }

    public void setFishCounter(int value)
    {
        fishCounter.text = value.ToString() + "<sprite=2>";
    }

    public void setMultiplierCounter(int value)
    {
        multiplier.text = value.ToString() + "X";
    }

    public void setFoodCounter(int value)
    {
        foodCounter.text = value.ToString() + "<sprite=3>";
    }

    public void setTotalFishCounter(int value)
    {
        totalFishCounter.text = value.ToString() + "<sprite=2>";
    }

    public void initRecordPath(GameDataManager gameDataManager)
    {
        SaveData saveData = new SaveData(0, 0, 0, 0);
        gameDataManager.LoadDataGame(saveData);
        recordPathValue = saveData.recordPath;
        recordLevel = saveData.recordLevel;
        allFish = saveData.fish;
        allFood = saveData.food;

        setRecordPath();
    }

    public void setResult(int path, int level, int fish, int food, int multiplier, GameDataManager gameDataManager)
    {
        pathValue = path;
        levelValue = level;
        int totalFish;
        setPathCounter();
        if (recordPathValue < pathValue)
        {
            recordPathValue = pathValue;
            recordLevel = levelValue;
        }
        else if(recordLevel < levelValue)
        {
            recordPathValue = pathValue;
            recordLevel = levelValue;
        }
        setRecordPath();

        setMultiplierCounter(multiplier);
        setFishCounter(fish);
        setFoodCounter(food);
        totalFish = fish * multiplier;
        setTotalFishCounter(totalFish);

        allFish += totalFish;
        allFood += food;

        gameDataManager.SaveDataGame(new SaveData(recordPathValue, recordLevel, allFish, allFood));
    }


}
