using UnityEngine;
using UnityEngine.InputSystem;

public class SwordEffect : MonoBehaviour
{
    private static readonly int Attack1 = Animator.StringToHash("Attack");
    [SerializeField] private GameObject slashEffectPrefab;
    [SerializeField] private Transform slashEffectSpawnPoint;
    [SerializeField] private Transform swordEffectColliderObject;
    private readonly Vector3 _spawnDown = new(0, 0, 0);
    private readonly Vector3 _spawnUp = new(180, 0, 0);

    private ActiveWeapon _activeWeapon;
    private PlayerController _playerController;
    private PlayerControls _playerControls;
    private GameObject _slashEffect;
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
        SlashEffectFlip();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void Attack(InputAction.CallbackContext context)
    {
        _swordAnimator.SetTrigger(Attack1);
        SwordEffectColliderEnable();
    }

    private void SwordEffectColliderEnable()
    {
        swordEffectColliderObject.gameObject.SetActive(true);
    }

    private void SwordEffectColliderDisable()
    {
        swordEffectColliderObject.gameObject.SetActive(false);
    }


    private void SlashEffectFlip()
    {
        if (_slashEffect == null) return;
        var playerScaleX = _playerController.transform.localScale.x;
        var effectScale = _slashEffect.transform.localScale;
        effectScale.x = Mathf.Sign(playerScaleX) * Mathf.Abs(effectScale.x);
        _slashEffect.transform.localScale = effectScale;
    }

    public void SlashEffectSpawnDown()
    {
        _slashEffect = Instantiate(slashEffectPrefab, slashEffectSpawnPoint.position,
            Quaternion.Euler(_spawnDown));
        _slashEffect.transform.parent = slashEffectSpawnPoint;
        swordEffectColliderObject.transform.rotation = Quaternion.Euler(_spawnDown);
    }

    public void SlashEffectSpawnUp()
    {
        _slashEffect = Instantiate(slashEffectPrefab, slashEffectSpawnPoint.position,
            Quaternion.Euler(_spawnUp));
        _slashEffect.transform.parent = slashEffectSpawnPoint;
        swordEffectColliderObject.transform.rotation = Quaternion.Euler(_spawnDown);
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