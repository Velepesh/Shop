using UnityEngine;

public interface IReadOnlyItem
{
    public string Label { get; }
    public Sprite Icon { get; }
    public Skin Skin { get; }
    public bool IsBuyed { get; }
    public bool IsSelected { get; }
}