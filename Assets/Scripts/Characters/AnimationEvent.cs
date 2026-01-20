using System;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public event Action DealingDamage;
    public event Action AttackStarted;
    public event Action AttackEnded;
    public event Action DashStart;
    public event Action Death;

    public void InvokeDealingDamageEvent() => DealingDamage?.Invoke();

    public void InvokeAttackStartedEvent() => AttackStarted?.Invoke();

    public void InvokeAttackEndedEvent() => AttackEnded?.Invoke();

    public void InvokeDashStartEvent() => DashStart?.Invoke();

    public void InvokeDeathEvent() => Death?.Invoke();

}
