using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[Serializable]
public class BackShopItem
{
    public RarityEnum rarity;
    public Sprite background;
    public Sprite extendedBackground;
}

[CreateAssetMenu(fileName = "ShopContent", menuName = "LevelProperties/Shop/New ShopContent")]
public class ShopContent : ScriptableObject
{
    [SerializeField] private SkinDatabase skinDatabase;
    [SerializeField] private BackShopItem[] _backShopItems;


    public IEnumerable<CatSkin> CatSkins => skinDatabase.ShopCatSkins;

    public Dictionary<RarityEnum, Sprite> backShopItems
    {
        get
        {
            Dictionary<RarityEnum, Sprite> dictionary = new Dictionary<RarityEnum, Sprite>();
            foreach(var item in _backShopItems)
                dictionary[item.rarity] = item.background;
            return dictionary;
        }
    }

    public Dictionary<RarityEnum, Sprite> extendedBackShopItems
    {
        get
        {
            Dictionary<RarityEnum, Sprite> dictionary = new Dictionary<RarityEnum, Sprite>();
            foreach (var item in _backShopItems)
                dictionary[item.rarity] = item.extendedBackground;
            return dictionary;
        }
    }

}
