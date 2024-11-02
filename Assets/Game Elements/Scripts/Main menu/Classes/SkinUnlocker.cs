using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinUnlocker : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public SkinUnlocker(IPersistentData saveData) => _persistentData = saveData;

    public void Visit(CatSkin skin) => _persistentData.saveData.OpenCatSkin(skin.SkinType);
}
