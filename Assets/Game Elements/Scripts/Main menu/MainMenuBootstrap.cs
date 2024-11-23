using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class MainMenuBootstrap : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    [SerializeField] private MainMenuManager _menuManager;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private LoginAndSignUpManager _loginSignUpManager;
    [SerializeField] private Gacha _gacha;

    IDataProvider _dataProvider;
    DataServerProvider _serverProvider;
    IPersistentData _persistentData;

    private void Awake()
    {
        Vector3 vector1 = new Vector3(0.00999999978f, -0.0590000004f, -0.477999985f) - new Vector3(0.0500030518f, 0.0490000248f, -0.777000427f);
       
        Debug.Log(new Vector3((float)Math.Round(vector1.x, 4), (float)Math.Round(vector1.y, 4), (float)Math.Round(vector1.z, 4)));

        InitializeData((string callback) => { });
        InitializeAuthorizationMenu();
        InitializeInventory();
        InitializeMenuManager();
        InitializeShop();
        InitializeGacha();

    }

    public void InitializeAuthorizationMenu()
    {
        _serverProvider = new DataServerProvider(_persistentData, this);
        _loginSignUpManager.Initialize(_serverProvider, _dataProvider, _persistentData, this);
    }
    public void InitializeData(Action<string> callback)
    {
        _persistentData = new PersistentData();
        _dataProvider = new DataLocalProvider(_persistentData);

        LoadDataOrInit(callback);
    }

    public void InitializeGacha()
    {
        BoughtSkinChecker boughtSkinChecker = new BoughtSkinChecker(_persistentData);
        SkinUnlocker skinUnlocker = new SkinUnlocker(_persistentData);
        _gacha.Initialize(boughtSkinChecker, skinUnlocker, _menuManager, this);
    }
    public void InitializeInventory()
    {
        SelectedSkinChecker selectedSkinChecker = new SelectedSkinChecker(_persistentData);
        SkinSelector skinSelector = new SkinSelector(_persistentData);
        _inventory.Initialize(_persistentData.saveData.OpenedCatSkinTypes, skinSelector, selectedSkinChecker, _dataProvider);

    }
    public void InitializeShop()
    {
        BoughtSkinChecker boughtSkinChecker = new BoughtSkinChecker(_persistentData);
        SkinUnlocker skinUnlocker = new SkinUnlocker(_persistentData);
        _shop.Initialize(boughtSkinChecker, skinUnlocker);

    }

    public void InitializeMenuManager()
    {
        _menuManager.Initialize(_persistentData, _dataProvider);
    }

    private void LoadDataOrInit(Action<string> callback)
    {
        _dataProvider.TryLoad(callback);
    }

    private void DebugData()
    {
        foreach(var item in _persistentData.saveData.OpenedCatSkinTypes)
        {
            Debug.Log(item);
        }
    }
}
