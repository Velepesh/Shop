using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SelectShopButton))]
[RequireComponent(typeof(Image))]
public class ShopButtonAnimation : MonoBehaviour
{
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _pressedSprite;
    [SerializeField] private Image _icon;
    [SerializeField] private Color _defaultIconColor;
    [SerializeField] private Color _pressedIconColor;
    private SelectShopButton _shopButton;
    private Image _image;

    private void Awake()
    {
        _shopButton = GetComponent<SelectShopButton>();
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _shopButton.Pressed += OnPressed;
        _shopButton.Unpressed += OnUnpressed;
    }

    private void OnDisable()
    {
        _shopButton.Pressed -= OnPressed;
        _shopButton.Unpressed -= OnUnpressed;
    }

    private void OnPressed()
    {
        _image.sprite = _pressedSprite;
        _icon.color = _pressedIconColor;
    }

    private void OnUnpressed()
    {
        _image.sprite = _defaultSprite;
        _icon.color = _defaultIconColor;
    }
}