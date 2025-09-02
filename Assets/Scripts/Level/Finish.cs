using System.Linq;
using UnityEngine;

public class Finish : MonoBehaviour, IInteractable
{
    [SerializeField] private Switch[] _switches;

    public bool _isFinish = false;
    public void Interact()
    {
        if (_switches.All(i => i.IsActive))
        {
            _isFinish = true;
            print("Вы победили!");
        }
    }
}
