using System;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private HealthBar _healthBar;

    private Health _health;

    public event Action Died;

    protected virtual void Awake()
    {
        _health = new Health(_maxHealth);
        _healthBar.Initialize(_health);
    }

    protected virtual void OnEnable()
    {
        _health.TakingDamage += OnTakingDamage;
        _health.Died += OnDied;
    }

    protected virtual void OnDisable()
    {
        _health.TakingDamage -= OnTakingDamage;
        _health.Died -= OnDied;
    }

    public void ApplyDamage(int damage)
    {
        _health.ApplyDamage(damage);
    }

    public void Heal(int value)
    {
        _health.Heal(value);
    }

    protected virtual void OnDied()
    {
        Died?.Invoke();
    }

    protected abstract void OnTakingDamage();

}
