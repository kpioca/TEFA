using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopItemVisitAcceptor
{
    abstract void Accept(IShopItemVisitor visitor);
}
