using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemExtendedView : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;

    [SerializeField] Image _backgroundImage;

    public ShopItem Item { get; private set; }

    public string Name => Item.Name;

    public void Initialize(ShopItem item, Sprite background)
    {
        _backgroundImage.sprite = background;
        Item = item;
        _nameText.text = Name;
    }
}
