using UnityEngine;

public class Lock : MonoBehaviour
{
    [SerializeField] private Key _key;

    public Key Key => _key;
}