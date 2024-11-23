using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Image))]
public class GachaItemView : MonoBehaviour
{

    [SerializeField] private Image _contentImage;
    [SerializeField] private Image _dublicateImage;

    [SerializeField] private StringValueView _priceDublicateView;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _newText;


    private Image _backgroundImage;
    public ShopItem Item { get; private set; }
    public bool IsReceived { get; private set; }

    public int PriceDublicate => Item.Price;
    public string Name => Item.Name;
    public GameObject Prefab => Item.PrefabShop;

    public void Initialize(ShopItem item, Sprite background)
    {
        _backgroundImage = GetComponent<Image>();
        _backgroundImage.sprite = background;

        Item = item;
        _contentImage.sprite = item.Icon;
        _priceDublicateView.Show($" {PriceDublicate.ToString()}<sprite=2>");
        _nameText.text = Name;
    }

    public void Receive(MainMenuManager mainMenuManager)
    {
        IsReceived = true;
        _dublicateImage.gameObject.SetActive(IsReceived);
        _newText.enabled = !IsReceived;
        _priceDublicateView.Show($" {PriceDublicate.ToString()}<sprite=2>");
        PayMoneyDublicate(mainMenuManager, PriceDublicate);
    }

    public void PayMoneyDublicate(MainMenuManager mainMenuManager, int price)
    {
        mainMenuManager.changeFish(price);
    }

    public void UnlockSkin(MainMenuManager mainMenuManager, SkinUnlocker skinUnlocker)
    {
        mainMenuManager.unlockGachaSkin(this, skinUnlocker);
    }

    public void UnReceive(MainMenuManager mainMenuManager, SkinUnlocker skinUnlocker)
    {
        IsReceived = false;
        _dublicateImage.gameObject.SetActive(IsReceived);
        _newText.enabled = !IsReceived;
        UnlockSkin(mainMenuManager, skinUnlocker);
    }

}
