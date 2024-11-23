using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class GachaRarity
{
    public RarityEnum Rarity;
    [Range(0f, 1f)]
    public float chanceGacha;
}

