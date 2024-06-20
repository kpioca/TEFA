using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneratorLevelProperties
{
    private LevelPropertiesDatabase database;
    private int amount_typeEnemy;
    private int amount_typeTraps;
    private int amount_typeBonuses;

    private List<EnemyInfo> enemy_properties;
    public List<EnemyInfo> Enemy_properties => enemy_properties;
    private List<TrapInfo> traps_properties;
    public List <TrapInfo> Traps_properties => traps_properties;
    private List<BonusInfo> bonuses_properties;
    public List<BonusInfo> Bonuses_properties => bonuses_properties;

    private List<ObjectProperties> islands_properties;
    public List<ObjectProperties> Islands_properties => islands_properties;

    private List<ObjectProperties> roadParts_properties;
    public List<ObjectProperties> RoadParts_properties => roadParts_properties;

    private List<EnemyInfo> temp_e;
    private List<TrapInfo> temp_t;
    private List<BonusInfo> temp_b;

    private GameSet gameSet;
    private float chanceGameSet;
    private int n;
    private float rand_num;

    private bool isReadySet = false;
    private int num_set = -1;
    private string setName = "";

    public GeneratorLevelProperties(LevelPropertiesDatabase database, out string setName, out List<float>[] chances)
    {
        this.database = database;
        setName = makeLevelProperties(out chances);
    }

    public string makeLevelProperties(out List<float>[] chances)
    {
        chanceGameSet = database.ChanceSpawnGameSet;
        chances = new List<float>[3];

        rand_num = Random.Range(0, 1f);
        if (rand_num <= chanceGameSet)
        {
            num_set = Random.Range(0, database.GameSets.Count);

            gameSet = database.GameSets[num_set];

            amount_typeEnemy = gameSet.enemies.Count;
            amount_typeTraps = gameSet.traps.Count;
            amount_typeBonuses = gameSet.bonuses.Count;

            enemy_properties = new List<EnemyInfo>(gameSet.enemies);
            traps_properties = new List<TrapInfo>(gameSet.traps);
            bonuses_properties = new List<BonusInfo>(gameSet.bonuses);

            temp_e = new List<EnemyInfo>(database.Enemy_properties);
            temp_t = new List<TrapInfo>(database.Traps_properties);
            temp_b = new List<BonusInfo>(database.Bonuses_properties);

            chances[0] = getChances(temp_e);
            chances[1] = getChances(temp_t);
            chances[2] = getChances(temp_b);

            islands_properties = database.Islands_properties;
            roadParts_properties = database.RoadParts_properties;

            Debug.Log($"GAMESET - {gameSet.nameSet}");
            return $"\n\nGameset:\n{gameSet.nameSet}";
        }
        else
        {
            amount_typeEnemy = Random.Range(database.Min_typeEnemy, database.Max_typeEnemy + 1);
            amount_typeTraps = Random.Range(database.Min_typeTraps, database.Max_typeTraps + 1);
            amount_typeBonuses = Random.Range(database.Min_typeBonuses, database.Max_typeBonuses + 1);

            enemy_properties = new List<EnemyInfo>();
            traps_properties = new List<TrapInfo>();
            bonuses_properties = new List<BonusInfo>();

            temp_e = new List<EnemyInfo>(database.Enemy_properties);
            temp_t = new List<TrapInfo>(database.Traps_properties);
            temp_b = new List<BonusInfo>(database.Bonuses_properties);

            chances[0] = getChances(temp_e);
            chances[1] = getChances(temp_t);
            chances[2] = getChances(temp_b);

            //check amount of items
            if (amount_typeEnemy <= database.Enemy_properties.Count &&
               amount_typeTraps <= database.Traps_properties.Count &&
               amount_typeBonuses <= database.Bonuses_properties.Count)
            {
                n = database.Enemy_properties.Count;
                addRandomItemsFromTo<EnemyInfo>(temp_e, amount_typeEnemy, out enemy_properties);

                n = database.Traps_properties.Count;
                addRandomItemsFromTo<TrapInfo>(temp_t, amount_typeTraps, out traps_properties);

                n = database.Bonuses_properties.Count;
                addRandomItemsFromTo<BonusInfo>(temp_b, amount_typeBonuses, out bonuses_properties);

                islands_properties = database.Islands_properties;
                roadParts_properties = database.RoadParts_properties;
            }
            else { Debug.Log("Wrong amount of enemy, traps or bonuses"); }
        }
        return "";
    }

    //public void addRandomItemsFromTo<T>(List<T> fromList, int amount, int randMaxExclusive, List<T> toList)
    //{
    //    int rand_num;

    //    for(int i = 0; i < amount; i++)
    //    {
    //        rand_num = Random.Range(0, randMaxExclusive);
    //        toList.Add(fromList[rand_num]);
    //        fromList.RemoveAt(rand_num);
    //        randMaxExclusive--;
    //    }
    //}

    public void addRandomItemsFromTo<T>(List<T> fromList, int amount, out List<T> toList) where T : SpawnElementInfo
    {
        toList = getElementsWithChance(fromList, amount);
    }

    private List<T> getElementsWithChance<T>(List<T> listItems, int amount) where T : SpawnElementInfo
    {
        int n;
        int k;
        int j = 0;
        int i = 0;

        List<T> elements = new List<T>();

        for (j = 0; j < amount; j++)
        {
            n = listItems.Count;
            if (n == 0) return elements;
            else
            {
                int sum = 0;
                float medium;
                float chance = 0;
                List<float> chances = new List<float>();

                List<int> levelOfCoolnessItems = new List<int>();
                for (i = 0; i < n; i++)
                {
                    levelOfCoolnessItems.Add(listItems[i].LevelOfCoolness);
                    sum += listItems[i].LevelOfCoolness;
                }

                List<int> rateItems = new List<int>(levelOfCoolnessItems);
                rateItems.Reverse();

                medium = (float)sum / n;

                for (i = 0; i < n; i++)
                {
                    chance = rateItems[i] / (float)sum;
                    chances.Add(chance);
                }

                elements.Add(getRandomElementFromList(listItems, chances, out k));
                listItems.RemoveAt(k);
            }
        }
        return elements;
    }

    private List<float> getChances<T>(List<T> listItems) where T : SpawnElementInfo
    {
        int n;
        int k;
        int j = 0;
        int i = 0;

        List<float> chances = new List<float>();

        n = listItems.Count;
        if (n == 0) return chances;
        else
        {
            int sum = 0;
            float chance = 0;

            List<int> levelOfCoolnessItems = new List<int>();
            for (i = 0; i < n; i++)
            {
                levelOfCoolnessItems.Add(listItems[i].LevelOfCoolness);
                sum += listItems[i].LevelOfCoolness;
            }

            List<int> rateItems = new List<int>(levelOfCoolnessItems);
            rateItems.Reverse();

            for (i = 0; i < n; i++)
            {
                chance = rateItems[i] / (float)sum;
                chances.Add(chance);
            }
        }

        return chances;
    }

    private T getRandomElementFromList<T>(List<T> list, List<float> chances, out int numInList) where T : SpawnElementInfo
    {
        float total_probability = 0;

        for(int i = 0; i < list.Count; i++)
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
