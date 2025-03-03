using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CatSkinsEnum
{
    ginger, 
    permiona,
    cat,
    priscilla,
    sweetie,
    sunshine,
    stripe,
    rose,
    caramel,
    noir,
    sushi,
    garland,
    cow,
    harry,
    azure,
    rain,
    xc0000225,
    aed,
    palette,
    cheese,
    cutie,
    kelvin,
    winged,
    joker,
    glorp,
    _42
}
[CreateAssetMenu(fileName = "CatSkin", menuName = "LevelProperties/Skins/Cat/New CatSkin")]
public class CatSkin : ShopItem
{
    [field: SerializeField] public CatSkinsEnum SkinType { get; private set; }
    [field: SerializeField] public GameObject PrefabGame { get; private set; }

    public override void Accept(IShopItemVisitor visitor)
    {
        visitor.Visit(this);
    }
}
