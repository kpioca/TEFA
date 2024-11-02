using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelector : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public SkinSelector(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(CatSkin skin)
        => _persistentData.saveData.SelectedCatSkinType = skin.SkinType;
}
