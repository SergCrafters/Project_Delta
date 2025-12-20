using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioListener _listener;

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;
    [SerializeField] private AudioSource _randomPitchSoundSource;

    [SerializeField] private float _lowPitch = 0f;
    [SerializeField] private float _topPitch = 2f;

    [SerializeField] private AudioClip _defaultMusic;

    [SerializeField] private float _sqrMaxDistanceToSource = 100f;

    private Transform _listenerTransform;

    private void Awake()
    {
        _listenerTransform = _listener.transform;

        RefreshSettings();

        PlayMusic(_defaultMusic);
        _musicSource.loop = true;

        _soundSource.playOnAwake = false;
        _musicSource.loop = false;
    }

    public float Get_sqrMaxDistanceToSource() => _sqrMaxDistanceToSource;

    public bool CanBeHeard(Vector3 sourcePosition)
    {
        return (sourcePosition - _listenerTransform.position).sqrMagnitude < _sqrMaxDistanceToSource;
    }

    public void PlayMusic(AudioClip clip)
    {
        _musicSource.Stop();
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void PlaySound(AudioClip clip)
    {
        _soundSource.PlayOneShot(clip);
    }

    public void PlayRandomPitchSound(AudioClip clip)
    {
        _randomPitchSoundSource.pitch = Random.Range(_lowPitch, _topPitch);
        _randomPitchSoundSource.PlayOneShot(clip);
    }

    public void RefreshSettings()
    {
        _musicSource.mute = PlayerPrefs.GetInt(ConstantData.SaveData.MUSIC_MUTE_KEY, ConstantData.SaveData.IS_ON_VALUE) != ConstantData.SaveData.IS_ON_VALUE;

        _soundSource.mute = PlayerPrefs.GetInt(ConstantData.SaveData.SOUND_MUTE_KEY, ConstantData.SaveData.IS_ON_VALUE) != ConstantData.SaveData.IS_ON_VALUE;
        _randomPitchSoundSource.mute = PlayerPrefs.GetInt(ConstantData.SaveData.SOUND_MUTE_KEY, ConstantData.SaveData.IS_ON_VALUE) != ConstantData.SaveData.IS_ON_VALUE;


        _musicSource.volume = PlayerPrefs.GetFloat(ConstantData.SaveData.MUSIC_KEY, ConstantData.SaveData.DEFAULT_VALUME);

        _soundSource.volume = PlayerPrefs.GetFloat(ConstantData.SaveData.SOUND_KEY, ConstantData.SaveData.DEFAULT_VALUME);
        _randomPitchSoundSource.volume = PlayerPrefs.GetFloat(ConstantData.SaveData.SOUND_KEY, (ConstantData.SaveData.DEFAULT_VALUME * ConstantData.SaveData.FOOTSTEP_VOLUME_SCALE));
        _randomPitchSoundSource.volume *= ConstantData.SaveData.FOOTSTEP_VOLUME_SCALE;
    }

}
