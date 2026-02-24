using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputInitializer : MonoBehaviour
{
    [SerializeField] private bool _isMobile;
    [SerializeField] Player _player;
    [SerializeField] InputReader _inputReader;
    [SerializeField] MobileInputReader _mobileInputReader;
    [SerializeField] TMP_Text _inreractInput;
    [SerializeField] Image _inreractMobileInput;

    private void Awake()
    {
        if (_isMobile)
        {
            _player.Initialize(_mobileInputReader);
            _inputReader.enabled = false;
            _inreractMobileInput.gameObject.SetActive(true);
            _inreractInput.gameObject.SetActive(false);
        }
        else
        {
            _player.Initialize(_inputReader);
            _mobileInputReader.gameObject.SetActive(false);
            _inreractMobileInput.gameObject.SetActive(false);
            _inreractInput.gameObject.SetActive(true);
        }
    }

}
