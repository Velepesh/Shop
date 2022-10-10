using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Shop : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private TMP_Text _itemNameText;
    [SerializeField] private Button _selectButton;
    [SerializeField] private List<Item> _items;
    [SerializeField] private ItemView _template;
    [SerializeField] private Transform _itemContainer;

    private Item _currentItem;
    private Item _previousItem;
    private ItemView _currentView;
    private ItemView _previousView;
    private List<ItemView> _itemViews = new List<ItemView>();

    public string Label => _label;
    public IReadOnlyList<IReadOnlyItem> Items => _items;

    public event UnityAction<int> ItemPressed;

    private void Awake()
    {
        InitItems();
    }

    private void OnEnable()
    {
        _selectButton.onClick.AddListener(OnSelectButton);

        for (int i = 0; i < _itemViews.Count; i++)
            _itemViews[i].ItemButtonClick += OnItemButtonClick;
    }

    private void OnDisable()
    {
        _selectButton.onClick.RemoveListener(OnSelectButton);

        for (int i = 0; i < _itemViews.Count; i++)
            _itemViews[i].ItemButtonClick -= OnItemButtonClick;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        UpdateItemNameText(_currentItem);
    }

    private void InitItems()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            Item currentItem = _items[i];
            AddItem(currentItem);
        }
        
        TrySetupByDefault();

        UpdateItemNameText(_currentItem);

        SetPreviousItem(_currentItem, _currentView);
    }

    private void TrySetupByDefault()
    {
        if (_currentItem == null)
        {
            SetCurrentItem(_items[0], _itemViews[0]);

            _currentItem.Buy();
            _currentItem.Selecte();

            _currentView.Render(_currentItem);
        }
    }

    private void AddItem(Item item)
    {
        ItemView view = Instantiate(_template, _itemContainer);
        _itemViews.Add(view);
        view.Render(item);

        if (item.IsSelected)
            SetCurrentItem(item, view);
    }

    private void OnSelectButton()
    {
        if (_currentItem.IsBuyed)
        {
            _previousItem.Unselecte();
            _previousView.Unselect();

            _currentItem.Selecte();
            _currentView.Select();

            SetPreviousItem(_currentItem, _currentView);
        }
    }

    private void OnItemButtonClick(Item item, ItemView view)
    {
        UpdateItemNameText(item);

        if(_currentView != view)
            _currentView.Unpressed();

        SetCurrentItem(item, view);

        ItemPressed?.Invoke(_items.IndexOf(_currentItem));
    }

    private void UpdateItemNameText(Item item)
    {
        if(item != null)
            _itemNameText.text = item.Label;
    }

    private void SetCurrentItem(Item item, ItemView view)
    {
        _currentItem = item;
        _currentView = view;
    }

    private void SetPreviousItem(Item item, ItemView view)
    {
        _previousItem = item;
        _previousView = view;
    }   
}