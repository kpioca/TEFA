using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedSkinChecker : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public bool IsSelected { get; private set; }

    public SelectedSkinChecker(IPersistentData persistentData)
        => _persistentData = persistentData;

    public void Visit(CatSkin skin)
        => IsSelected = _persistentData.saveData.SelectedCatSkinType == skin.SkinType;

}
