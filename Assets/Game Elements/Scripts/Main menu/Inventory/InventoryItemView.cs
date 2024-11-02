using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemView : MonoBehaviour, IPointerClickHandler
{
    public event Action<InventoryItemView> Click;
    [SerializeField] private Image _contentImage;

    [SerializeField] private TMP_Text _nameText;


    private Image _backgroundImage;
    public ShopItem Item { get; private set; }
    public bool IsSelected { get; private set; }

    public RarityEnum Rarity => Item.Rarity;
    public string Name => Item.Name;
    public GameObject Prefab => Item.PrefabShop;

    public void Initialize(ShopItem item, Sprite background)
    {
        _backgroundImage = GetComponent<Image>();
        _backgroundImage.sprite = background;

        Item = item;
        _contentImage.sprite = item.Icon;
        _nameText.text = Name;
    }

    public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);
    public void Select()
    {
        IsSelected = true;
    }

    public void UnSelect()
    {
        IsSelected = false;
    }
}
