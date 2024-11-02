using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopContent _contentItems;
    [SerializeField] private ShopPanel _shopPanel;

    [SerializeField] private MainMenuManager _mainMenuManager;
    [SerializeField] private ShopItemExtendedView _extendedShopItemView;

    [SerializeField] private SkinPlacement _skinPlacement;

    [SerializeField] private GameObject _downPanel;

    [Header("Bootstrap")]
    [SerializeField] private MainMenuBootstrap _mainMenuBootstrap;

    private SkinUnlocker _skinUnlocker;
    private BoughtSkinChecker _boughtSkinChecker;

    private ShopItemView _previewedShopItem;

    public void Start()
    {

    }

    private void OnEnable()
    {
        _shopPanel.ItemViewClicked += OnItemViewClicked;
        _extendedShopItemView.exitExtendedShopItemView.Click += OnItemViewExit;

    }

    private void OnDisable()
    {
        _shopPanel.ItemViewClicked -= OnItemViewClicked;
        _extendedShopItemView.exitExtendedShopItemView.Click -= OnItemViewExit;
    }

    public void Initialize(BoughtSkinChecker boughtSkinChecker, SkinUnlocker skinUnlocker)
    {
        _boughtSkinChecker = boughtSkinChecker;
        _skinUnlocker = skinUnlocker;

        _shopPanel.Initialize(boughtSkinChecker);
        _shopPanel.Show(_contentItems.CatSkins.Cast<ShopItem>());

    }


    public void BuyButton()
    {
        if (_mainMenuManager.buySkin(_previewedShopItem, _skinUnlocker))
        {
            OnItemViewExit(_extendedShopItemView.exitExtendedShopItemView);
            _shopPanel.Show(_contentItems.CatSkins.Cast<ShopItem>());
            _mainMenuBootstrap.InitializeInventory();
        }
    }

    private void OnItemViewClicked(ShopItemView item)
    {
        _previewedShopItem = item;

        _previewedShopItem.Item.Accept(_boughtSkinChecker);

        //if (!_boughtSkinChecker.IsBought)
        {
            _extendedShopItemView.Initialize(item.Item, _contentItems.extendedBackShopItems[item.Item.Rarity]);
            _extendedShopItemView.gameObject.SetActive(true);
            _downPanel.SetActive(false);
            _skinPlacement.InstantiateModel(item.Prefab, new Vector3(0,0,0));
        }
    }

    private void OnItemViewExit(ExitExtendedShopItemView item)
    {
        _extendedShopItemView.gameObject.SetActive(false);
        _downPanel.SetActive(true);
    }

}
