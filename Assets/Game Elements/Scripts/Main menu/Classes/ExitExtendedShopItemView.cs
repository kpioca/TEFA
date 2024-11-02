using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExitExtendedShopItemView : MonoBehaviour, IPointerClickHandler
{
    public event Action<ExitExtendedShopItemView> Click;

    public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);
}
