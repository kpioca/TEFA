using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoughtSkinChecker : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public bool IsBought { get ; private set; }

    public BoughtSkinChecker(IPersistentData saveData) => _persistentData = saveData;

    public void Visit(CatSkin skin) => IsBought = _persistentData.saveData.OpenedCatSkinTypes.Contains(skin.SkinType);
}
