using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "LevelProperties/Shop/New ShopItemViewFactory")]
public class ShopItemViewFactory : ScriptableObject
{
    [SerializeField] GameObject _catSkinItemPrefab;
    private ShopItemView _catSkinItemView;
    private ShopItemView _catSkinItem
    {
        get
        {
            if (_catSkinItemView != null)
            {
                return _catSkinItemView;
            }
            else
            {
                _catSkinItemView = _catSkinItemPrefab.GetComponent<ShopItemView>();
                return _catSkinItemView;
            }
        }
    }



    [SerializeField] ShopContent _shopContent;

    public ShopItemView Get(ShopItem shopItem, Transform parent)
    {
        ShopItemVisitor visitor = new ShopItemVisitor(_catSkinItem);
        shopItem.Accept(visitor);

        ShopItemView instance = Instantiate(visitor.Prefab, parent);

        instance.Initialize(shopItem, _shopContent.backShopItems[shopItem.Rarity]);
        return instance;
    }

    private class ShopItemVisitor : IShopItemVisitor
    {
        private ShopItemView _catSkinItemPrefab;
        public ShopItemView Prefab { get; private set; }
        public ShopItemVisitor(ShopItemView catSkinItemPrefab)
        {
            _catSkinItemPrefab = catSkinItemPrefab;
        }

        public void Visit(CatSkin skin) => Prefab = _catSkinItemPrefab;
    }
}
