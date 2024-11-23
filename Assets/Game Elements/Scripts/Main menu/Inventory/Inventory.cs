using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private SkinDatabase skinDatabase;
    [SerializeField] private InventoryPanel _inventoryPanel;
    [SerializeField] private BackInventoryItem[] _backInventoryItems;
    [SerializeField] private InventoryItemExtendedView _extendedInventoryItemView;
    [SerializeField] private SkinPlacement _skinPlacement;

    private InventoryContent _contentItems;

    private IDataProvider _dataProvider;

    private SkinSelector _skinSelector;
    private SelectedSkinChecker _selectedSkinChecker;

    private InventoryItemView _previewedInventoryItem;
    private InventoryItemView _selectedInventoryItem;

    public void Initialize(IEnumerable<CatSkinsEnum> openedSkinsEnum, SkinSelector skinSelector, SelectedSkinChecker selectedSkinChecker, IDataProvider dataProvider)
    {
        _skinSelector = skinSelector;
        _selectedSkinChecker = selectedSkinChecker;
        _dataProvider = dataProvider;

        _contentItems = new InventoryContent();
        _contentItems.Initialize(GetOpenedSkins(openedSkinsEnum), _backInventoryItems);
        _inventoryPanel.Initialize(_selectedSkinChecker, _contentItems);

        _inventoryPanel.Show(_contentItems.CatSkins.Cast<ShopItem>());
        _selectedInventoryItem = _previewedInventoryItem;
    }

    public List<CatSkin> GetOpenedSkins(IEnumerable<CatSkinsEnum> openedSkinsEnum)
    {
        List<CatSkin> skins = new List<CatSkin>();
        foreach (CatSkinsEnum cat in openedSkinsEnum)
        {
            CatSkin skin = skinDatabase.ShopCatSkins.Find(skin => skin.SkinType == cat);
            if (skin != null)
                skins.Add(skin);
            else
            {
                skin = skinDatabase.GachaCatSkins.Find(skin => skin.SkinType == cat);
                if (skin != null)
                    skins.Add(skin);
                else throw new ArgumentException(nameof(cat));
            }
        }
        return skins;
    }

    private void OnEnable()
    {
        _inventoryPanel.ItemViewClicked += OnItemViewClicked;

    }

    private void OnDisable()
    {
        _inventoryPanel.ItemViewClicked -= OnItemViewClicked;
    }

    public CatSkin GetSelectedSkin(CatSkinsEnum selectedSkinEnum)
    {
        return skinDatabase.ShopCatSkins.Find(skin => skin.SkinType == selectedSkinEnum);
    }

    private void OnItemViewClicked(InventoryItemView item)
    {
        if (_previewedInventoryItem != item)
        {
            _previewedInventoryItem = item;
            _extendedInventoryItemView.Initialize(item.Item, _contentItems.selectedBackShopItems[item.Item.Rarity]);
            _skinPlacement.InstantiateModel(item.Prefab, new Vector3(0, 0, 0));
        }
    }

    private void ItemViewShow(InventoryItemView item)
    {
        _previewedInventoryItem = item;
        _extendedInventoryItemView.Initialize(item.Item, _contentItems.selectedBackShopItems[item.Item.Rarity]);
        _skinPlacement.InstantiateModel(item.Prefab, new Vector3(0, 0, 0));
    }

    public void OnItemViewSelected()
    {
        if (_selectedInventoryItem != _previewedInventoryItem)
        {
            _selectedInventoryItem = _previewedInventoryItem;
            _selectedInventoryItem.Item.Accept(_skinSelector);
            _dataProvider.Save();
        }

    }
    public void OnInventoryCategorySelected(bool state)
    {
        if (state)
        {
            ItemViewShow(_selectedInventoryItem);
            _skinPlacement._rotator.ResetRotation();
        }
    }
}
