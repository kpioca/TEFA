using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "GachaContent", menuName = "LevelProperties/Gacha/New GachaContent")]
public class GachaContent : ScriptableObject
{
    [SerializeField] private SkinDatabase skinDatabase;
    [SerializeField] private BackInventoryItem[] _backItems;


    public IEnumerable<CatSkin> CatSkins => skinDatabase.GachaCatSkins;

    public Dictionary<RarityEnum, Sprite> backGachaItems
    {
        get
        {
            Dictionary<RarityEnum, Sprite> dictionary = new Dictionary<RarityEnum, Sprite>();
            foreach(var item in _backItems)
                dictionary[item.rarity] = item.background;
            return dictionary;
        }
    }


}
