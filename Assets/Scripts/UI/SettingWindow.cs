using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : MonoBehaviour
{

    [SerializeField] private AudioManager _audioManager;

    [SerializeField] private Button _backButton;
    [SerializeField] private Slider _musicVoluem;
    [SerializeField] private Slider _soundVoluem;
    [SerializeField] private Toggle _musicSwitcher;
    [SerializeField] private Toggle _soundSwitcher;

    private void OnEnable()
    {
        _backButton.onClick.AddListener(Close);
        _musicVoluem.onValueChanged.AddListener(ChangeVoluemMusic);
        _soundVoluem.onValueChanged.AddListener(ChangeVoluemSound);
        _musicSwitcher.onValueChanged.AddListener(SwitchMutMusic);
        _soundSwitcher.onValueChanged.AddListener(SwitchMutSound);

    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveListener(Close);
        _musicVoluem.onValueChanged.RemoveListener(ChangeVoluemMusic);
        _soundVoluem.onValueChanged.RemoveListener(ChangeVoluemSound);
        _musicSwitcher.onValueChanged.RemoveListener(SwitchMutMusic);
        _soundSwitcher.onValueChanged.RemoveListener(SwitchMutSound);
    }

    public void Open()
    {
        gameObject.SetActive(true);

        _musicSwitcher.isOn = PlayerPrefs.GetInt(ConstantData.SaveData.MUSIC_MUTE_KEY, ConstantData.SaveData.IS_ON_VALUE) == ConstantData.SaveData.IS_ON_VALUE;
        _soundSwitcher.isOn = PlayerPrefs.GetInt(ConstantData.SaveData.SOUND_MUTE_KEY, ConstantData.SaveData.IS_ON_VALUE) == ConstantData.SaveData.IS_ON_VALUE;
        _musicVoluem.value = PlayerPrefs.GetFloat(ConstantData.SaveData.MUSIC_KEY, ConstantData.SaveData.DEFAULT_VALUME);
        _soundVoluem.value = PlayerPrefs.GetFloat(ConstantData.SaveData.SOUND_KEY, ConstantData.SaveData.DEFAULT_VALUME);
    }

    private void ChangeVoluem(float value, string key)
    {
        PlayerPrefs.SetFloat(key, value);
        _audioManager.RefreshSettings();
    }


    private void ChangeVoluemMusic(float value)
    {
        ChangeVoluem(value, ConstantData.SaveData.MUSIC_KEY);
    }

    private void ChangeVoluemSound(float value)
    {
        ChangeVoluem(value, ConstantData.SaveData.SOUND_KEY);
    }

    private void SwitchMut(bool isOn, string key)
    {
        PlayerPrefs.SetInt(key, isOn ? ConstantData.SaveData.IS_ON_VALUE : ConstantData.SaveData.IS_OFF_VALUE);
        _audioManager.RefreshSettings();
    }

    private void SwitchMutMusic(bool isOn)
    {
        SwitchMut(isOn, ConstantData.SaveData.MUSIC_MUTE_KEY);
    }

    private void SwitchMutSound(bool isOn)
    {
        SwitchMut(isOn, ConstantData.SaveData.SOUND_MUTE_KEY);
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }

}
