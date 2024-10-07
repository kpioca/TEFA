using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] TMP_Text fishCounter;
    [SerializeField] TMP_Text foodCounter;
    [SerializeField] GameDataManager gameDataManager;

    [SerializeField] int fish;
    [SerializeField] int food;

    [SerializeField] YandexMobileAdsRewardedAdScript adScript;

    SaveData data = new SaveData(0, 0, 0, 0);
    void Start()
    {
        gameDataManager.LoadDataGame(data);
        fish = data.fish;
        food = data.food;
        initCounters();
    }

    public void initCounters()
    {
        
        int fishView = fish >= 100000 ? (fish / 1000) : fish;
        int foodView = food >= 100000 ? (food / 1000) : food;
        string addFishK = fish >= 100000 ? "K" : "";
        string addFoodK = food >= 100000 ? "K" : "";

        fishCounter.text = fishView.ToString() + addFishK;
        foodCounter.text = foodView.ToString() + addFoodK;
    }

    public void changeFish(int difference)
    {
        fish = fish += difference;
        data.fish = fish;
    }

    public void changeFood(int difference)
    {
        food = food += difference;
        data.food = food;
    }
    public void StartGame()
    {
        gameDataManager.SaveDataGame(data);
        SceneManager.LoadSceneAsync(0);
    }

    public void getRewardForAd(int amount)
    {
       changeFood(amount);
    }

    /*
     * if (GUILayout.Button("Request Rewarded Ad", buttonStyle, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height / 8)))
            {
                this.RequestRewardedAd();
            }

            if (this.rewardedAd != null) {
                if (GUILayout.Button("Show Rewarded Ad", buttonStyle, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height / 8)))
                {
                    this.ShowRewardedAd();
                }
            }
            if (this.rewardedAd != null)
            {
                if (GUILayout.Button("Destroy Rewarded Ad", buttonStyle, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height / 8)))
                {
                    this.rewardedAd.Destroy();
                }
            }
     */
}
