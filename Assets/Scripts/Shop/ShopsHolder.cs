using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ShopsHolder : MonoBehaviour
{
    [SerializeField] private TMP_Text _groupNameText;
    [SerializeField] private List<SelectShopButton> _selectShopButtons;
    [SerializeField] private Shop _openedShop;

    private void OnEnable()
    {
        for (int i = 0; i < _selectShopButtons.Count; i++)
            _selectShopButtons[i].Opened += OnOpened;

        ChangePressedButton(_openedShop);
        OnOpened(_openedShop);
    }

    private void OnDisable()
    {
        for (int i = 0; i < _selectShopButtons.Count; i++)
            _selectShopButtons[i].Opened -= OnOpened;
    }

    private void OnOpened(Shop shop)
    {
        if (_openedShop != shop)
        {
            CloseOpenedShop();
            ChangePressedButton(shop);
            shop.Open();

            _openedShop = shop;
        }        

        _groupNameText.text = _openedShop.Label;
    }

    private void ChangePressedButton(Shop shop)
    {
        for (int i = 0; i < _selectShopButtons.Count; i++)
        {
            if (shop == _selectShopButtons[i].Shop)
                _selectShopButtons[i].Press();
            else
                _selectShopButtons[i].Unpress();
        }
    }

    private void CloseOpenedShop()
    {
        if (_openedShop != null)
            _openedShop.Close(); 
    }
}