using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    public event Action<ShopItemView> ItemViewClicked;
    private List<ShopItemView> _shopItems = new List<ShopItemView>();

    [SerializeField] private Transform _itemsParent;
    [SerializeField] private ShopItemViewFactory _shopItemViewFactory;

    private BoughtSkinChecker _boughtSkinChecker;

    public void Initialize(BoughtSkinChecker boughtSkinChecker)
    {
        _boughtSkinChecker = boughtSkinChecker;
    }
    public void Show(IEnumerable<ShopItem> items)
    {
        Clear();
        foreach(ShopItem item in items)
        {
            ShopItemView spawnedItem = _shopItemViewFactory.Get(item, _itemsParent);
            //spawnedItem.UnBought();


            //Проверка, куплен ли скин
            spawnedItem.Item.Accept(_boughtSkinChecker);

            if (_boughtSkinChecker.IsBought)
            {
                spawnedItem.Bought();
                spawnedItem.Click -= OnItemViewClick;
            }
            else
            {
                spawnedItem.UnBought();
                spawnedItem.Click += OnItemViewClick;
            }
            //
            
            _shopItems.Add(spawnedItem);
        }

        Sort();
    }

    private void OnItemViewClick(ShopItemView view)
    {
        ItemViewClicked?.Invoke(view);
    }

    private void Sort()
    {
        _shopItems = _shopItems
            .OrderBy(item => item.IsBought)
            .ThenBy(item => item.Price)
            .ToList();
        
        for(int i = 0; i < _shopItems.Count; i++)
        {
            _shopItems[i].transform.SetSiblingIndex(i);
        }
    }

    private void Clear()
    {
        foreach(ShopItemView item in _shopItems)
        {
            item.Click -= OnItemViewClick;
            Destroy(item.gameObject);
        }

        _shopItems.Clear();
    }
}
