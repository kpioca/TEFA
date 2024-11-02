using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gift_Properties", menuName = "LevelProperties/Bonuses/New Gift_Properties")]
public class GiftInfo : BonusInfo
{
    [SerializeField] StatusEffectInfo effectInfo;

    [Header("Bonus parameters")]

    [SerializeField] float chanceGetHealth = 0.15f;
    [SerializeField] int maxHealth = 9;

    [SerializeField] float chanceGetFish = 0.34f;
    [SerializeField] int amountFish = 25;

    [SerializeField] float chanceGetFood = 0.01f;
    [SerializeField] int amountFood = 1;

    [SerializeField] BonusInfo[] possibleBonuses;
    public override void Action(GameManager gameManager, ContentPlayer contentPlayer)
    {
        int n = possibleBonuses.Length;

        List<string> names = new List<string>() { "bonus", "health", "fish", "food" };
        List<float> chances = new List<float>() { 0.5f, chanceGetHealth, chanceGetFish, chanceGetFood };

        string result = getRandomElementFromList(names, chances);

        switch (result)
        {
            case "bonus":
                int num = Random.Range(0, n);
                BonusInfo bonus = possibleBonuses[num];
                bonus.Action(gameManager, contentPlayer);
                break;
            case "health":
                if (contentPlayer.Health < maxHealth)
                {
                    contentPlayer.Health += 1;
                    contentPlayer.changeCounterHealth(contentPlayer.Health);
                }
                break;
            case "fish":
                contentPlayer.game_Manager.AddMoney(amountFish);
                break;
            case "food":
                contentPlayer.game_Manager.AddFood(amountFood);
                break;
        }
    }

    private string getRandomElementFromList(List<string> names, List<float> chances)
    {
        float total_probability = 0;

        foreach (float elem in chances)
        {
            total_probability += elem;
        }

        int n = chances.Count;

        float randomPoint = UnityEngine.Random.value * total_probability;

        for (int i = 0; i < n; i++)
        {
            if (randomPoint < chances[i])
            {
                return names[i];
            }
            else
            {
                randomPoint -= chances[i];
            }
        }
        return names[n - 1];
    }
}
