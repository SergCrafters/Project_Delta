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
        _musicSwitcher.isOn = SaveService.MusicIsOn;
        _soundSwitcher.isOn = SaveService.SoundIsOn;
        _musicVoluem.value = SaveService.MusicVolume;
        _soundVoluem.value = SaveService.SoundVolume;
    }

    private void ChangeVoluemMusic(float value)
    {
        SaveService.SetMusicVolume(value);
        _audioManager.RefreshSettings();

    }

    private void ChangeVoluemSound(float value)
    {
        SaveService.SetSoundVolume(value);
        _audioManager.RefreshSettings();

    }

    private void SwitchMutMusic(bool isOn)
    {
        SaveService.SetMusicIsOn(isOn);
        _audioManager.RefreshSettings();
    }

    private void SwitchMutSound(bool isOn)
    {
        SaveService.SetSoundIsOn(isOn);
        _audioManager.RefreshSettings();
    }

    private void Close()
    {
        gameObject.SetActive(false);
        SaveService.Save();
    }
}
