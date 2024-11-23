using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "SkinDatabase", menuName = "LevelProperties/New SkinDatabase")]
public class SkinDatabase : ScriptableObject
{
    [field: SerializeField] public List<CatSkin> ShopCatSkins { get; private set; }
    [field: SerializeField] public List<CatSkin> GachaCatSkins { get; private set; }
    [field: SerializeField] public List<GachaRarity> GachaRaritySkins { get; private set;}


    public Dictionary<RarityEnum, List<CatSkin>> GachaCatDictionary
    {
        get
        {
            Dictionary<RarityEnum, List<CatSkin>> dictionary = new Dictionary<RarityEnum, List<CatSkin>>();
            foreach (var item in GachaRaritySkins)
                dictionary.Add(item.Rarity, new List<CatSkin>(GachaCatSkins.FindAll((CatSkin cat) => cat.Rarity == item.Rarity)));
            return dictionary;
        }
    }

    public Dictionary<RarityEnum, float> GachaRarityDictionary {
        get
        {
            Dictionary<RarityEnum, float> dictionary = new Dictionary<RarityEnum, float>();
            foreach (var item in GachaRaritySkins)
                dictionary.Add(item.Rarity, item.chanceGacha);
            return dictionary;
        }
    }

    [field: SerializeField] public List<GachaCapsule> GachaCapsules { get; private set; }

    public Dictionary<RarityEnum, GameObject> GachaCapsulesDictionary
    {
        get
        {
            Dictionary<RarityEnum, GameObject> dictionary = new Dictionary<RarityEnum, GameObject>();
            foreach (var item in GachaCapsules)
                dictionary.Add(item.Rarity, item.capsuleObj);
            return dictionary;
        }
    }

    private void OnValidate()
    {
        var skinDuplicates = ShopCatSkins.GroupBy(item => item.SkinType)
            .Where(array => array.Count() > 1);
        var gachaSkinDuplicates = ShopCatSkins.GroupBy(item => item.SkinType)
            .Where(array => array.Count() > 1);

        if (skinDuplicates.Count() > 0)
            throw new InvalidOperationException(nameof(skinDuplicates));
        if(gachaSkinDuplicates.Count() > 0)
            throw new InvalidOperationException(nameof(gachaSkinDuplicates));
    }
}
