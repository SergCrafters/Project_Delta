using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class PlayerInputInitializer : MonoBehaviour
{
    [SerializeField] private bool _isMobile;
    [SerializeField] private Player _player;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private MobileInputReader _mobileInputReader;
    [SerializeField] private TMP_Text _inreractInput;
    [SerializeField] private Image _inreractMobileInput;

    private void Awake()
    {
        if (Application.isEditor && _isMobile || YG2.envir.isMobile || YG2.envir.isTablet)
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
