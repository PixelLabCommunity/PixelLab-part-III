using UnityEngine;
using UnityEngine.InputSystem;

public class SwordAttackEffect : MonoBehaviour
{
    private static readonly int Attack1 = Animator.StringToHash("Attack");
    [SerializeField] private GameObject slashEffectPrefab;
    [SerializeField] private Transform slashEffectSpawnPoint;
    [SerializeField] private Transform swordAttackEffectColliderObject;
    private readonly Vector3 _spawnDown = new(0, 0, 0);
    private readonly Vector3 _spawnUp = new(-180, 0, 0);

    private PlayerController _playerController;
    private PlayerControls _playerControls;
    private GameObject _slashEffect;
    private Animator _swordAttackAnimator;

    private void Awake()
    {
        _playerController = GetComponentInParent<PlayerController>();
        _playerControls = new PlayerControls();
        _swordAttackAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        _playerControls.Combat.Attack.started += Attack;
    }

    private void Update()
    {
        /*SlashEffectFlip();*/
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void Attack(InputAction.CallbackContext context)
    {
        if (_swordAttackAnimator == null) return;
        _swordAttackAnimator.SetTrigger(Attack1);
        SwordEffectColliderEnable();
    }

    private void SwordEffectColliderEnable()
    {
        if (swordAttackEffectColliderObject != null)
            swordAttackEffectColliderObject.gameObject.SetActive(true);
    }

    private void SwordEffectColliderDisable()
    {
        if (swordAttackEffectColliderObject != null)
            swordAttackEffectColliderObject.gameObject.SetActive(false);
    }

    private void SlashEffectFlip()
    {
        if (_slashEffect == null || _playerController == null || _playerController.transform == null)
            return;

        /*var playerScaleX = Mathf.Sign(_playerController.transform.localScale.x);

        _slashEffect.transform.localRotation = Quaternion.Euler(playerScaleX > 0 ? _spawnDown : _spawnUp);*/
    }


    public void SlashEffectSpawnDown()
    {
        /*if (slashEffectSpawnPoint == null) return;
        _slashEffect = Instantiate(slashEffectPrefab, slashEffectSpawnPoint.position,
            Quaternion.identity);
        _slashEffect.transform.parent = slashEffectSpawnPoint;
        _slashEffect.transform.localRotation = Quaternion.Euler(_spawnDown);
        if (swordAttackEffectColliderObject != null)
            swordAttackEffectColliderObject.transform.rotation = Quaternion.Euler(_spawnDown);*/
    }

    public void SlashEffectSpawnUp()
    {
        /*if (slashEffectSpawnPoint == null) return;
        _slashEffect = Instantiate(slashEffectPrefab, slashEffectSpawnPoint.position,
            Quaternion.identity);
        _slashEffect.transform.parent = slashEffectSpawnPoint;
        _slashEffect.transform.localRotation = Quaternion.Euler(_spawnUp);
        if (swordAttackEffectColliderObject != null)
            swordAttackEffectColliderObject.transform.rotation = Quaternion.Euler(_spawnUp);*/
    }
}