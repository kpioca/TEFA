using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemExtendedView : MonoBehaviour
{
    [field: SerializeField] public ExitExtendedShopItemView exitExtendedShopItemView { get; private set; }
    [SerializeField] private StringValueView _priceView;
    [SerializeField] private TMP_Text _nameText;

    [SerializeField] Image _backgroundImage;

    public ShopItem Item { get; private set; }

    public int Price => Item.Price;
    public string Name => Item.Name;

    public GameObject Prefab => Item.PrefabShop;

    public void Initialize(ShopItem item, Sprite background)
    {
        _backgroundImage.sprite = background;

        Item = item;
        _priceView.Show(Price.ToString() + "<sprite=2>");
        _nameText.text = Name;

    }
}
