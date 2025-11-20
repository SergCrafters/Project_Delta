using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;

    [SerializeField] private AudioClip _stepSound;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private AudioClip _attackSound;
    [SerializeField] private AudioClip _dashSound;
    [SerializeField] private AudioClip _deathSound;

    private float _nextPlayStepTime;


    public void PlayStepSound()
    {
        if (_nextPlayStepTime < Time.time)
        {
            _nextPlayStepTime = _stepSound.length + Time.time;
            _audioManager.PlayRandomPitchSound(_stepSound);
        }
    }

    public void PlayDashSound() => _audioManager.PlaySound(_dashSound);

    public void PlayHitSound() => _audioManager.PlaySound(_hitSound);

    public void PlayAttackSound() => _audioManager.PlaySound(_attackSound);

    public void PlayDeathSound() => _audioManager.PlaySound(_deathSound);

}
