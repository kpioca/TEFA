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

    public int pathValue;
    public int recordPathValue;
    public void setPathCounter()
    {
        pathCounter.text = pathValue.ToString() + "<sprite=1>";
    }

    public void setRecordPath()
    {
        recordPathCounter.text = recordPathValue.ToString() + "<sprite=0>";
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

    public void setTotalFishCounterCounter(int value)
    {
        totalFishCounter.text = value.ToString() + "<sprite=2>";
    }

    public void initRecordPath(GameDataManager gameDataManager)
    {
        SaveData saveData = new SaveData(0);
        gameDataManager.LoadRecordPath(saveData);
        recordPathValue = saveData.recordPath;
        setRecordPath();
    }

    public void setResult(int path, int fish, int food, GameDataManager gameDataManager)
    {
        pathValue = path;
        setPathCounter();
        if (recordPathValue < pathValue)
        {
            recordPathValue = pathValue;
            gameDataManager.SaveRecordPath(new SaveData(recordPathValue));
        }
        setRecordPath();

        setMultiplierCounter(1);
        setFishCounter(fish);
        setFoodCounter(food);
        setTotalFishCounterCounter(fish);
    }


}
