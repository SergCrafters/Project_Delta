using System.Linq;
using UnityEngine;

public class Finish : MonoBehaviour, IInteractable
{
    [SerializeField] private Switch[] _switches;
    public void Interact()
    {
        if (_switches.All(i => i.IsActive))
        {
            print("Вы победили!");
        }
    }
}
