using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

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
    private List<Stamp> stamps = new List<Stamp>();
    public List<Stamp> Stamps => stamps;

    private GameSet gameSet;
    private float chanceGameSet;
    private int n;
    private float rand_num;

    private bool isReadySet = false;
    private int num_set = -1;
    private string setName = "";

    

    public GeneratorLevelProperties(LevelPropertiesDatabase database, out string setName, out GameSet gameSet, out List<float>[] chances)
    {
        this.database = database;
        setName = makeLevelProperties(out chances, out gameSet);
    }

    public string makeLevelProperties(out List<float>[] chances, out GameSet gameSet)
    {
        chanceGameSet = database.ChanceSpawnGameSet;
        chances = new List<float>[3];
        gameSet = null;

        List<GameSet> databaseGameSets = database.GameSets;
        List<EnemyInfo> databaseEnemy = database.Enemy_properties;
        List<TrapInfo> databaseTraps = database.Traps_properties;
        List<BonusInfo> databaseBonuses = database.Bonuses_properties;
        List<ObjectProperties> databaseIslands = database.Islands_properties;
        List<ObjectProperties> databaseRoadParts = database.RoadParts_properties;

        rand_num = Random.Range(0, 1f);
        if (rand_num <= chanceGameSet && databaseGameSets.Count > 0)
        {
            num_set = Random.Range(0, databaseGameSets.Count);

            gameSet = databaseGameSets[num_set];

            amount_typeEnemy = gameSet.Enemies.Count;
            amount_typeTraps = gameSet.Traps.Count;
            amount_typeBonuses = gameSet.Bonuses.Count;

            enemy_properties = new List<EnemyInfo>(gameSet.Enemies);
            traps_properties = new List<TrapInfo>(gameSet.Traps);
            bonuses_properties = new List<BonusInfo>(gameSet.Bonuses);

            stamps = getStamps(enemy_properties.Count);

            temp_e = new List<EnemyInfo>(databaseEnemy);
            temp_t = new List<TrapInfo>(databaseTraps);
            temp_b = new List<BonusInfo>(databaseBonuses);

            chances[0] = getChances(temp_e);
            chances[1] = getChances(temp_t);
            chances[2] = getChances(temp_b);

            islands_properties = databaseIslands;
            roadParts_properties = databaseRoadParts;

            Debug.Log($"GAMESET - {gameSet.NameSet}");
            return $"\n\nGameset:\n{gameSet.NameSet}";
        }
        else
        {
            temp_e = new List<EnemyInfo>(databaseEnemy);
            temp_t = new List<TrapInfo>(databaseTraps);
            temp_b = new List<BonusInfo>(databaseBonuses);

            int r1 = Random.Range(database.Min_typeEnemy, database.Max_typeEnemy + 1);
            int r2 = Random.Range(database.Min_typeTraps, database.Max_typeTraps + 1);
            int r3 = Random.Range(database.Min_typeBonuses, database.Max_typeBonuses + 1);

            amount_typeEnemy = r1 > temp_e.Count ? temp_e.Count : r1;
            amount_typeTraps = r2 > temp_t.Count ? temp_t.Count : r2;
            amount_typeBonuses = r3 > temp_b.Count ? temp_b.Count : r3;

            enemy_properties = new List<EnemyInfo>();
            traps_properties = new List<TrapInfo>();
            bonuses_properties = new List<BonusInfo>();

            chances[0] = getChances(temp_e);
            chances[1] = getChances(temp_t);
            chances[2] = getChances(temp_b);

            //check amount of items
            if (amount_typeEnemy <= databaseEnemy.Count &&
               amount_typeTraps <= databaseTraps.Count &&
               amount_typeBonuses <= databaseBonuses.Count)
            {
                temp_e.Sort();
                temp_t.Sort();
                temp_b.Sort();

                n = databaseEnemy.Count;
                addRandomItemsFromTo<EnemyInfo>(temp_e, amount_typeEnemy, out enemy_properties);

                n = database.Traps_properties.Count;
                addRandomItemsFromTo<TrapInfo>(temp_t, amount_typeTraps, out traps_properties);

                n = database.Bonuses_properties.Count;
                addRandomItemsFromTo<BonusInfo>(temp_b, amount_typeBonuses, out bonuses_properties);

                islands_properties = databaseIslands;
                roadParts_properties = databaseRoadParts;
            }
            else { Debug.Log("Wrong amount of enemy, traps or bonuses"); }
            stamps = getStamps(enemy_properties.Count);
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

    public List<Stamp> getStamps(int amountEnemy)
    {
        int max_n = database.MaxNumStamps;
        float chanceStamp = database.ChanceAppearanceStamp;
        int n = max_n > amountEnemy ? amountEnemy : max_n;
        bool resultRandom;
        List<StampInfo> allStamps = database.StampInfos;
        List<Stamp> stamps = new List<Stamp>();
        n = allStamps.Count > 0 ? n : 0;

        for(int i = 0; i < n; i++)
        {
            resultRandom = probabilityFunc(chanceStamp);
            if (resultRandom)
                stamps.Add(Stamp.createStampFromInfo(getRandomElement(allStamps)));
        }

        return stamps;
    }

    private bool probabilityFunc(float chance) //return true if event happened, else - false
    {
        if (UnityEngine.Random.value <= chance) return true;
        else return false;
    }

    public void addRandomItemsFromTo<T>(List<T> fromList, int amount, out List<T> toList) where T : SpawnElementInfo
    {
        toList = getElementsWithChance(fromList, amount, 1f);
    }

    private T getRandomElement<T>(List<T> listItems) where T : Object
    {
        int n = listItems.Count;
        int k = Random.Range(0, n);
        return listItems[k];
    }

    private List<T> getElementsWithChance<T>(List<T> listItems, int amount, float chanceScaler = 2) where T : SpawnElementInfo
    {
        int n;
        int n_rate;
        int k;
        int p = 0;
        int i = 0;
        int j = 0;

        List<T> elements = new List<T>();

        for (p = 0; p < amount; p++)
        {
            n = listItems.Count;
            if (n == 0) return elements;
            else
            {
                int sum = 0;
                float chance = 0;
                int value = 0;
                List<float> chances = new List<float>();

                List<List<int>> levelOfCoolnessItems = new List<List<int>>();
                List<int> UniqueLevelOfCoolness = new List<int>();
                List<int> groupItems = new List<int>();
                int coolness = 0;
                for (i = 0; i < n; i++)
                {
                    value = (int)(listItems[i].LevelOfCoolness * chanceScaler);
                    //value = (int)Mathf.Pow(listItems[i].LevelOfCoolness, chanceScaler);
                    if (groupItems.Count > 0)
                    {
                        if (value != groupItems[0])
                        {
                            levelOfCoolnessItems.Add(groupItems);
                            UniqueLevelOfCoolness.Add(coolness);
                            groupItems = new List<int>();
                        }
                    }
                    coolness = listItems[i].LevelOfCoolness;
                    groupItems.Add(value);
                }
                if (groupItems.Count != 0)
                {
                    levelOfCoolnessItems.Add(groupItems);
                    UniqueLevelOfCoolness.Add(coolness);
                }

                List<List<int>> rateItems = new List<List<int>>(levelOfCoolnessItems);
                UniqueLevelOfCoolness.Reverse();

                n_rate = rateItems.Count;
                List<int> currGroup;
                int m = 0;
                int levelOfCoolness = 0;
                for (i = 0; i < n_rate; i++)
                {
                    currGroup = rateItems[i];
                    m = currGroup.Count;
                    levelOfCoolness = UniqueLevelOfCoolness[i];
                    for (j = 0; j < m; j++)
                    {
                        currGroup[j] = (int)(levelOfCoolness * chanceScaler);
                        //currGroup[j] = (int)Mathf.Pow(levelOfCoolness, chanceScaler);
                        sum += currGroup[j];
                    }
                }


                for (i = 0; i < n_rate; i++)
                {
                    currGroup = rateItems[i];
                    m = currGroup.Count;
                    chance = (currGroup.Sum() / (float)m)/sum;
                    for (j = 0; j < m; j++)
                        chances.Add(chance);
                }

                /*
                Debug.Log("---");
                for(i = 0; i < n; i++)
                {
                    Debug.Log($"{listItems[i].Id} - {listItems[i].LevelOfCoolness} - {chances[i]}");
                }
                */

                elements.Add(getRandomElementFromList(listItems, chances, out k));
                listItems.RemoveAt(k);
            }
        }
        return elements;
    }
    private List<float> getChances<T>(List<T> listItems, int chanceScaler = 2) where T : SpawnElementInfo
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
            int value = 0;

            List<int> levelOfCoolnessItems = new List<int>();
            for (i = 0; i < n; i++)
            {
                value = chanceScaler * listItems[i].LevelOfCoolness;
                levelOfCoolnessItems.Add(value);
                sum += value;
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
