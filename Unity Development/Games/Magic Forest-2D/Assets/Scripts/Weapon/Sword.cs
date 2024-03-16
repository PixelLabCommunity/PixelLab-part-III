using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    private static readonly int Attack1 = Animator.StringToHash("Attack");
    private ActiveWeapon _activeWeapon;
    private PlayerController _playerController;
    private PlayerControls _playerControls;
    private Animator _swordAnimator;

    private void Awake()
    {
        _activeWeapon = GetComponentInParent<ActiveWeapon>();
        _playerController = GetComponentInParent<PlayerController>();
        _playerControls = new PlayerControls();
        _swordAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        _playerControls.Combat.Attack.started += Attack;
    }

    private void Update()
    {
        FlipWeapon();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void Attack(InputAction.CallbackContext context)
    {
        _swordAnimator.SetTrigger(Attack1);
    }

    private void FlipWeapon()
    {
        var mousePos = Input.mousePosition;
        if (Camera.main == null) return;
        var playerScreenPoint = Camera.main.WorldToScreenPoint(_playerController.transform.position);

        var activeWeaponTransform = _activeWeapon.transform;
        var localScale = activeWeaponTransform.localScale;
        localScale = new Vector3(
            mousePos.x < playerScreenPoint.x ? -1 : 1,
            localScale.y,
            localScale.z
        );
        activeWeaponTransform.localScale = localScale;
    }
}