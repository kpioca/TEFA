using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RarityEnum
{
    Common,
    Rare,
    Epic,
    Mythic,
    Legendary
}

public abstract class ShopItem: ScriptableObject, IShopItemVisitAcceptor
{
    [field: SerializeField] public GameObject PrefabShop { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField, Range(0, 200000)] public int Price { get; private set; }

    [field: SerializeField] public RarityEnum Rarity { get; set; }

    public virtual void Accept(IShopItemVisitor visitor)
    {
        
    }
}
