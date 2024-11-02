using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemViewFactory", menuName = "LevelProperties/Inventory/New InventoryItemViewFactory")]
public class InventoryItemViewFactory : ScriptableObject
{
    [SerializeField] GameObject _catSkinItemPrefab;
    private InventoryItemView _catSkinItemView;
    private InventoryItemView _catSkinItem
    {
        get
        {
            if(_catSkinItemView != null)
            {
                return _catSkinItemView;
            }
            else
            {
                _catSkinItemView = _catSkinItemPrefab.GetComponent<InventoryItemView>();
                return _catSkinItemView;
            }
        }
    }

    public InventoryItemView Get(ShopItem shopItem, Transform parent, InventoryContent inventoryContent)
    {
        InventoryItemVisitor visitor = new InventoryItemVisitor(_catSkinItem);
        shopItem.Accept(visitor);

        InventoryItemView instance = Instantiate(visitor.Prefab, parent);

        instance.Initialize(shopItem, inventoryContent.backInventoryItems[shopItem.Rarity]);
        return instance;
    }

    private class InventoryItemVisitor : IShopItemVisitor
    {
        private InventoryItemView _catSkinItemPrefab;
        public InventoryItemView Prefab { get; private set; }
        public InventoryItemVisitor(InventoryItemView catSkinItemPrefab)
        {
            _catSkinItemPrefab = catSkinItemPrefab;
        }

        public void Visit(CatSkin skin) => Prefab = _catSkinItemPrefab;
    }
}
