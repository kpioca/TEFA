using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainMenuBootstrap : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    [SerializeField] private MainMenuManager _menuManager;
    [SerializeField] private Inventory _inventory;

    IDataProvider _dataProvider;
    IPersistentData _persistentData;

    public void Awake()
    {
        InitializeData();
        InitializeInventory();
        InitializeMenuManager();
        InitializeShop();
    }
    
    private void InitializeData()
    {
        _persistentData = new PersistentData();
        _dataProvider = new DataLocalProvider(_persistentData);
        LoadDataOrInit();
    }

    public void InitializeInventory()
    {
        SelectedSkinChecker selectedSkinChecker = new SelectedSkinChecker(_persistentData);
        SkinSelector skinSelector = new SkinSelector(_persistentData);
        _inventory.Initialize(_persistentData.saveData.OpenedCatSkinTypes, skinSelector, selectedSkinChecker, _dataProvider);

    }
    private void InitializeShop()
    {
        BoughtSkinChecker boughtSkinChecker = new BoughtSkinChecker(_persistentData);
        SkinUnlocker skinUnlocker = new SkinUnlocker(_persistentData);
        _shop.Initialize(boughtSkinChecker, skinUnlocker);

    }

    private void InitializeMenuManager()
    {
        _menuManager.Initialize(_persistentData, _dataProvider);
    }

    private void LoadDataOrInit()
    {
        if (_dataProvider.TryLoad() == false)
        {
            _persistentData.saveData = new SaveData();
            _dataProvider.Save();
        }
    }

    private void DebugData()
    {
        foreach(var item in _persistentData.saveData.OpenedCatSkinTypes)
        {
            Debug.Log(item);
        }
    }
}
