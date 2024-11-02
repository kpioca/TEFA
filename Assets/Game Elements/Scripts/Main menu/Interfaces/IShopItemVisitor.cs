using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopItemVisitor
{
    void Visit(CatSkin skin);
}
