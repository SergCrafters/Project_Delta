using UnityEngine;

public class MobileInputReader : MonoBehaviour, IInputReader
{
    [SerializeField] private VariableJoystick _joystick;
    [SerializeField] private ToutchHandler _dashButton;
    [SerializeField] private ToutchHandler _attackButton;
    [SerializeField] private ToutchHandler _interactButton;

    private bool _dashTap;
    private bool _isInterect;
    private bool _isAttack;

    public Vector2 Dirrection => _joystick.Direction;

    private void OnEnable()
    {
        _dashButton.Down += SetDash;
        _attackButton.Down += SetAttack;
        _interactButton.Down += SetInteract;
    }

    private void OnDisable()
    {
        _dashButton.Down -= SetDash;
        _attackButton.Down -= SetAttack;
        _interactButton.Down -= SetInteract;
    }

    private void Update()
    {
        if (TimeManager.IsPaused)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _dashTap = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
            _isAttack = true;

        if (Input.GetKeyDown(KeyCode.F))
        {
            _isInterect = true;
        }
    }

    public bool GetIsDashTap() => GetBoolAsTrigger(ref _dashTap);

    public bool GetIsInteract() => GetBoolAsTrigger(ref _isInterect);

    public bool GetIsAttack() => GetBoolAsTrigger(ref _isAttack);

    public void SetDash() => _dashTap = true;

    public void SetInteract() => _isInterect = true;

    public void SetAttack() => _isAttack = true;

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
