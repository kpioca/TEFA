using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDatabase", menuName = "LevelProperties/New CardDatabase")]

public class CardDatabase : ScriptableObject
{
    [Header("Card base")]
    [SerializeField] CardBaseSprite[] cardBases;
    public CardBaseSprite[] CardBases => cardBases;

    public Dictionary<TypeSpawnElement, Sprite> cardBasesDict
    {
        get
        {
            Dictionary<TypeSpawnElement, Sprite> cardBasesDict = new Dictionary<TypeSpawnElement, Sprite>();
            int n = cardBases.Length;
            for (int i = 0; i < n; i++)
                cardBasesDict.Add(cardBases[i].typeCard, cardBases[i].cardBase);
            return cardBasesDict;
        }
    }
}


[Serializable]
public class CardBaseSprite
{
    public Sprite cardBase;
    public TypeSpawnElement typeCard;

}

