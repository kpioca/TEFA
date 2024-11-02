using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class BackInventoryItem
{
    public RarityEnum rarity;
    public Sprite background;
    public Sprite selectedBackground;
}

public class InventoryContent
{
    private List<CatSkin> _catSkins;
    private BackInventoryItem[] _backInventoryItems;

    public void Initialize(List<CatSkin> catSkins, BackInventoryItem[] backInventoryItems)
    {
        _catSkins = new List<CatSkin>(catSkins);
        _backInventoryItems = backInventoryItems;
    }

    public IEnumerable<CatSkin> CatSkins => _catSkins;

    public Dictionary<RarityEnum, Sprite> backInventoryItems
    {
        get
        {
            Dictionary<RarityEnum, Sprite> dictionary = new Dictionary<RarityEnum, Sprite>();
            foreach (var item in _backInventoryItems)
                dictionary[item.rarity] = item.background;
            return dictionary;
        }
    }

    public Dictionary<RarityEnum, Sprite> selectedBackShopItems
    {
        get
        {
            Dictionary<RarityEnum, Sprite> dictionary = new Dictionary<RarityEnum, Sprite>();
            foreach (var item in _backInventoryItems)
                dictionary[item.rarity] = item.selectedBackground;
            return dictionary;
        }
    }
}
