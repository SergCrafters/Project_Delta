using UnityEngine;

public class MedKit : MonoBehaviour, IItem
{
    [SerializeField] private int _value;
    [SerializeField] private Sprite _icon;

    public int Value => _value;

    public Sprite Icon => _icon;

    public void Collect()
    {
        Destroy(gameObject);
    }
}
