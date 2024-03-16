using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GeneratorLevel : MonoBehaviour
{
    GeneratorLevelProperties generator;
    [Header("References")]
    [SerializeField] LevelPropertiesDatabase database;
    [SerializeField] GameManager gameManager;

    //
    [Header("Object properties")]
    [SerializeField] private List<Enemy> enemy_properties;
    public List<Enemy> Enemy_properties => enemy_properties;
    [SerializeField] private List<Trap> traps_properties;
    public List<Trap> Traps_properties => traps_properties;
    [SerializeField] private List<Bonus> bonuses_properties;
    public List<Bonus> Bonuses_properties => bonuses_properties;

    [SerializeField] private List<ObjectProperties> islands_properties;
    public List<ObjectProperties> Islands_properties => islands_properties;

    [SerializeField] private List<ObjectProperties> roadParts_properties;
    public List<ObjectProperties> RoadParts_properties => roadParts_properties;

    [SerializeField] private List<ObjectProperties> misc_properties;
    public List<ObjectProperties> Misc_properties => misc_properties;
    //

    //
    [Header("Object dictionaries")]
    [SerializeField] private Dictionary<string, Enemy> enemy_propertiesDict;
    [SerializeField] private Dictionary<string, Trap> traps_propertiesDict;
    [SerializeField] private Dictionary<string, Bonus> bonuses_propertiesDict;
    [SerializeField] private Dictionary<string, ObjectProperties> islands_propertiesDict;
    [SerializeField] private Dictionary<string, ObjectProperties> roadParts_propertiesDict;
    [SerializeField] private Dictionary<string, ObjectProperties> misc_propertiesDict;
    //

    float openedPartItemsStage1;
    float openedPartItemsStage2;
    float openedPartItemsStage3;

    GameObject fishMoney_prefab;

    LevelSkinSetDatabase skinSet;

    List<Enemy> openedEnemy;
    List<Trap> openedTraps;
    List<Bonus> openedBonuses;

    int n_AllSpawnedPlatforms;
    int n_securePlatforms;
    float distZbetweenPlatforms;

    [SerializeField]int n_stage;

    List<GameObject> ready_partsOfPath = new List<GameObject>(4);


    void Start()
    {
        GlobalEventManager.OnPathWaySpawn += spawnNewPathWay;
        GlobalEventManager.OnChangeStageGame += newStageGame;
        init(out generator, database);;


        

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void init(out GeneratorLevelProperties generator, LevelPropertiesDatabase database)
    {
        generator = new GeneratorLevelProperties(database);

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

        n_stage = changeStageGame(1, out openedEnemy, out openedTraps, out openedBonuses, openedPartItemsStage1, enemy_properties, traps_properties, bonuses_properties);

        initSpawnPartPath(ready_partsOfPath, n_AllSpawnedPlatforms, new Vector3(0, 0, 0), distZbetweenPlatforms, roadParts_properties, openedEnemy, openedTraps, openedBonuses, fishMoney_prefab, n_securePlatforms);
    }
    private void initSpawnPartPath(List<GameObject> partsOfPath, int n_parts, Vector3 startPos, float DistanceZ_between_roads, List<ObjectProperties> roadParts_properties, List<Enemy> openedEnemy, List<Trap> openedTraps, List<Bonus> openedBonuses, GameObject fishMoney_prefab, int n_securePlatforms, int n_stage = 1)
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

    private int changeStageGame(int stage, out List<Enemy> openedEnemy, out List<Trap> openedTraps, out List<Bonus> openedBonuses, float openedPart, List<Enemy> enemy_properties, List<Trap> traps_properties, List<Bonus> bonuses_properties)
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
    private List<T> getOpenedItems<T>(List<T> fromList, float openedPart) where T : SpawnElement
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
    private void assignChancesToOpenedItems<T>(List<T> listOpenedItems, int num_state = 1) where T : SpawnElement
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

    private void sortBubbleForSpawnElements<T>(List<T> list) where T : SpawnElement
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

    private T getRandomElementFromList<T>(List<T> list) where T : SpawnElement
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

    private T getRandomElementFromList<T>(List<T> list, out int numInList) where T : SpawnElement
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

    private GameObject assemblingReadyPieceOfPath(GameObject road_prefab, List<Enemy> openedEnemy, List<Trap> openedTraps, List<Bonus> openedBonuses, GameObject fishMoney_prefab, int n_stage, bool secureMode = false)
    {
        GameObject raw_road = spawnObject(road_prefab, null);

        GameObject temp;
        int n_e = 0, n_t = 0, n_b = 0, n_fish = 0, n_extraIsl = 0;
        int k, t, p;

        InfoPieceOfPath info = raw_road.GetComponent<InfoPieceOfPath>();

        List<GameObject> islandsSmallMarks = new List<GameObject>(getInfoIslandsSmallMarks(raw_road));
        List<GameObject> islandsBigMarks = new List<GameObject>(getInfoIslandsBigMarks(raw_road));
        List<GameObject> islandsTallMarks = new List<GameObject>(getInfoIslandsTallMarks(raw_road));
        List<GameObject> enemyIslSmMarks = new List<GameObject>(getInfoEnemyIslSmallMarks(raw_road));
        List<GameObject> enemyIslBgMarks = new List<GameObject>(getInfoEnemyIslBigMarks(raw_road));
        List<GameObject> enemyIslTlMarks = new List<GameObject>(getInfoEnemyIslTallMarks(raw_road));
        List<GameObject> trapsMarks = new List<GameObject>(getInfoTrapsMarks(raw_road));
        List<GameObject> bonusesMarks = new List<GameObject>(getInfoBonusesMarks(raw_road));
        List<GameObject> miscMarks = new List<GameObject>(getInfoMiscMarks(raw_road));

        List<GameObject> islExtraSmallMarks = new List<GameObject>(getInfoExtraIslSmallMarks(raw_road));
        List<GameObject> islExtraBigMarks = new List<GameObject>(getInfoExtraIslBigMarks(raw_road));
        List<GameObject> islExtraTallMarks = new List<GameObject>(getInfoExtraIslTallMarks(raw_road));

        float chanceSpawnBonus = 0;

        List<GameObject> temp_marks1;
        List<GameObject> temp_marks2;
        List<GameObject> temp_marks3;

        n_extraIsl = UnityEngine.Random.Range(database.Min_extraIslands, database.Max_extraIslands + 1);

        switch (n_stage)
        {
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
        }

        if (secureMode == true)
        {
            n_e = 0;
            n_t = 0;
        }


        temp_marks1 = new List<GameObject>();
        temp_marks2 = new List<GameObject>();
        temp_marks3 = new List<GameObject>();

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
                createTrap(raw_road, openedTraps, trapsMarks, info);

        if (probabilityFunc(chanceSpawnBonus))
            for (int i = 0; i < n_b; i++)
                if (bonusesMarks.Count != 0)
                    createBonus(raw_road, openedBonuses, bonusesMarks, info);

        for (int i = 0; i < n_fish; i++)
            if (miscMarks.Count != 0)
                spawnMisc(raw_road, fishMoney_prefab, miscMarks, info);

        return raw_road;
    }


    private void spawnIsland(GameObject raw_road, string id_island, List<GameObject> islandsMarks, List<GameObject> enemyOnIslMarks, List<GameObject> spawnedIslandsMarks, Dictionary<string, ObjectProperties> islands_propertiesDict, InfoPieceOfPath info)
    {
        GameObject temp;
        int t;

        t = UnityEngine.Random.Range(0, islandsMarks.Count);
        temp = spawnObject(islands_propertiesDict[id_island].Prefab, islandsMarks[t].transform.position, islandsMarks[t].transform.rotation, raw_road.transform);
        info.Islands.Add(temp);
        islandsMarks.RemoveAt(t);
        spawnedIslandsMarks.Add(enemyOnIslMarks[t]);
        enemyOnIslMarks.RemoveAt(t);
    }

    private void spawnExtraIsland(GameObject raw_road, string[] id_islandsVariants, List<GameObject> islandsMarks, Dictionary<string, ObjectProperties> islands_propertiesDict, InfoPieceOfPath info)
    {
        GameObject temp;
        int t, p, n;

        t = UnityEngine.Random.Range(0, islandsMarks.Count);
        n = id_islandsVariants.Length;
        p = UnityEngine.Random.Range(0, n);

        temp = spawnObject(islands_propertiesDict[id_islandsVariants[p]].Prefab, islandsMarks[t].transform.position, islandsMarks[t].transform.rotation, raw_road.transform);

        info.Islands.Add(temp);
        islandsMarks.RemoveAt(t);
    }

    private void spawnMisc(GameObject raw_road, GameObject prefab, List<GameObject> miscMarks, InfoPieceOfPath info)
    {
        GameObject temp;
        int k;

        k = UnityEngine.Random.Range(0, miscMarks.Count);
        temp = spawnObject(prefab, miscMarks[k].transform.position, miscMarks[k].transform.rotation, raw_road.transform);
        info.Misc.Add(temp);
        miscMarks.RemoveAt(k);
    }

    private void createEnemy(GameObject raw_road, List<Enemy> openedSpawnElements, List<GameObject> spawnElementMarks, InfoPieceOfPath info)
    {
        GameObject temp;
        int k, num;
        InfoSpawnElement infoElement;
        Enemy element;

        k = UnityEngine.Random.Range(0, spawnElementMarks.Count);
        temp = spawnObject(getRandomElementFromList(openedSpawnElements, out num).Prefab, spawnElementMarks[k].transform.position, spawnElementMarks[k].transform.rotation, raw_road.transform);
        infoElement = temp.GetComponent<InfoSpawnElement>();
        infoElement.game_Manager = gameManager;
        infoElement.visionZone.turnToInitialState();

        element = openedSpawnElements[num];

        info.Enemies.Add(temp);
        infoElement.setInfoSpawnElement(element);
        infoElement.setUpSpawnElement(element);

        spawnElementMarks.RemoveAt(k);
    }

    private void createTrap(GameObject raw_road, List<Trap> openedSpawnElements, List<GameObject> spawnElementMarks, InfoPieceOfPath info)
    {
        GameObject temp;
        int k, num;
        InfoSpawnElement infoElement;
        Trap element;

        k = UnityEngine.Random.Range(0, spawnElementMarks.Count);
        temp = spawnObject(getRandomElementFromList(openedSpawnElements, out num).Prefab, spawnElementMarks[k].transform.position, spawnElementMarks[k].transform.rotation, raw_road.transform);
        infoElement = temp.GetComponent<InfoSpawnElement>();
        infoElement.game_Manager = gameManager;

        element = openedSpawnElements[num];

        info.Traps.Add(temp);
        infoElement.setInfoSpawnElement(element);
        infoElement.setUpSpawnElement(element);

        spawnElementMarks.RemoveAt(k);
    }

    private void createBonus(GameObject raw_road, List<Bonus> openedSpawnElements, List<GameObject> spawnElementMarks, InfoPieceOfPath info)
    {
        GameObject temp;
        int k, num;
        InfoSpawnElement infoElement;
        Bonus element;

        k = UnityEngine.Random.Range(0, spawnElementMarks.Count);
        temp = spawnObject(getRandomElementFromList(openedSpawnElements, out num).Prefab, spawnElementMarks[k].transform.position, spawnElementMarks[k].transform.rotation, raw_road.transform);
        infoElement = temp.GetComponent<InfoSpawnElement>();
        infoElement.game_Manager = gameManager;

        element = openedSpawnElements[num];

        info.Bonuses.Add(temp);
        infoElement.setInfoSpawnElement(element);
        infoElement.setUpSpawnElement(element);

        spawnElementMarks.RemoveAt(k);
    }

    private void placeObjectToPos(GameObject obj, Vector3 pos, Quaternion rotation, Transform parent = null)
    {
        obj.transform.position = pos;
        obj.transform.rotation = rotation;
        obj.transform.SetParent(parent);
    }

    private GameObject spawnObject(GameObject prefab, Vector3 pos, Quaternion rotation, Transform parent)
    {
        GameObject temp = KhtPool.GetObject(prefab);
        temp.transform.position = pos;
        temp.transform.rotation = rotation;
        temp.transform.SetParent(parent);
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

    private void despawnObjectsFromList(List<GameObject> list)
    {
        int n = list.Count;
        for (int i = 0; i < n; i++)
        {
            despawnObject(list[i]);
        }
        list.Clear();
    }
    private void disassemblingReadyPieceOfPath(GameObject piecePath)
    {
        List<GameObject> enemies = getInfoEnemies(piecePath);
        List<GameObject> traps = getInfoTraps(piecePath);
        List<GameObject> bonuses = getInfoBonuses(piecePath);
        List<GameObject> islands = getInfoIslands(piecePath);
        List<GameObject> misc = getInfoMisc(piecePath);

        despawnObjectsFromList(enemies);
        despawnObjectsFromList(traps);
        despawnObjectsFromList(bonuses);
        despawnObjectsFromList(islands);
        despawnObjectsFromList(misc);

        despawnObject(piecePath);
    }

    private void initObjectPools(List<Enemy> enemies, List<Trap> traps, List<Bonus> bonuses, List<ObjectProperties> islands, List<ObjectProperties> roadParts, List<ObjectProperties> misc)
    {
        int n_e = enemies.Count;
        int n_t = traps.Count;
        int n_b = bonuses.Count;

        List<Cannon> cannons = new List<Cannon>();

        for (int i = 0; i < n_e; i++) {
            if (enemies[i].Type == Enemy.enemyType.cannon)
                cannons.Add((Cannon)enemies[i]);
        }

        initObjectPool(enemies);
        initProjectilesPool(cannons);
        initObjectPool(traps);
        initObjectPool(bonuses);

        initObjectPool(islands);
        initObjectPool(roadParts);
        initObjectPool(misc);
    }

    private void initObjectPool<T>(List<T> listObjects) where T : SpawnElement
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

    private void initProjectilesPool<T>(List<T> listObjects) where T : Cannon
    {
        int n = listObjects.Count;
        int m = 0;
        List<GameObject> tempList = new List<GameObject>();

        for (int i = 0; i < n; i++)
        {
            m = listObjects[i].SizeOfProjectilePool;
            for (int j = 0; j < m; j++)
                tempList.Add(KhtPool.GetObject(listObjects[i].PrefabProjectile));
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

    private GameObject[] getInfoIslandsSmallMarks(GameObject road)
    {
        return road.GetComponent<InfoPieceOfPath>().Marks_islandsSmall;
    }

    private GameObject[] getInfoIslandsBigMarks(GameObject road)
    {
        return road.GetComponent<InfoPieceOfPath>().Marks_islandsBig;
    }

    private GameObject[] getInfoIslandsTallMarks(GameObject road)
    {
        return road.GetComponent<InfoPieceOfPath>().Marks_islandsTall;
    }

    private GameObject[] getInfoEnemyIslSmallMarks(GameObject road)
    {
        return road.GetComponent<InfoPieceOfPath>().Marks_enemyIslSmall;
    }

    private GameObject[] getInfoEnemyIslBigMarks(GameObject road)
    {
        return road.GetComponent<InfoPieceOfPath>().Marks_enemyIslBig;
    }

    private GameObject[] getInfoEnemyIslTallMarks(GameObject road)
    {
        return road.GetComponent<InfoPieceOfPath>().Marks_enemyIslTall;
    }

    private GameObject[] getInfoTrapsMarks(GameObject road)
    {
        return road.GetComponent<InfoPieceOfPath>().Marks_traps;
    }

    private GameObject[] getInfoBonusesMarks(GameObject road)
    {
        return road.GetComponent<InfoPieceOfPath>().Marks_bonuses;
    }

    private GameObject[] getInfoMiscMarks(GameObject road)
    {
        return road.GetComponent<InfoPieceOfPath>().Marks_misc;
    }

    private GameObject[] getInfoExtraIslSmallMarks(GameObject road)
    {
        return road.GetComponent<InfoPieceOfPath>().Marks_extraIslandsSmall;
    }

    private GameObject[] getInfoExtraIslBigMarks(GameObject road)
    {
        return road.GetComponent<InfoPieceOfPath>().Marks_extraIslandsBig;
    }

    private GameObject[] getInfoExtraIslTallMarks(GameObject road)
    {
        return road.GetComponent<InfoPieceOfPath>().Marks_extraIslandsTall;
    }

    private List<GameObject> getInfoEnemies(GameObject piecePath)
    {
        return piecePath.GetComponent<InfoPieceOfPath>().Enemies;
    }

    private List<GameObject> getInfoTraps(GameObject piecePath)
    {
        return piecePath.GetComponent<InfoPieceOfPath>().Traps;
    }

    private List<GameObject> getInfoBonuses(GameObject piecePath)
    {
        return piecePath.GetComponent<InfoPieceOfPath>().Bonuses;
    }

    private List<GameObject> getInfoIslands(GameObject piecePath)
    {
        return piecePath.GetComponent<InfoPieceOfPath>().Islands;
    }

    private List<GameObject> getInfoMisc(GameObject piecePath)
    {
        return piecePath.GetComponent<InfoPieceOfPath>().Misc;
    }

}
