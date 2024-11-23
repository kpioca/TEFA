using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    [Header("Leaderboard")]
    [SerializeField] LeaderboardSubmitSystem leaderboardSystem;

    [Header("Buttons")]
    [SerializeField] Button[] buttons;

    private int allFish;
    private int allFood;

    public int pathValue;
    public int levelValue;
    public int recordPathValue;
    public int recordLevel;

    bool _isRankingActive;

    IPersistentData _persistentData;
    IDataProvider _dataProvider;

    public void setPathCounter()
    {
        pathCounter.text = pathValue.ToString() + "<sprite=1>" + $"<sprite={levelValue+4}>";
    }

    public void setRecordPath()
    {
        if (!_isRankingActive)
            recordPathCounter.text = recordPathValue.ToString() + "<sprite=0>" + $"<sprite={recordLevel + 4}>";
        else recordPathCounter.text = "--||--";
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

    public void Initialize(IDataProvider dataProvider, IPersistentData persistentData, bool isRankingActive)
    {
        _persistentData = persistentData;
        _dataProvider = dataProvider;
        _isRankingActive = isRankingActive;

        recordPathValue = _persistentData.saveData.RecordPath;
        recordLevel = _persistentData.saveData.RecordLevel;
        allFish = _persistentData.saveData.Fish;
        allFood = _persistentData.saveData.Food;

        setRecordPath();
    }

    public void setResult(int path, int level, int fish, int food, int multiplier)
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

        _persistentData.saveData.RecordPath = recordPathValue;
        _persistentData.saveData.RecordLevel = recordLevel;
        _persistentData.saveData.Fish = allFish;
        _persistentData.saveData.Food = allFood;

        _dataProvider.Save();

        if(_isRankingActive)
        leaderboardSystem.InitializeSubmitRecord(pathValue, (bool callback) =>
        {
            foreach (Button button in buttons)
                button.interactable = true;
        });
    }


}
