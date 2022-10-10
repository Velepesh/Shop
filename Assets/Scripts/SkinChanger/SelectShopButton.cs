using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Button))]
public class SelectShopButton : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    
    private Button _button;

    public Shop Shop => _shop;

    public event UnityAction Pressed;
    public event UnityAction Unpressed;
    public event UnityAction<Shop> Opened;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void Unpress()
    {
        Unpressed?.Invoke();
    }

    public void Press()
    {
        Pressed?.Invoke();
    } 

    private void OnButtonClick()
    {
        Open();
    }

    private void Open()
    {
        _shop.gameObject.SetActive(true);

        Opened?.Invoke(_shop);
    }
}