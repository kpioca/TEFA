using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
using TMPro;


public struct StagesSpawnParameters
{
    public int min_enemies1;
    public int max_enemies1;

    public int min_enemies2;
    public int max_enemies2;

    public int min_enemies3;
    public int max_enemies3;

    public int min_traps1;
    public int max_traps1;

    public int min_traps2;
    public int max_traps2;

    public int min_traps3;
    public int max_traps3;

    public int min_bonuses1;
    public int max_bonuses1;

    public int min_bonuses2;
    public int max_bonuses2;

    public int min_bonuses3;
    public int max_bonuses3;

    public float chance_Bonuses1;
    public float chance_Bonuses2;
    public float chance_Bonuses3;

    public StagesSpawnParameters(LevelPropertiesDatabase database)
    {
        min_enemies1 = database.Min_Enemy_stage1;
        min_enemies2 = database.Min_Enemy_stage2;
        min_enemies3 = database.Min_Enemy_stage3;

        max_enemies1 = database.Max_Enemy_stage1;
        max_enemies2 = database.Max_Enemy_stage2;
        max_enemies3 = database.Max_Enemy_stage3;

        min_traps1 = database.Min_Traps_stage1;
        min_traps2 = database.Min_Traps_stage2;
        min_traps3 = database.Min_Traps_stage3;

        max_traps1 = database.Max_Traps_stage1;
        max_traps2 = database.Max_Traps_stage2;
        max_traps3 = database.Max_Traps_stage3;

        min_bonuses1 = database.Min_Bonuses_stage1;
        min_bonuses2 = database.Min_Bonuses_stage2;
        min_bonuses3 = database.Min_Bonuses_stage3;

        max_bonuses1 = database.Max_Bonuses_stage1;
        max_bonuses2 = database.Max_Bonuses_stage2;
        max_bonuses3 = database.Max_Bonuses_stage3;

        chance_Bonuses1 = database.Chance_Bonuses_stage1;
        chance_Bonuses2 = database.Chance_Bonuses_stage2;
        chance_Bonuses3 = database.Chance_Bonuses_stage3;
    }
}

public class GeneratorLevel : MonoBehaviour
{
    

    GeneratorLevelProperties generator;
    [Header("References")]
    [SerializeField] LevelPropertiesDatabase database;
    [SerializeField] GameManager gameManager;
    [SerializeField] private TMP_Text text_info_level;

    private StagesSpawnParameters spawnParameters;

    //
    [Header("Object properties")]
    [SerializeField] private List<EnemyInfo> enemy_properties;
    public List<EnemyInfo> Enemy_properties => enemy_properties;
    [SerializeField] private List<TrapInfo> traps_properties;
    public List<TrapInfo> Traps_properties => traps_properties;
    [SerializeField] private List<BonusInfo> bonuses_properties;
    public List<BonusInfo> Bonuses_properties => bonuses_properties;

    [SerializeField] private List<ObjectProperties> islands_properties;
    public List<ObjectProperties> Islands_properties => islands_properties;

    [SerializeField] private List<ObjectProperties> roadParts_properties;
    public List<ObjectProperties> RoadParts_properties => roadParts_properties;

    [SerializeField] private List<ObjectProperties> misc_properties;
    public List<ObjectProperties> Misc_properties => misc_properties;
    //

    //
    [Header("Object dictionaries")]
    [SerializeField] private Dictionary<string, EnemyInfo> enemy_propertiesDict;
    [SerializeField] private Dictionary<string, TrapInfo> traps_propertiesDict;
    [SerializeField] private Dictionary<string, BonusInfo> bonuses_propertiesDict;
    [SerializeField] private Dictionary<string, ObjectProperties> islands_propertiesDict;
    [SerializeField] private Dictionary<string, ObjectProperties> roadParts_propertiesDict;
    [SerializeField] private Dictionary<string, ObjectProperties> misc_propertiesDict;
    //

    float openedPartItemsStage1;
    float openedPartItemsStage2;
    float openedPartItemsStage3;

    GameObject fishMoney_prefab;

    LevelSkinSetDatabase skinSet;

    List<EnemyInfo> openedEnemy;
    List<TrapInfo> openedTraps;
    List<BonusInfo> openedBonuses;

    int n_AllSpawnedPlatforms;
    int n_securePlatforms;
    float distZbetweenPlatforms;

    [SerializeField]int n_stage;

    List<GameObject> ready_partsOfPath = new List<GameObject>(4);




    void Start()
    {
        GlobalEventManager.OnPathWaySpawn += spawnNewPathWay;
        GlobalEventManager.OnChangeStageGame += newStageGame;
        init(out generator, database);
        GlobalEventManager.OnUnSubscribe += unSubscribe;

    }

