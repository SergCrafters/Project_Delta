using System;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public event Action DealingDamage;
    public event Action AttackStarted;
    public event Action AttackEnded;

    public void InvokeDealingDamageEvent() => DealingDamage?.Invoke();

    public void InvokeAttackStartedEvent() => AttackStarted?.Invoke();

    public void InvokeAttackEndedEvent() => AttackEnded?.Invoke();


}
