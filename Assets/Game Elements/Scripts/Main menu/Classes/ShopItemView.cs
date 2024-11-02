using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Image))]
public class ShopItemView : MonoBehaviour, IPointerClickHandler
{
    public event Action<ShopItemView> Click;

    [SerializeField] private Image _contentImage;
    [SerializeField] private Image _boughtImage;

    [SerializeField] private StringValueView _priceView;
    [SerializeField] private TMP_Text _nameText;


    private Image _backgroundImage;
    public ShopItem Item { get; private set; }
    public bool IsBought { get; private set; }

    public int Price => Item.Price;
    public string Name => Item.Name;
    public GameObject Prefab => Item.PrefabShop;

    public void Initialize(ShopItem item, Sprite background)
    {
        _backgroundImage = GetComponent<Image>();
        _backgroundImage.sprite = background;

        Item = item;
        _contentImage.sprite = item.Icon;
        _priceView.Show($" {Price.ToString()}<sprite=2>");
        _nameText.text = Name;
    }

    public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);

    public void Bought()
    {
        IsBought = true;
        _boughtImage.gameObject.SetActive(IsBought);
        _priceView.Show("Приобретено");
    }

    public void UnBought()
    {
        IsBought = false;
        _boughtImage.gameObject.SetActive(IsBought);
        _priceView.Show($" {Price.ToString()}<sprite=2>");
    }

}
