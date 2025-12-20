using UnityEngine;

public interface IItem
{
    public Sprite Icon { get; }

    public abstract void Collect();
}
