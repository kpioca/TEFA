using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GachaItemViewFactory", menuName = "LevelProperties/Gacha/New GachaItemViewFactory")]
public class GachaItemViewFactory : ScriptableObject
{
    [SerializeField] GameObject _catSkinItemPrefab;
    private GachaItemView _catSkinItemView;
    private GachaItemView _catSkinItem
    {
        get
        {
            if (_catSkinItemView != null)
            {
                return _catSkinItemView;
            }
            else
            {
                _catSkinItemView = _catSkinItemPrefab.GetComponent<GachaItemView>();
                return _catSkinItemView;
            }
        }
    }



    [SerializeField] GachaContent _gachaContent;

    public GachaItemView Get(ShopItem shopItem, Transform parent)
    {
        GachaItemVisitor visitor = new GachaItemVisitor(_catSkinItem);
        shopItem.Accept(visitor);

        GachaItemView instance = Instantiate(visitor.Prefab, parent);

        instance.Initialize(shopItem, _gachaContent.backGachaItems[shopItem.Rarity]);
        return instance;
    }

    private class GachaItemVisitor : IShopItemVisitor
    {
        private GachaItemView _catSkinItemPrefab;
        public GachaItemView Prefab { get; private set; }
        public GachaItemVisitor(GachaItemView catSkinItemPrefab)
        {
            _catSkinItemPrefab = catSkinItemPrefab;
        }

        public void Visit(CatSkin skin) => Prefab = _catSkinItemPrefab;
    }
}