    void unSubscribe()
    {
        GlobalEventManager.OnPathWaySpawn -= spawnNewPathWay;
        GlobalEventManager.OnChangeStageGame -= newStageGame;
        GlobalEventManager.OnUnSubscribe -= unSubscribe;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void init(out GeneratorLevelProperties generator, LevelPropertiesDatabase database)
    {
        string text = "";
        float rand_num1;
        int rand_num2;

        generator = new GeneratorLevelProperties(database, out text);

        spawnParameters = new StagesSpawnParameters(database);



        enemy_properties = generator.Enemy_properties;
        traps_properties = generator.Traps_properties;
        bonuses_properties = generator.Bonuses_properties;
        islands_properties = generator.Islands_properties;
        roadParts_properties = generator.RoadParts_properties;
        misc_properties = database.Misc_properties;

        enemy_propertiesDict = database.Enemy_propertiesDict;
        traps_propertiesDict = database.Traps_propertiesDict;
        bonuses_propertiesDict = database.Bonuses_propertiesDict;
        islands_propertiesDict = database.Islands_propertiesDict;
        roadParts_propertiesDict = database.RoadParts_propertiesDict;
        misc_propertiesDict = database.Misc_propertiesDict;

        fishMoney_prefab = misc_propertiesDict["money_fish"].Prefab;

        openedPartItemsStage1 = database.Part_openItems_stage1;
        openedPartItemsStage2 = database.Part_openItems_stage2;
        openedPartItemsStage3 = database.Part_openItems_stage3;

        n_AllSpawnedPlatforms = database.Num_allSpawnedPlatforms;
        n_securePlatforms = database.Num_securePlatformsAtBegin;
        distZbetweenPlatforms = database.DistanceZ_between_roads;

        initObjectPools(enemy_properties, traps_properties, bonuses_properties, islands_properties, roadParts_properties, misc_properties);


        rand_num1 = UnityEngine.Random.Range(0, 1f);

        text_info_level.text += $"Enemies: {enemy_properties.Count}\nTraps: {traps_properties.Count}\nBonuses: {bonuses_properties.Count}";

        if (rand_num1 <= database.ChanceModificator)
        {
            rand_num2 = UnityEngine.Random.Range(0, database.Modificators.Count);
            spawnParameters = database.Modificators[rand_num2].Action(spawnParameters);

            text_info_level.text += $"\n\nModificator:\n{database.Modificators[rand_num2].NameModificator}";
        }
        text_info_level.text += text;


        //
        Debug.Log($"---------Spawn Elements Info---------");
        Debug.Log($"Enemies : {enemy_properties.Count}\nTraps : {traps_properties.Count}\nBonuses : {bonuses_properties.Count}");
        Debug.Log("-Enemies-");
        for (int i = 0; i < enemy_properties.Count; i++)
            Debug.Log($"Id : {enemy_properties[i].Id}");
        Debug.Log("-Traps-");
        for (int i = 0; i < traps_properties.Count; i++)
            Debug.Log($"Id : {traps_properties[i].Id}");
        Debug.Log("-Bonuses-");
        for (int i = 0; i < bonuses_properties.Count; i++)
            Debug.Log($"Id : {bonuses_properties[i].Id}");
        //

        n_stage = changeStageGame(1, out openedEnemy, out openedTraps, out openedBonuses, openedPartItemsStage1, enemy_properties, traps_properties, bonuses_properties);

        initSpawnPartPath(ready_partsOfPath, n_AllSpawnedPlatforms, new Vector3(0, 0, 0), distZbetweenPlatforms, roadParts_properties, openedEnemy, openedTraps, openedBonuses, fishMoney_prefab, n_securePlatforms);
    }
    private void initSpawnPartPath(List<GameObject> partsOfPath, int n_parts, Vector3 startPos, float DistanceZ_between_roads, List<ObjectProperties> roadParts_properties, List<EnemyInfo> openedEnemy, List<TrapInfo> openedTraps, List<BonusInfo> openedBonuses, GameObject fishMoney_prefab, int n_securePlatforms, int n_stage = 1)
    {
        GameObject temp_obj;
        int i;

        if(n_securePlatforms > n_parts)
            n_securePlatforms = n_parts-1;

        for (i = 0; i < n_securePlatforms; i++)
        {
            temp_obj = assemblingReadyPieceOfPath(roadParts_properties[0].Prefab, openedEnemy, openedTraps, openedBonuses, fishMoney_prefab, n_stage, true);
            placeObjectToPos(temp_obj, startPos + DistanceZ_between_roads * Vector3.forward * i, Quaternion.identity, null);
            temp_obj.GetComponent<BoxCollider>().enabled = false;
            partsOfPath.Add(temp_obj);
        }

        for (; i < n_parts; i++)
        {
            temp_obj = assemblingReadyPieceOfPath(roadParts_properties[0].Prefab, openedEnemy, openedTraps, openedBonuses, fishMoney_prefab, n_stage);
            placeObjectToPos(temp_obj, startPos + DistanceZ_between_roads * Vector3.forward * i, Quaternion.identity, null);
            partsOfPath.Add(temp_obj);
        }
    }

    private int changeStageGame(int stage, out List<EnemyInfo> openedEnemy, out List<TrapInfo> openedTraps, out List<BonusInfo> openedBonuses, float openedPart, List<EnemyInfo> enemy_properties, List<TrapInfo> traps_properties, List<BonusInfo> bonuses_properties)
    {
        openedEnemy = getOpenedItems(enemy_properties, openedPart);
        openedTraps = getOpenedItems(traps_properties, openedPart);
        openedBonuses = getOpenedItems(bonuses_properties, openedPart);

        assignChancesToOpenedItems(openedEnemy, stage);
        assignChancesToOpenedItems(openedTraps, stage);
        assignChancesToOpenedItems(openedBonuses, stage);

        //
        Debug.Log($"---------STATE {stage}---------");
        Debug.Log("-Enemies-");
        for (int i = 0; i < openedEnemy.Count; i++)
            Debug.Log($"Id : {openedEnemy[i].Id} , chance : {openedEnemy[i].ChanceIfSpawnThisType}");
        Debug.Log("-Traps-");
        for (int i = 0; i < openedTraps.Count; i++)
            Debug.Log($"Id : {openedTraps[i].Id} , chance : {openedTraps[i].ChanceIfSpawnThisType}");
        Debug.Log("-Bonuses-");
        for (int i = 0; i < openedBonuses.Count; i++)
            Debug.Log($"Id : {openedBonuses[i].Id} , chance : {openedBonuses[i].ChanceIfSpawnThisType}");
        //

        return stage;
    }

    private void newStageGame(int new_stage)
    {
        float openedPartItems = 0;
        switch (new_stage)
        {
            case 1:
                openedPartItems = openedPartItemsStage1;
                break;
            case 2:
                openedPartItems = openedPartItemsStage2;
                break;
            case 3:
                openedPartItems = openedPartItemsStage3;
                break;
        }
        n_stage = changeStageGame(new_stage, out openedEnemy, out openedTraps, out openedBonuses, openedPartItems, enemy_properties, traps_properties, bonuses_properties);
    }
    private void spawnNewPathWay()
    {
        GameObject temp_obj;
        if(ready_partsOfPath[0].GetComponent<BoxCollider>().enabled == false)
            ready_partsOfPath[0].GetComponent<BoxCollider>().enabled = true;
        disassemblingReadyPieceOfPath(ready_partsOfPath[0]);
        ready_partsOfPath.RemoveAt(0);
        int n = ready_partsOfPath.Count;

        temp_obj = assemblingReadyPieceOfPath(roadParts_properties[0].Prefab, openedEnemy, openedTraps, openedBonuses, fishMoney_prefab, n_stage);
        placeObjectToPos(temp_obj, ready_partsOfPath[n-1].transform.position + distZbetweenPlatforms * Vector3.forward, Quaternion.identity, null);;
        ready_partsOfPath.Add(temp_obj);
    }
    private List<T> getOpenedItems<T>(List<T> fromList, float openedPart) where T : SpawnElementInfo
    {
        int n = fromList.Count;
        int num_openedItems = 0;
        List<T> listItemsSorted = new List<T>(fromList);
        List<T> listOpenedItems;

        if (n == 0 || openedPart == 0) return new List<T> { };
        else
        {
            num_openedItems = (int)Math.Ceiling(openedPart * n);
            sortBubbleForSpawnElements<T>(listItemsSorted);
            listOpenedItems = listItemsSorted.GetRange(0, num_openedItems);
            return listOpenedItems;
        }
    }
    private void assignChancesToOpenedItems<T>(List<T> listOpenedItems, int num_state = 1) where T : SpawnElementInfo
    {
        int n = listOpenedItems.Count;

        if (n == 0) return;
        else
        {
            int sum = 0;
            float medium;
            float chance = 0;

            List<int> levelOfCoolnessItems = new List<int>();
            for (int i = 0; i < n; i++)
            {
                levelOfCoolnessItems.Add(listOpenedItems[i].LevelOfCoolness);
                sum += listOpenedItems[i].LevelOfCoolness;
            }

            List<int> rateItems = new List<int>(levelOfCoolnessItems);
            rateItems.Reverse();

            medium = (float)sum / n;

            //change chances for states 2 and 3
            if (num_state != 1)
            {
                for (int i = 0; i < n; i++)
                {
                    if (num_state == 2)
                    {
                        if (rateItems[i] <= medium)
                        {
                            rateItems[i]++;
                            sum++;
                        }
                        else if (rateItems[i] != 1)
                        {
                            rateItems[i]--;
                            sum--;
                        }
                    }
                    else if (num_state == 3)
                    {
                        if (rateItems[i] <= medium)
                        {
                            rateItems[i] += 2;
                            sum += 2;
                        }
                        else if (rateItems[i] == 2)
                        {
                            rateItems[i]--;
                            sum--;
                        }
                        else if (rateItems[i] == 3)
                        {
                            rateItems[i] -= 2;
                            sum -= 2;
                        }
                    }
                }
            }

            for (int i = 0; i < n; i++)
            {
                chance = rateItems[i] / (float)sum;
                listOpenedItems[i].ChanceIfSpawnThisType = chance;
            }
        }
    }

    private void sortBubbleForSpawnElements<T>(List<T> list) where T : SpawnElementInfo
    {
        bool swapped;
        int n = list.Count;
        for (int i = 0; i < n; i++)
        {
            swapped = false;
            for (int j = 0; j < n - 1 - i; j++)
            {
                if (list[j + 1].LevelOfCoolness < list[j].LevelOfCoolness)
                {
                    T temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = temp;
                    swapped = true;
                }
            }
            if (!swapped)
                break;
        }
    }

    private bool probabilityFunc(float chance) //return true if event happened, else - false
    {
        if (UnityEngine.Random.value <= chance) return true;
        else return false;
    }

    private T getRandomElementFromList<T>(List<T> list) where T : SpawnElementInfo
    {
        float total_probability= 0;

        foreach (T elem in list)
        {
            total_probability += elem.ChanceIfSpawnThisType;
        }

        float randomPoint = UnityEngine.Random.value * total_probability;

        for (int i = 0; i < list.Count; i++)
        {
            if (randomPoint < list[i].ChanceIfSpawnThisType)
            {
                return list[i];
            }
            else
            {
                randomPoint -= list[i].ChanceIfSpawnThisType;
            }
        }
        return list[list.Count - 1];
    }

    private T getRandomElementFromList<T>(List<T> list, out int numInList) where T : SpawnElementInfo
    {
        float total_probability = 0;

        foreach (T elem in list)
        {
            total_probability += elem.ChanceIfSpawnThisType;
        }

        float randomPoint = UnityEngine.Random.value * total_probability;

        for (int i = 0; i < list.Count; i++)
        {
            if (randomPoint < list[i].ChanceIfSpawnThisType)
            {
                numInList = i;
                return list[numInList];
            }
            else
            {
                randomPoint -= list[i].ChanceIfSpawnThisType;
            }
        }
        numInList = list.Count - 1;
        return list[numInList];
    }

    private GameObject assemblingReadyPieceOfPath(GameObject road_prefab, List<EnemyInfo> openedEnemy, List<TrapInfo> openedTraps, List<BonusInfo> openedBonuses, GameObject fishMoney_prefab, int n_stage, bool secureMode = false)
    {
        GameObject raw_road = spawnObject(road_prefab, null);

        GameObject temp;
        int n_e = 0, n_t = 0, n_b = 0, n_fish = 0, n_extraIsl = 0;
        int k, t, p;

        InfoPieceOfPath info = raw_road.GetComponent<InfoPieceOfPath>();

        List<Mark> islandsSmallMarks = new List<Mark>(getInfoIslandsSmallMarks(info));
        List<Mark> islandsBigMarks = new List<Mark>(getInfoIslandsBigMarks(info));
        List<Mark> islandsTallMarks = new List<Mark>(getInfoIslandsTallMarks(info));
        List<Mark> enemyIslSmMarks = new List<Mark>(getInfoEnemyIslSmallMarks(info));
        List<Mark> enemyIslBgMarks = new List<Mark>(getInfoEnemyIslBigMarks(info));
        List<Mark> enemyIslTlMarks = new List<Mark>(getInfoEnemyIslTallMarks(info));
        List<Mark> trapsMarks = new List<Mark>(getInfoTrapsMarks(info));
        List<Mark> bonusesMarks = new List<Mark>(getInfoBonusesMarks(info));
        List<Mark> miscMarks = new List<Mark>(getInfoMiscMarks(info));
        List<Mark> holesInstances = new List<Mark>(getInfoHoleInstances(info));

        List<Mark> islExtraSmallMarks = new List<Mark>(getInfoExtraIslSmallMarks(info));
        List<Mark> islExtraBigMarks = new List<Mark>(getInfoExtraIslBigMarks(info));
        List<Mark> islExtraTallMarks = new List<Mark>(getInfoExtraIslTallMarks(info));

        float chanceSpawnBonus = 0;

        List<Mark> temp_marks1;
        List<Mark> temp_marks2;
        List<Mark> temp_marks3;

        n_extraIsl = UnityEngine.Random.Range(database.Min_extraIslands, database.Max_extraIslands + 1);

        switch (n_stage)
        {
            /*
            case 1:
                n_e = UnityEngine.Random.Range(database.Min_Enemy_stage1, database.Max_Enemy_stage1 + 1);
                n_t = UnityEngine.Random.Range(database.Min_Traps_stage1, database.Max_Traps_stage1 + 1);
                n_b = UnityEngine.Random.Range(database.Min_Bonuses_stage1, database.Max_Bonuses_stage1 + 1);
                n_fish = UnityEngine.Random.Range(database.Min_fishMoney_stage1, database.Max_fishMoney_stage1 + 1);
                chanceSpawnBonus = database.Chance_Bonuses_stage1;
                break;
            case 2:
                n_e = UnityEngine.Random.Range(database.Min_Enemy_stage2, database.Max_Enemy_stage2 + 1);
                n_t = UnityEngine.Random.Range(database.Min_Traps_stage2, database.Max_Traps_stage2 + 1);
                n_b = UnityEngine.Random.Range(database.Min_Bonuses_stage2, database.Max_Bonuses_stage2 + 1);
                n_fish = UnityEngine.Random.Range(database.Min_fishMoney_stage2, database.Max_fishMoney_stage2 + 1);
                chanceSpawnBonus = database.Chance_Bonuses_stage2;
                break;
            case 3:
                n_e = UnityEngine.Random.Range(database.Min_Enemy_stage3, database.Max_Enemy_stage3 + 1);
                n_t = UnityEngine.Random.Range(database.Min_Traps_stage3, database.Max_Traps_stage3 + 1);
                n_b = UnityEngine.Random.Range(database.Min_Bonuses_stage3, database.Max_Bonuses_stage3 + 1);
                n_fish = UnityEngine.Random.Range(database.Min_fishMoney_stage3, database.Max_fishMoney_stage3 + 1);
                chanceSpawnBonus = database.Chance_Bonuses_stage3;
                break;
            */

            case 1:
                n_e = UnityEngine.Random.Range(spawnParameters.min_enemies1, spawnParameters.max_enemies1 + 1);
                n_t = UnityEngine.Random.Range(spawnParameters.min_traps1, spawnParameters.max_traps1 + 1);
                n_b = UnityEngine.Random.Range(spawnParameters.min_bonuses1, spawnParameters.max_bonuses1 + 1);
                n_fish = UnityEngine.Random.Range(database.Min_fishMoney_stage1, database.Max_fishMoney_stage1 + 1);
                chanceSpawnBonus = spawnParameters.chance_Bonuses1;
                break;
            case 2:
                n_e = UnityEngine.Random.Range(spawnParameters.min_enemies2, spawnParameters.max_enemies2 + 1);
                n_t = UnityEngine.Random.Range(spawnParameters.min_traps2, spawnParameters.max_traps2 + 1);
                n_b = UnityEngine.Random.Range(spawnParameters.min_bonuses2, spawnParameters.max_bonuses2 + 1);
                n_fish = UnityEngine.Random.Range(database.Min_fishMoney_stage2, database.Max_fishMoney_stage2 + 1);
                chanceSpawnBonus = spawnParameters.chance_Bonuses2;
                break;
            case 3:
                n_e = UnityEngine.Random.Range(spawnParameters.min_enemies3, spawnParameters.max_enemies3 + 1);
                n_t = UnityEngine.Random.Range(spawnParameters.min_traps3, spawnParameters.max_traps3 + 1);
                n_b = UnityEngine.Random.Range(spawnParameters.min_bonuses3, spawnParameters.max_bonuses3 + 1);
                n_fish = UnityEngine.Random.Range(database.Min_fishMoney_stage3, database.Max_fishMoney_stage3 + 1);
                chanceSpawnBonus = spawnParameters.chance_Bonuses3;
                break;
        }

        if (secureMode == true)
        {
            n_e = 0;
            n_t = 0;
        }


        temp_marks1 = new List<Mark>();
        temp_marks2 = new List<Mark>();
        temp_marks3 = new List<Mark>();

        for (int i = 0; i < n_e; i++)
        {
            k = UnityEngine.Random.Range(1, 4);

            if (k == 1 && islandsSmallMarks.Count != 0)
                spawnIsland(raw_road, "isl_sm", islandsSmallMarks, enemyIslSmMarks, temp_marks1, islands_propertiesDict, info);

            else if (k == 2 && islandsBigMarks.Count != 0)
                spawnIsland(raw_road, "isl_bg", islandsBigMarks, enemyIslBgMarks, temp_marks2, islands_propertiesDict, info);

            else if (k == 3 && islandsTallMarks.Count != 0)
                spawnIsland(raw_road, "isl_tl", islandsTallMarks, enemyIslTlMarks, temp_marks3, islands_propertiesDict, info);

        }

        enemyIslSmMarks = temp_marks1;
        enemyIslBgMarks = temp_marks2;
        enemyIslTlMarks = temp_marks3;

        for (int i = 0; i < n_extraIsl; i++)
        {
            k = UnityEngine.Random.Range(1, 4);

            if (k == 1 && islExtraSmallMarks.Count != 0)
                spawnExtraIsland(raw_road, new string[] { "isl_extraSm1", "isl_extraSm2" }, islExtraSmallMarks, islands_propertiesDict, info);

            else if (k == 2 && islExtraBigMarks.Count != 0)
                spawnExtraIsland(raw_road, new string[] { "isl_extraBg1", "isl_extraBg2" }, islExtraBigMarks, islands_propertiesDict, info);

            else if (k == 3 && islExtraTallMarks.Count != 0)
                spawnExtraIsland(raw_road, new string[] { "isl_extraTl1", "isl_extraTl2" }, islExtraTallMarks, islands_propertiesDict, info);

        }

        for (int i = 0;  i < n_e; i++)
        {
            if (enemyIslSmMarks.Count != 0)
                createEnemy(raw_road, openedEnemy, enemyIslSmMarks, info);

            else if(enemyIslBgMarks.Count != 0)
                createEnemy(raw_road, openedEnemy, enemyIslBgMarks, info);

            else if (enemyIslTlMarks.Count != 0)
                createEnemy(raw_road, openedEnemy, enemyIslTlMarks, info);
        }

        for (int i = 0; i < n_t; i++)
            if(trapsMarks.Count != 0)
                createTrap(raw_road, openedTraps, trapsMarks, holesInstances, info);

        if (probabilityFunc(chanceSpawnBonus))
            for (int i = 0; i < n_b; i++)
                if (bonusesMarks.Count != 0)
                    createBonus(raw_road, openedBonuses, bonusesMarks, info);

        for (int i = 0; i < n_fish; i++)
            if (miscMarks.Count != 0)
                spawnMisc(raw_road, fishMoney_prefab, miscMarks, info);

        return raw_road;
    }


    private void spawnIsland(GameObject raw_road, string id_island, List<Mark> islandsMarks, List<Mark> enemyOnIslMarks, List<Mark> spawnedIslandsMarks, Dictionary<string, ObjectProperties> islands_propertiesDict, InfoPieceOfPath info)
    {
        GameObject temp;
        int t;

        t = UnityEngine.Random.Range(0, islandsMarks.Count);
        temp = spawnObject(islands_propertiesDict[id_island].Prefab, islandsMarks[t].obj.transform.position, islandsMarks[t].obj.transform.rotation, raw_road.transform);

        int num = info.Islands.Count;
        SpawnPlace spawnPlace = new SpawnPlace(temp, islandsMarks[t], num);
        info.Islands.Add(spawnPlace);
        islandsMarks[t].isTaken = true;

        islandsMarks.RemoveAt(t);
        spawnedIslandsMarks.Add(enemyOnIslMarks[t]);
        enemyOnIslMarks[t].isTaken = true;
        enemyOnIslMarks[t].spawnPlace = spawnPlace;

        enemyOnIslMarks.RemoveAt(t);
    }

    private void spawnExtraIsland(GameObject raw_road, string[] id_islandsVariants, List<Mark> islandsMarks, Dictionary<string, ObjectProperties> islands_propertiesDict, InfoPieceOfPath info)
    {
        GameObject temp;
        int t, p, n;

        t = UnityEngine.Random.Range(0, islandsMarks.Count);
        n = id_islandsVariants.Length;
        p = UnityEngine.Random.Range(0, n);

        temp = spawnObject(islands_propertiesDict[id_islandsVariants[p]].Prefab, islandsMarks[t].obj.transform.position, islandsMarks[t].obj.transform.rotation, raw_road.transform);

        int num = info.Islands.Count;
        SpawnPlace spawnPlace = new SpawnPlace(temp, islandsMarks[t], num);
        info.Islands.Add(spawnPlace);
        islandsMarks[t].isTaken = true;
        islandsMarks[t].spawnPlace = spawnPlace;
        islandsMarks.RemoveAt(t);
    }

    private void spawnMisc(GameObject raw_road, GameObject prefab, List<Mark> miscMarks, InfoPieceOfPath info)
    {
        GameObject temp;
        int k;

        k = UnityEngine.Random.Range(0, miscMarks.Count);
        temp = spawnObject(prefab, miscMarks[k].obj.transform.position, miscMarks[k].obj.transform.rotation, raw_road.transform);

        DestroyableAndCollectable destr;

        destr = temp.GetComponent<DestroyableAndCollectable>();
        if (destr != null)
        {
            destr.num = info.Misc.Count;
            destr.info = info;
            destr.type = "misc";
        }

        int num = info.Misc.Count;
        SpawnPlace spawnPlace = new SpawnPlace(temp, miscMarks[k], num);
        info.Misc.Add(spawnPlace);
        miscMarks[k].isTaken = true;
        miscMarks[k].spawnPlace = spawnPlace;
        miscMarks.RemoveAt(k);
    }

    private void createEnemy(GameObject raw_road, List<EnemyInfo> openedSpawnElements, List<Mark> spawnElementMarks, InfoPieceOfPath info)
    {
        GameObject temp;
        int k, num;
        ContentEnemy infoElement;
        EnemyInfo element;

        k = UnityEngine.Random.Range(0, spawnElementMarks.Count);
        temp = spawnObject(getRandomElementFromList(openedSpawnElements, out num).Prefab, spawnElementMarks[k].obj.transform.position, spawnElementMarks[k].obj.transform.rotation, raw_road.transform);
        infoElement = temp.GetComponent<ContentEnemy>();
        infoElement.game_Manager = gameManager;
        infoElement.visionZone.turnToInitialState();

        element = openedSpawnElements[num];

        int num2 = info.Enemies.Count;
        SpawnPlace spawnPlace = new SpawnPlace(temp, spawnElementMarks[k], num2);
        info.Enemies.Add(spawnPlace);
        spawnElementMarks[k].isTaken = true;
        spawnElementMarks[k].spawnPlace = spawnPlace;
        infoElement.setInfoSpawnElement(element, info);

        spawnElementMarks.RemoveAt(k);
    }

    private void createTrap(GameObject raw_road, List<TrapInfo> openedSpawnElements, List<Mark> spawnElementMarks, List<Mark> holesInstances, InfoPieceOfPath info)
    {
        GameObject temp;
        int k, num;
        ContentTrap infoElement;
        TrapInfo element;

        Mark hole;


        k = UnityEngine.Random.Range(0, spawnElementMarks.Count);
        temp = spawnObjectWithPrefabParameters(getRandomElementFromList(openedSpawnElements, out num).Prefab, spawnElementMarks[k].obj.transform.position, raw_road.transform);
        infoElement = temp.GetComponent<ContentTrap>();
        //infoElement.game_Manager = gameManager;

        element = openedSpawnElements[num];

        DestroyableAndCollectable destr;

        destr = temp.GetComponent<DestroyableAndCollectable>();
        if (destr != null)
        {
            destr.num = info.Traps.Count;
            destr.info = info;
            destr.type = "trap";
        }

        int num2 = info.Traps.Count;
        SpawnPlace spawnPlace = new SpawnPlace(temp, spawnElementMarks[k], num2);
        info.Traps.Add(spawnPlace);
        spawnElementMarks[k].isTaken = true;
        spawnElementMarks[k].spawnPlace = spawnPlace;
        if (element.DoHoleInRoad)
        {
            
            hole = holesInstances[k];
            num2 = info.CurrHoles.Count;
            spawnPlace = new SpawnPlace(hole.obj, hole, num2);
            info.CurrHoles.Add(spawnPlace);
            hole.isTaken = true;
            hole.spawnPlace = spawnPlace;
            hole.obj.SetActive(false);
        }
        //infoElement.setInfoSpawnElement(element);

        spawnElementMarks.RemoveAt(k);
        holesInstances.RemoveAt(k);
    }

    private void createBonus(GameObject raw_road, List<BonusInfo> openedSpawnElements, List<Mark> spawnElementMarks, InfoPieceOfPath info)
    {
        GameObject temp;
        int k, num;
        ContentBonus infoElement;
        BonusInfo element;

        k = UnityEngine.Random.Range(0, spawnElementMarks.Count);
        temp = spawnObject(getRandomElementFromList(openedSpawnElements, out num).Prefab, spawnElementMarks[k].obj.transform.position, spawnElementMarks[k].obj.transform.rotation, raw_road.transform);
        infoElement = temp.GetComponent<ContentBonus>();
        //infoElement.game_Manager = gameManager;

        element = openedSpawnElements[num];

        DestroyableAndCollectable destr;

        destr = temp.GetComponent<DestroyableAndCollectable>();
        if (destr != null)
        {
            destr.num = info.Bonuses.Count;
            destr.info = info;
            destr.type = "bonus";
        }
        int num2 = info.Bonuses.Count;
        SpawnPlace spawnPlace = new SpawnPlace(temp, spawnElementMarks[k], num2);
        info.Bonuses.Add(spawnPlace);
        spawnElementMarks[k].isTaken = true;
        spawnElementMarks[k].spawnPlace = spawnPlace;
        //infoElement.setInfoSpawnElement(element);

        spawnElementMarks.RemoveAt(k);
    }

    private void placeObjectToPos(GameObject obj, Vector3 pos, Quaternion rotation, Transform parent = null)
    {
        Transform objTrans = obj.transform;
        objTrans.position = pos;
        objTrans.rotation = rotation;
        objTrans.SetParent(parent);
    }

    private GameObject spawnObject(GameObject prefab, Vector3 pos, Quaternion rotation, Transform parent)
    {
        GameObject temp = KhtPool.GetObject(prefab);
        Transform tempTrans = temp.transform;

        tempTrans.position = pos;
        tempTrans.rotation = rotation;
        tempTrans.SetParent(parent);
        temp.SetActive(true);
        return temp;
    }

    private GameObject spawnObjectWithPrefabParameters(GameObject prefab, Vector3 pos, Transform parent)
    {
        GameObject temp = KhtPool.GetObject(prefab);
        Transform tempTrans = temp.transform;

        tempTrans.position = new Vector3(pos.x, tempTrans.position.y, pos.z);
        tempTrans.SetParent(parent);
        temp.SetActive(true);
        return temp;
    }

    private GameObject spawnObject(GameObject prefab, Transform parent)
    {
        GameObject temp = KhtPool.GetObject(prefab);
        temp.transform.SetParent(parent);
        temp.SetActive(true);
        return temp;
    }

    private void despawnObject(GameObject obj)
    {
        KhtPool.ReturnObject(obj);
    }

    private void despawnObjectsFromList(List<SpawnPlace> list)
    {
        int n = list.Count;
        for (int i = 0; i < n; i++)
        {
            list[i].mark.isTaken = false;
            list[i].mark.spawnPlace = null;
            despawnObject(list[i].obj);
        }
        list.Clear();
    }

    private void removeHoles(List<SpawnPlace> list)
    {
        int n = list.Count;
        
        for(int i = 0; i < n; i++)
        {
            list[i].mark.isTaken = false;
            list[i].mark.spawnPlace = null;
            list[i].obj.SetActive(true);
        }
        list.Clear();
    }

    private void disassemblingReadyPieceOfPath(GameObject piecePath)
    {
        InfoPieceOfPath info = piecePath.GetComponent<InfoPieceOfPath>();
        List<SpawnPlace>[] infoForDis = getInfoForDisassembling(info);
        List<SpawnPlace> enemies = infoForDis[0];
        List<SpawnPlace> traps = infoForDis[1];
        List<SpawnPlace> bonuses = infoForDis[2];
        List<SpawnPlace> islands = infoForDis[3];
        List<SpawnPlace> misc = infoForDis[4];
        List<SpawnPlace> holes = infoForDis[5];

        despawnObjectsFromList(enemies);
        despawnObjectsFromList(traps);
        despawnObjectsFromList(bonuses);
        despawnObjectsFromList(islands);
        despawnObjectsFromList(misc);

        if (info.CurrHoles.Count != 0)
            removeHoles(holes);

        despawnObject(piecePath);
    }

    private void initObjectPools(List<EnemyInfo> enemies, List<TrapInfo> traps, List<BonusInfo> bonuses, List<ObjectProperties> islands, List<ObjectProperties> roadParts, List<ObjectProperties> misc)
    {
        int n_e = enemies.Count;
        int n_t = traps.Count;
        int n_b = bonuses.Count;

        List<CannonInfo> cannons = new List<CannonInfo>();
        List<MageBattleInfo> mage_battles = new List<MageBattleInfo>();

        for (int i = 0; i < n_e; i++) {
            if (enemies[i] is CannonInfo)
                cannons.Add((CannonInfo)enemies[i]);
            else if (enemies[i] is MageBattleInfo)
                mage_battles.Add((MageBattleInfo)enemies[i]);
        }

        initObjectPool(enemies);
        initProjectilesPool(cannons);
        initProjectilesPool(mage_battles);
        initObjectPool(traps);
        initObjectPool(bonuses);

        initObjectPool(islands);
        initObjectPool(roadParts);
        initObjectPool(misc);
    }

    private void initObjectPool<T>(List<T> listObjects) where T : SpawnElementInfo
    {
        int n = listObjects.Count;
        int m = 0;
        List<GameObject> tempList = new List<GameObject>();

        for (int i = 0; i < n; i++)
        {
            m = listObjects[i].SizeOfObjectPool;
            for(int j = 0; j < m; j++)
                tempList.Add(KhtPool.GetObject(listObjects[i].Prefab));
        }
        for (int i = 0; i < tempList.Count; i++) KhtPool.ReturnObject(tempList[i]);
    }

    private void initObjectPool(List<ObjectProperties> listObjects)
    {
        int n = listObjects.Count;
        int m = 0;
        List<GameObject> tempList = new List<GameObject>();

        for (int i = 0; i < n; i++)
        {
            m = listObjects[i].SizeOfObjectPool;
            for (int j = 0; j < m; j++)
                tempList.Add(KhtPool.GetObject(listObjects[i].Prefab));
        }
        for (int i = 0; i < tempList.Count; i++) KhtPool.ReturnObject(tempList[i]);
    }

    private void initProjectilesPool<T>(List<T> listObjects) where T : CannonInfo
    {
        int n = listObjects.Count;
        int m = 0;
        List<GameObject> tempList = new List<GameObject>();

        for (int i = 0; i < n; i++)
        {
            m = listObjects[i].SizeOfProjectilePool;
            for (int j = 0; j < m; j++)
                tempList.Add(KhtPool.GetObject(listObjects[i].bulletInfo.Prefab));
        }
        for (int i = 0; i < tempList.Count; i++) KhtPool.ReturnObject(tempList[i]);
    }

    private void initProjectilesPool(List<MageBattleInfo> listObjects)
    {
        int n = listObjects.Count;
        int m = 0;
        List<GameObject> tempList = new List<GameObject>();

        for (int i = 0; i < n; i++)
        {
            m = listObjects[i].SizeOfProjectilePool;
            for (int j = 0; j < m; j++)
                tempList.Add(KhtPool.GetObject(listObjects[i].bulletInfo.Prefab));
        }
        for (int i = 0; i < tempList.Count; i++) KhtPool.ReturnObject(tempList[i]);
    }
    private void initObjectPool(GameObject _object, int amount)
    {
        int n = amount;
        List<GameObject> tempList = new List<GameObject>();

        for (int i = 0; i < n; i++) tempList.Add(KhtPool.GetObject(_object));
        for (int i = 0; i < n; i++) KhtPool.ReturnObject(_object);
    }

    private Mark[] getInfoIslandsSmallMarks(InfoPieceOfPath info)
    {
        return info.Marks_islandsSmall;
    }

    private Mark[] getInfoIslandsBigMarks(InfoPieceOfPath info)
    {
        return info.Marks_islandsBig;
    }

    private Mark[] getInfoIslandsTallMarks(InfoPieceOfPath info)
    {
        return info.Marks_islandsTall;
    }

    private Mark[] getInfoEnemyIslSmallMarks(InfoPieceOfPath info)
    {
        return info.Marks_enemyIslSmall;
    }

    private Mark[] getInfoEnemyIslBigMarks(InfoPieceOfPath info)
    {
        return info.Marks_enemyIslBig;
    }

    private Mark[] getInfoEnemyIslTallMarks(InfoPieceOfPath info)
    {
        return info.Marks_enemyIslTall;
    }

    private Mark[] getInfoTrapsMarks(InfoPieceOfPath info)
    {
        return info.Marks_traps;
    }

    private Mark[] getInfoBonusesMarks(InfoPieceOfPath info)
    {
        return info.Marks_bonuses;
    }

    private Mark[] getInfoMiscMarks(InfoPieceOfPath info)
    {
        return info.Marks_misc;
    }

    private Mark[] getInfoExtraIslSmallMarks(InfoPieceOfPath info)
    {
        return info.Marks_extraIslandsSmall;
    }

    private Mark[] getInfoExtraIslBigMarks(InfoPieceOfPath info)
    {
        return info.Marks_extraIslandsBig;
    }

    private Mark[] getInfoExtraIslTallMarks(InfoPieceOfPath info)
    {
        return info.Marks_extraIslandsTall;
    }

    private Mark[] getInfoHoleInstances(InfoPieceOfPath info)
    {
        return info.HolesInstances;
    }

    private List<SpawnPlace>[] getInfoForDisassembling(InfoPieceOfPath info)
    {
        List<SpawnPlace> enemies = info.Enemies;
        List<SpawnPlace> traps = info.Traps;
        List<SpawnPlace> bonuses = info.Bonuses;
        List<SpawnPlace> islands = info.Islands;
        List<SpawnPlace> misc = info.Misc;
        List<SpawnPlace> holes = info.CurrHoles;

        List<SpawnPlace>[] infoForDis = new List<SpawnPlace>[] {enemies, traps, bonuses, islands, misc, holes};

        return infoForDis;
    }

}

