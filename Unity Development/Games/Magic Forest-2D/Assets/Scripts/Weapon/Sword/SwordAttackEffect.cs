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
        SlashEffectFlipUpAndDown();
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

    private void SlashEffectFlipUpAndDown()
    {
        if (_slashEffect == null || _playerController == null || _playerController.transform == null)
            return;

        // Ensure _slashEffect is not destroyed before accessing it
        if (_slashEffect != null)
        {
            var playerScaleX = Mathf.Sign(_playerController.transform.localScale.x);
            _slashEffect.transform.localRotation = Quaternion.Euler(playerScaleX > 0 ? _spawnDown : _spawnUp);
        }
    }


    public void SlashEffectSpawnDown()
    {
        if (slashEffectSpawnPoint == null) return;

        // Determine if the player is facing left
        var isFacingLeft = PlayerController.instance.facingLeft;

        // Instantiate the slash effect
        _slashEffect = Instantiate(slashEffectPrefab, slashEffectSpawnPoint.position, Quaternion.identity);
        _slashEffect.transform.parent = slashEffectSpawnPoint;

        // Flip the slash effect spawn point if the player is facing left
        if (isFacingLeft)
        {
            // Flip the scale on the x-axis
            var newScale = _slashEffect.transform.localScale;
            newScale.x *= -1;
            _slashEffect.transform.localScale = newScale;
        }

        // Set the rotation of the sword attack effect collider object
        if (swordAttackEffectColliderObject != null)
            swordAttackEffectColliderObject.transform.rotation = Quaternion.Euler(_spawnDown);
    }


    public void SlashEffectSpawnUp()
    {
        if (slashEffectSpawnPoint == null) return;

        // Determine if the player is facing left
        var isFacingLeft = PlayerController.instance.facingLeft;

        // Instantiate the slash effect
        _slashEffect = Instantiate(slashEffectPrefab, slashEffectSpawnPoint.position, Quaternion.identity);
        _slashEffect.transform.parent = slashEffectSpawnPoint;

        // Set the rotation for the slash effect
        _slashEffect.transform.localRotation = Quaternion.Euler(_spawnUp);

        // Flip the slash effect spawn point if the player is facing left
        if (isFacingLeft)
        {
            // Flip the scale on the x-axis
            var newScale = _slashEffect.transform.localScale;
            newScale.x *= -1;
            _slashEffect.transform.localScale = newScale;
        }

        // Set the rotation of the sword attack effect collider object
        if (swordAttackEffectColliderObject != null)
            swordAttackEffectColliderObject.transform.rotation = Quaternion.Euler(_spawnUp);
    }
}