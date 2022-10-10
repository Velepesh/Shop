using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkinChanger : MonoBehaviour
{
    [SerializeField] private Shop _shop;

    private int _currentSkinIndex;
    private List<GameObject> _skins = new List<GameObject>();

    public event UnityAction ItemPressed;

    private void OnEnable()
    {
        _shop.ItemPressed += OnItemPressed;
    }

    private void OnDisable()
    {
        _shop.ItemPressed -= OnItemPressed;
    }

    private void Start()
    {
        AddSkins(_shop.Items);
    }

    private void AddSkins(IReadOnlyList<IReadOnlyItem> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            IReadOnlyItem item = items[i];

            var skinObject = Instantiate(item.Skin.gameObject, transform);

            _skins.Add(skinObject);

            if (item.IsSelected)
                ChangeSkin(i);
            else
                DisbleSkin(skinObject);
        }
    }

    private void OnItemPressed(int skinIndex)
    {
        if (_currentSkinIndex != skinIndex)
        {
            ChangeSkin(skinIndex);

            ItemPressed?.Invoke();
        }
    }

    private void ChangeSkin(int index)
    {
        for (int i = 0; i < _skins.Count; i++)
        {
            if (i == index)
            {
                DisbleSkin(_skins[_currentSkinIndex]);
                _currentSkinIndex = i;

                EnableSkin(_skins[_currentSkinIndex]);

                return;
            }
        }
    }

    private void EnableSkin(GameObject skin)
    {
        skin.SetActive(true);
    }

    private void DisbleSkin(GameObject skin)
    {
        skin.SetActive(false);
    }
}