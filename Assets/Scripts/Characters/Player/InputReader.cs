using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class InputReader : MonoBehaviour, IInputReader
{
    public Vector2 Dirrection { get; private set; }

    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _dashCooldown;
    [SerializeField] private float _dashTime;

    private bool _isAttack;
    private bool _isInterect;
    private bool _dashTap;

    private void Update()
    {
        Dirrection = new Vector2(Input.GetAxis(ConstantData.InpudData.HORIZONTAL_AXIS), Input.GetAxis(ConstantData.InpudData.VERTICAL_AXIS));

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

    public bool GetIsAttack() => GetBoolAsTrigger(ref _isAttack);

    public bool GetIsDashTap() => GetBoolAsTrigger(ref _dashTap);

    public bool GetIsInteract() => GetBoolAsTrigger(ref _isInterect);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
