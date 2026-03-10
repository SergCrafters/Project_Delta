using System;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public event Action<int> Return;

    [SerializeField] private float _dashCooldown;
    [SerializeField] private float _dashTime;

    private Vector3 _pointDash;
    private float _dashTimer;
    private bool _isDash;
    private bool _dashTap;

    private void Start()
    {
        _pointDash = transform.position;
        _dashTimer = Time.time;
    }

    private void Update()
    {
        if ((Time.time - _dashTimer > _dashCooldown) && _dashTap)
        {
            _isDash = true;
            _dashTimer = Time.time;
            _pointDash = transform.position;
        }

        if (Time.time - _dashTimer > _dashTime)
            _isDash = false;
    }

    public void DashTap(bool dashTap) => 
        _dashTap = dashTap;

    public bool GetIsDash() => 
        _isDash;

    public Vector3 GetIsPointDash() => 
        _pointDash;

    public void ReturnToSafeZone(int returnDamage)
    {
        transform.position = _pointDash;
        Return?.Invoke(returnDamage);
    }
}
