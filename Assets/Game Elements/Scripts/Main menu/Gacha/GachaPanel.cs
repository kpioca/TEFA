using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GachaPanel : MonoBehaviour
{
    private List<GachaItemView> _gachaItems = new List<GachaItemView>();

    [SerializeField] private Transform _itemsParent;
    [SerializeField] private GachaItemViewFactory _gachaItemViewFactory;

    private BoughtSkinChecker _boughtSkinChecker;
    private SkinUnlocker _skinUnlocker;
    private MainMenuManager _menuManager;

    public void Initialize(BoughtSkinChecker boughtSkinChecker, SkinUnlocker skinUnlocker, MainMenuManager menuManager)
    {
        _boughtSkinChecker = boughtSkinChecker;
        _skinUnlocker = skinUnlocker;
        _menuManager = menuManager;
    }
    public void Show(List<ShopItem> items)
    {
        Clear();
        foreach(ShopItem item in items)
        {
            GachaItemView spawnedItem = _gachaItemViewFactory.Get(item, _itemsParent);


            //Проверка, есть ли скин
            spawnedItem.Item.Accept(_boughtSkinChecker);

            if (_boughtSkinChecker.IsBought)
            {
                spawnedItem.Receive(_menuManager);
            }
            else
            {
                spawnedItem.UnReceive(_menuManager, _skinUnlocker);
            }
            //
            
            _gachaItems.Add(spawnedItem);
        }

    }

    private void Clear()
    {
        foreach(GachaItemView item in _gachaItems)
        {
            Destroy(item.gameObject);
        }

        _gachaItems.Clear();
    }
}
