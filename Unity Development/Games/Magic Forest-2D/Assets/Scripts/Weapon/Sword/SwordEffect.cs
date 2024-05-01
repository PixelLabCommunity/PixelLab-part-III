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

    private PlayerController _playerController;
    private PlayerControls _playerControls;
    private GameObject _slashEffect;
    private Animator _swordAnimator;

    private void Awake()
    {
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
        if (_swordAnimator == null) return;
        SlashEffectFlip();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void Attack(InputAction.CallbackContext context)
    {
        if (_swordAnimator == null) return;
        _swordAnimator.SetTrigger(Attack1);
        SwordEffectColliderEnable();
    }

    private void SwordEffectColliderEnable()
    {
        if (swordEffectColliderObject != null)
            swordEffectColliderObject.gameObject.SetActive(true);
    }

    private void SwordEffectColliderDisable()
    {
        if (swordEffectColliderObject != null)
            swordEffectColliderObject.gameObject.SetActive(false);
    }

    private void SlashEffectFlip()
    {
        if (_slashEffect == null || _playerController == null || _playerController.transform == null)
            return;

        var playerScaleX = Mathf.Sign(_playerController.transform.localScale.x);

        _slashEffect.transform.localRotation = Quaternion.Euler(playerScaleX > 0 ? _spawnDown : _spawnUp);
    }


    public void SlashEffectSpawnDown()
    {
        if (slashEffectSpawnPoint == null) return;
        _slashEffect = Instantiate(slashEffectPrefab, slashEffectSpawnPoint.position,
            Quaternion.Euler(_spawnDown));
        _slashEffect.transform.parent = slashEffectSpawnPoint;
        if (swordEffectColliderObject != null)
            swordEffectColliderObject.transform.rotation = Quaternion.Euler(_spawnDown);
    }

    public void SlashEffectSpawnUp()
    {
        if (slashEffectSpawnPoint == null) return;
        _slashEffect = Instantiate(slashEffectPrefab, slashEffectSpawnPoint.position,
            Quaternion.Euler(_spawnUp));
        _slashEffect.transform.parent = slashEffectSpawnPoint;
        if (swordEffectColliderObject != null)
            swordEffectColliderObject.transform.rotation = Quaternion.Euler(_spawnDown);
    }
}