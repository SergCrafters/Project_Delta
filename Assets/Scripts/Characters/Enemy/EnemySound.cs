using UnityEngine;

public class EnemySound : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;

    [SerializeField] private AudioClip _stepSound;
    [SerializeField] private AudioClip _runSound;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private AudioClip _attackSound;
    [SerializeField] private AudioClip _deathSound;

    private float _nextPlayStepTime;
    private float _nextPlayRunTime;


    public void PlayStepSound()
    {
        _nextPlayStepTime = PlayTimedPitchSound(_stepSound, _nextPlayStepTime);
    }

    public void PlayRunSound()
    {
        _nextPlayRunTime = PlayTimedPitchSound(_stepSound, _nextPlayRunTime);
    }

    public void PlayHitSound() => _audioManager.PlaySound(_hitSound);

    public void PlayAttackSound() => _audioManager.PlaySound(_attackSound);

    public void PlayDeathSound() => _audioManager.PlaySound(_deathSound);

    private float PlayTimedPitchSound(AudioClip sound, float nextPlayTime)
    {
        if (nextPlayTime < Time.time)
        {
            nextPlayTime = sound.length + Time.time;
            _audioManager.PlayRandomPitchSound(sound);
        }

        return nextPlayTime;
    }
}
