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

    private List<Enemy> enemy_properties;
    public List<Enemy> Enemy_properties => enemy_properties;
    private List<Trap> traps_properties;
    public List <Trap> Traps_properties => traps_properties;
    private List<Bonus> bonuses_properties;
    public List<Bonus> Bonuses_properties => bonuses_properties;

    private List<ObjectProperties> islands_properties;
    public List<ObjectProperties> Islands_properties => islands_properties;

    private List<ObjectProperties> roadParts_properties;
    public List<ObjectProperties> RoadParts_properties => roadParts_properties;

    private List<Enemy> temp_e;
    private List<Trap> temp_t;
    private List<Bonus> temp_b;
    private int n;
    private int rand_num;

    private bool isReadySet = false;
    private int num_set = -1;

    public GeneratorLevelProperties(LevelPropertiesDatabase database)
    {
        this.database = database;
        makeLevelProperties();
    }

    public void makeLevelProperties()
    {
        amount_typeEnemy = Random.Range(database.Min_typeEnemy, database.Max_typeEnemy + 1);
        amount_typeTraps = Random.Range(database.Min_typeTraps, database.Max_typeTraps + 1);
        amount_typeBonuses = Random.Range(database.Min_typeBonuses, database.Max_typeBonuses + 1);

        enemy_properties = new List<Enemy>();
        traps_properties = new List<Trap>();
        bonuses_properties = new List<Bonus>();

        temp_e = new List<Enemy>(database.Enemy_properties);
        temp_t = new List<Trap>(database.Traps_properties);
        temp_b = new List<Bonus>(database.Bonuses_properties);

        //check amount of items
        if(amount_typeEnemy <= database.Enemy_properties.Count &&
           amount_typeTraps <= database.Traps_properties.Count &&
           amount_typeBonuses <= database.Bonuses_properties.Count)
        {
            n = database.Enemy_properties.Count;
            addRandomItemsFromTo<Enemy>(temp_e, amount_typeEnemy, n, enemy_properties);

            n = database.Traps_properties.Count;
            addRandomItemsFromTo<Trap>(temp_t, amount_typeTraps, n, traps_properties);

            n = database.Bonuses_properties.Count;
            addRandomItemsFromTo<Bonus>(temp_b, amount_typeBonuses, n, bonuses_properties);

            islands_properties = database.Islands_properties;
            roadParts_properties = database.RoadParts_properties;
        }
        else { Debug.Log("Wrong amount of enemy, traps or bonuses"); }
    }

    public void addRandomItemsFromTo<T>(List<T> fromList, int amount, int randMaxExclusive, List<T> toList)
    {
        int rand_num;
        
        for(int i = 0; i < amount; i++)
        {
            rand_num = Random.Range(0, randMaxExclusive);
            toList.Add(fromList[rand_num]);
            fromList.RemoveAt(rand_num);
            randMaxExclusive--;
        }
    }
}
