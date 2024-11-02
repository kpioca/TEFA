using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    public event Action<InventoryItemView> ItemViewClicked;
    private List<InventoryItemView> _inventoryItems = new List<InventoryItemView>();

    [SerializeField] private Transform _itemsParent;
    [SerializeField] private InventoryItemViewFactory _inventoryItemViewFactory;

    private InventoryContent _inventoryContent;
    private SelectedSkinChecker _selectedSkinChecker;

    public void Initialize(SelectedSkinChecker selectedSkinChecker, InventoryContent inventoryContent)
    {
        _selectedSkinChecker = selectedSkinChecker;
        _inventoryContent = inventoryContent;
    }
    public void Show(IEnumerable<ShopItem> items)
    {
        Clear();
        foreach (ShopItem item in items)
        {
            InventoryItemView spawnedItem = _inventoryItemViewFactory.Get(item, _itemsParent, _inventoryContent);

            spawnedItem.Click += OnItemViewClick;
            //Проверка, выбран ли скин

            spawnedItem.Item.Accept(_selectedSkinChecker);

            if (_selectedSkinChecker.IsSelected)
            {
                ItemViewClicked?.Invoke(spawnedItem);
            }
            //
            
            _inventoryItems.Add(spawnedItem);
        }

        Sort();
    }

    private void OnItemViewClick(InventoryItemView view)
    {
        ItemViewClicked?.Invoke(view);
    }

    private void Sort()
    {
        _inventoryItems = _inventoryItems
            .OrderByDescending(item => item.Rarity)
            .ThenBy(item => item.Name)
            .ToList();

        for (int i = 0; i < _inventoryItems.Count; i++)
        {
            _inventoryItems[i].transform.SetSiblingIndex(i);
        }
    }

    private void Clear()
    {
        foreach (InventoryItemView item in _inventoryItems)
        {
            item.Click -= OnItemViewClick;
            Destroy(item.gameObject);
        }

        _inventoryItems.Clear();
    }
}
