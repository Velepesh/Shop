using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 51)]
public class Item : ScriptableObject, IReadOnlyItem
{ 
    [SerializeField] private string _label;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Skin _skin;
    [SerializeField] private bool _isBuyed;

    private readonly string _selectText = "Select";
    private int _isBuyedInt => PlayerPrefs.GetInt(_label, Convert.ToInt32(_isBuyed));
    private int _isSelectInt => PlayerPrefs.GetInt(_label + _selectText, 0);

    public string Label => _label;
    public Sprite Icon => _icon;
    public Skin Skin => _skin;
    public bool IsBuyed => _isBuyedInt == 1;
    public bool IsSelected => _isSelectInt == 1;

    public void Buy()
    {
        _isBuyed = true;
    }

    public void Selecte()
    {
        PlayerPrefs.SetInt(_label + _selectText, 1);
    }

    public void Unselecte()
    {
        PlayerPrefs.SetInt(_label + _selectText, 0);
    }
}
