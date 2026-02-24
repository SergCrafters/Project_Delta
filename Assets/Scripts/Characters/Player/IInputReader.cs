using UnityEngine;

public interface IInputReader
{
    public Vector2 Dirrection { get; }

    public bool GetIsDashTap();

    public bool GetIsInteract();

    public bool GetIsAttack();
}
