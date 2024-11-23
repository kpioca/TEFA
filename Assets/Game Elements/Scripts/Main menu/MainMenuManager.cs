using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] TMP_Text fishCounter;
    [SerializeField] TMP_Text foodCounter;
    [SerializeField] TMP_Text healthCounter;
    [SerializeField] TMP_Text armorCounter;
    [SerializeField] TMP_Text upgradeCounter;
    [SerializeField] TMP_Text priceUpgradeText;
    [SerializeField] Button upgradeButton;

    [SerializeField] GameObject _settingsPanel;

    [SerializeField] GameObject _skinCategoryContent;
    [SerializeField] GameObject _foodCategoryContent;

    [Header("LoginAndSignUpManager")]
    [SerializeField] LoginAndSignUpManager _loginAndSignUpManager;



    [SerializeField] int fish;
    public int Fish => fish;
    [SerializeField] int food;

    [SerializeField] int health = 0;
    [SerializeField] int armor = 0;
    [SerializeField] int upgradePoints = 0;

    int maxUpgradeNum = 9;
    int priceUpgrade;

    //[SerializeField] YandexMobileAdsRewardedAdScript adScript;

    IPersistentData _persistentData;
    IDataProvider _dataProvider;
    public void Initialize(IPersistentData persistentData, IDataProvider dataProvider)
    {
        _persistentData = persistentData;
        _dataProvider = dataProvider;
        fish = persistentData.saveData.Fish;
        food = persistentData.saveData.Food;

        health = persistentData.saveData.Health;
        armor = persistentData.saveData.Armor;
        upgradePoints = persistentData.saveData.UpgradePoints;
        if(health == 0)
        {
            changeHealth(5);
        }

        /*
        data.health = 5;
        data.armor = 0;
        data.upgradePoints = 0;
        gameDataManager.SaveDataGame(data);
        */

        _skinCategoryContent.SetActive(true);
        _foodCategoryContent.SetActive(false);
        initCounters();
        checkValues();
    }


    public void EnableSettingsPanel()
    {
        _settingsPanel.SetActive(true);
        if(!_loginAndSignUpManager.IsInitializedGameSession)
            _loginAndSignUpManager.InitializeGameSession();
    }

    public void DisableSettingsPanel()
    {
        _settingsPanel.SetActive(false);
    }
    public void checkValues()
    {
        int value = 0;
        int temp_food = 0;
        if(fish < 0)
        {
            value = Mathf.FloorToInt(-fish / 500);
            temp_food = food - value;
            temp_food = temp_food > 0 ? temp_food : 0;
            setFishAndFood(0, temp_food);
        }
    }

    public void OnToggleSkinCategoryChanged(bool state)
    {
        if (state)
            _skinCategoryContent.SetActive(true);
        else _skinCategoryContent.SetActive(false);
    }

    public void OnToggleFoodCategoryChanged(bool state)
    {
        if (state)
            _foodCategoryContent.SetActive(true);
        else _foodCategoryContent.SetActive(false);
    }

    public void countPriceUpgrade()
    {
        int sum = health + armor + upgradePoints;
        int price = 0;
        if (sum < 9)
        {
            price = (int)(1000 * Mathf.Pow(2.5f, sum - 5));
            priceUpgrade = price;
            priceUpgradeText.text = priceUpgrade.ToString() + "<sprite=2>";
        }
        else
        {
            priceUpgradeText.text = "Продано";
            upgradeButton.interactable = false;
        }
    }

    public bool buySkin(ShopItemView shopItemView, SkinUnlocker skinUnlocker)
    {
        if (fish >= shopItemView.Item.Price)
        {
            changeFish(-shopItemView.Price);
            shopItemView.Item.Accept(skinUnlocker);
            shopItemView.Bought();
            _dataProvider.Save();
            return true;
        }
        return false;
    }

    public void unlockGachaSkin(GachaItemView gachaItemView, SkinUnlocker skinUnlocker)
    {
        gachaItemView.Item.Accept(skinUnlocker);
        _dataProvider.Save();
    }

    public void buyUpgradePoint(int value)
    {
        int sum = health + armor + upgradePoints;
        if (sum < maxUpgradeNum && fish >= priceUpgrade)
        {
            changeUpgradePoints(value);
            changeFish(-priceUpgrade);
            countPriceUpgrade();
        }
    }
    public void addHealth(int value)
    {
        if (upgradePoints >= value)
        {
            if ((health > 1 && value < 0) || value > 0)
            {
                changeUpgradePoints(-value);
                changeHealth(value);
            }
        }
    }

    public void addArmor(int value)
    {
        if (upgradePoints >= value)
        {
            if ((armor > 0 && value < 0) || value > 0)
            {
                changeUpgradePoints(-value);
                changeArmor(value);
            }
        }
    }


    public void changeUpgradePoints(int differenceUpgradePoints)
    {
        upgradePoints += differenceUpgradePoints;
        _persistentData.saveData.UpgradePoints = upgradePoints;
        changeUpgradePointsCounter(upgradePoints);

    }

    public void changeHealth(int differenceHealth)
    {
        health += differenceHealth;
        _persistentData.saveData.Health = health;
        changeHealthCounter(health);
        _dataProvider.Save();
    }

    public void changeArmor(int differenceArmor)
    {
        armor += differenceArmor;
        _persistentData.saveData.Armor = armor;
        changeArmorCounter(armor);
        _dataProvider.Save();
    }

    public void initCounters()
    {
        changeFishCounter(fish);
        changeFoodCounter(food);
        changeHealthCounter(health);
        changeArmorCounter(armor);
        changeUpgradePointsCounter(upgradePoints);
        countPriceUpgrade();
    }

    public void changeUpgradePointsCounter(int upgradePoints)
    {
        upgradeCounter.text = $"Доступно: {upgradePoints.ToString()}<sprite=11>";
    }
    public void changeHealthCounter(int health)
    {
        healthCounter.text = health.ToString() + "<sprite=0>";
    }

    public void changeArmorCounter(int armor)
    {
        armorCounter.text = armor.ToString() + "<sprite=1>";
    }

    public void changeFishCounter(int fish)
    {
        int fishView = fish >= 100000 ? (fish / 1000) : fish;
        string addFishK = fish >= 100000 ? "K" : "";
        fishCounter.text = fishView.ToString() + addFishK;
    }

    public void changeFoodCounter(int food)
    {
        int foodView = food >= 100000 ? (food / 1000) : food;
        string addFoodK = food >= 100000 ? "K" : "";
        foodCounter.text = foodView.ToString() + addFoodK;
    }

    public void changeFish(int difference)
    {
        fish = fish += difference;
        _persistentData.saveData.Fish = fish;
        changeFishCounter(fish);
        _dataProvider.Save();
    }

    public void changeFishAndFood(int difference_fish, int difference_food)
    {
        fish = fish += difference_fish;
        _persistentData.saveData.Fish = fish;
        changeFishCounter(fish);

        food = food += difference_food;
        _persistentData.saveData.Food = food;
        changeFoodCounter(food);

        _dataProvider.Save();
    }

    public void setFishAndFood(int fish, int food)
    {
        _persistentData.saveData.Fish = fish;
        changeFishCounter(fish);

        _persistentData.saveData.Food = food;
        changeFoodCounter(food);

        _dataProvider.Save();
    }

    public bool spendFood(int difference)
    {
        if (food + difference >= 0)
        {
            food = food += difference;
            _persistentData.saveData.Food = food;
            changeFoodCounter(food);

            _dataProvider.Save();
            return true;
        }
        else return false;
    }
    public void buyFood(int amount)
    {
        int price = 0;
        switch (amount)
        {
            case 1:
                price = 750;
                break;
            case 5:
                price = 3000;
                break;
            case 20:
                price = 10000;
                break;
        }
        if(fish >= price)
        changeFishAndFood(-price, amount);
    }

    public void changeFood(int difference)
    {
        food = food += difference;
        _persistentData.saveData.Food = food;
        changeFoodCounter(food);

        _dataProvider.Save();
    }
    public void StartGame()
    {
        _dataProvider.Save();
        SceneManager.LoadScene(1);
    }

    public void getRewardForAd(int amount)
    {
       changeFood(amount);
    }

}
