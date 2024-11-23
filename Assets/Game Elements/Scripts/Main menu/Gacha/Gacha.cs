using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gacha : MonoBehaviour
{
    [SerializeField] private GachaContent _contentItems;
    [SerializeField] private GachaPanel _gachaPanel;
    [SerializeField] SkinDatabase skinDatabase;
    MainMenuBootstrap _menuBootstrap;

    [SerializeField] GameObject _gachaLayer;
    [SerializeField] private GameObject _controlButtonPanel;

    [Header("Coord for spawn capsules")]
    [SerializeField] private float xRange_1;
    [SerializeField] private float xRange_2;
    [SerializeField] private float yRange_1;
    [SerializeField] private float yRange_2;
    [SerializeField] private float zRange_1;
    [SerializeField] private float zRange_2;


    [SerializeField] private Transform gachaMachine;

    [Header("Parameters for animations")]
    [SerializeField] private GameObject _dial;
    [SerializeField] private GameObject _door;
    [SerializeField] private Vector3 _posCapsule;
    [SerializeField] private GameObject _buyPanel;
    private RarityEnum capsuleRarity;

    private List<GameObject> capsules = new List<GameObject>();

    private BoughtSkinChecker _boughtSkinChecker;
    private SkinUnlocker _skinUnlocker;
    private MainMenuManager _menuManager;

    Dictionary<RarityEnum, GameObject> capsulesDict;
    Dictionary<RarityEnum, List<CatSkin>> gachaSkinDict;
    List<RarityEnum> rarityEnums;
    List<float> chances;

    public void Initialize(BoughtSkinChecker boughtSkinChecker, SkinUnlocker skinUnlocker, MainMenuManager mainMenuManager, MainMenuBootstrap mainMenuBootstrap)
    {
        _boughtSkinChecker = boughtSkinChecker;
        _skinUnlocker = skinUnlocker;
        _menuManager = mainMenuManager;
        _menuBootstrap = mainMenuBootstrap;

        _gachaPanel.Initialize(boughtSkinChecker, skinUnlocker, mainMenuManager);

        capsulesDict = skinDatabase.GachaCapsulesDictionary;
        gachaSkinDict = skinDatabase.GachaCatDictionary;

        InitializeCapsules();
    }
    public void InitializeCapsules(int n_capsules = 70)
    {
        ClearCapsules();
        rarityEnums = new List<RarityEnum>();
        chances = new List<float>();
        Vector3 pos;
        float x, y, z;
        foreach (var item in skinDatabase.GachaRaritySkins)
        {
            rarityEnums.Add(item.Rarity);
            chances.Add(item.chanceGacha);
        }

        for(int i = 0; i < n_capsules; i++)
        {
            x = Random.Range(xRange_1, xRange_2);
            y = Random.Range(yRange_1, yRange_2);
            z = Random.Range(zRange_1, zRange_2);

            pos = new Vector3(x, y, z);

            capsules.Add(spawnObject(capsulesDict[getRandomElementFromList(rarityEnums, chances)], pos, gachaMachine));
        }

        
    }

    private void ClearCapsules()
    {
        if(capsules != null) { 
        foreach(GameObject item in capsules)
        {
            KhtPool.ReturnObject(item);
        }
            capsules.Clear();
        }
    }

    public void OnExitGachaProcess()
    {
        _gachaLayer.SetActive(false);
        _controlButtonPanel.SetActive(true);
    }

    
    public void OnGachaBought(int n_bought)
    {
        int food;
        switch (n_bought)
        {
            case 1:
                food = 5;
                break;
            case 5:
                food = 20;
                break;
            default:
                throw new ArgumentException(nameof(n_bought));
        }
        if (_menuManager.spendFood(-food))
        {
            _buyPanel.SetActive(false);
            List<ShopItem> skins = new List<ShopItem>();
            int n;
            int num_skin;

            for (int i = 0; i < n_bought; i++)
            {
                RarityEnum rarity = getRandomElementFromList(rarityEnums, chances);
                n = gachaSkinDict[rarity].Count;
                num_skin = Random.Range(0, n);
                skins.Add(gachaSkinDict[rarity][num_skin]);
            }

            
            capsuleRarity = skins[0].Rarity;

            _gachaPanel.Show(skins);
            _menuBootstrap.InitializeInventory();
            GachaAnimation();
        }

    }

    private void GachaAnimation()
    {
        _controlButtonPanel.SetActive(false);
        DOTween.Sequence()
            .Append(_dial.transform.DOLocalRotate(new Vector3(0, 0, 160), 0.5f))
            .Append(_dial.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.8f).SetEase(Ease.OutElastic))
            .AppendCallback(StartGacha);
    }

    private void StartGacha()
    {
        StartCoroutine(StartGachaCoroutine());
    }

    private IEnumerator StartGachaCoroutine()
    {
        Vector3 posDoor = _door.transform.position;
        GameObject capsule = spawnObject(capsulesDict[capsuleRarity], _posCapsule, gachaMachine);
        _door.transform.DOLocalMove(new Vector3(0.573f, -1.036f, 1.001f), 0.5f)
            .SetEase(Ease.OutQuint);
        yield return new WaitForSeconds(2f);

        _gachaLayer.SetActive(true);
        _buyPanel.SetActive(true);

        KhtPool.ReturnObject(capsule);
        _door.transform.position = posDoor;

    }
    private RarityEnum getRandomElementFromList(List<RarityEnum> list, List<float> chances)
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
                return list[i];
            }
            else
            {
                randomPoint -= chances[i];
            }
        }
        return list[n - 1];
    }

    private GameObject spawnObject(GameObject prefab, Vector3 pos, Transform parent)
    {
        GameObject temp = KhtPool.GetObject(prefab);
        Transform tempTrans = temp.transform;

        tempTrans.position = pos;
        tempTrans.rotation = Quaternion.identity;
        tempTrans.SetParent(parent);
        temp.SetActive(true);
        return temp;
    }

}
