using System.Linq;
using UnityEngine;

public class Finish : MonoBehaviour, IInteractable
{
    [SerializeField] private Switch[] _switches;

    public bool _isFinish = false;
    private Player _player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.GetComponent<Player>();
        }
    }
    public void Interact()
    {
        if (_switches.All(i => i.IsActive))
        {
            _isFinish = true;
            _player.Finish();
        }
    }
}
