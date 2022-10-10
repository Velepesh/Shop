using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _pressedView;
    [SerializeField] private Image _selectedView;
    [SerializeField] private Image _lockedView;
    [SerializeField] private Button _viewButton;

    private Item _item;

    public event UnityAction<Item, ItemView> ItemButtonClick;

    private void OnEnable()
    {
        _viewButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _viewButton.onClick.RemoveListener(OnButtonClick);
    }

    public void Render(Item item)
    {
        _item = item;
        TryLockItem(item);
        TrySelectedItem(item);

        _icon.sprite = item.Icon;
    }

    private void TryLockItem(Item item)
    {
        if (item.IsBuyed)
            Unlock();
        else
            Lock();
    }

    private void TrySelectedItem(Item item)
    {
        if (item.IsSelected)
        {
            Select();
            EnableView(_pressedView);
        }
        else 
        {
            Unselect();
        }
    }

    public void Lock()
    {
        _lockedView.gameObject.SetActive(true);
    }

    public void Unlock()
    {
        _lockedView.gameObject.SetActive(false);
    }

    public void Unpressed()
    {
        DisableView(_pressedView);
    }

    public void Select()
    {
        EnableView(_selectedView);
    }

    public void Unselect()
    {
        DisableView(_selectedView);
    }

    private void OnButtonClick()
    {
         EnableView(_pressedView);
         ItemButtonClick?.Invoke(_item, this);
    }

    private void EnableView(Image view)
    {
        view.enabled = true;
    }

    private void DisableView(Image view)
    {
        view.enabled = false;
    }
}