using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private float attackCooldown = 0.25f;

    private readonly int _fireHash = Animator.StringToHash("Fire");

    private ActiveWeapon _activeWeapon;
    private Animator _bowAnimator;
    private float _lastAttackTime;
    private PlayerController _playerController;
    private PlayerControls _playerControls;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _playerControls = new PlayerControls();

        _activeWeapon = FindFirstObjectByType<ActiveWeapon>();
        _playerController = FindFirstObjectByType<PlayerController>();
        _bowAnimator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _playerControls.Combat.Attack.started += _ => Attack();
        _lastAttackTime = -attackCooldown;
        DebugLog();
    }

    private void Update()
    {
        FlipWeapon();
    }

    private void FixedUpdate()
    {
        if (_playerController.FacingLeft)
            _spriteRenderer.flipX = true;
        else
            _spriteRenderer.flipX = false;
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    public void Attack()
    {
        if (!(Time.time - _lastAttackTime >= attackCooldown)) return;
        Debug.LogWarning("Bow Attack!");

        if (_bowAnimator == null)
        {
            Debug.LogError("Animator component is null. Ensure it is not destroyed.");
            return;
        }

        _bowAnimator.SetTrigger(_fireHash);
        SpawnArrow();
        _lastAttackTime = Time.time;
    }

    private void SpawnArrow()
    {
        var mousePosition = Input.mousePosition;
        if (Camera.main == null) return;
        var worldMousePosition =
            Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, transform.position.z));
        var direction = worldMousePosition - transform.position;

        var spawnPosition = arrowSpawnPoint.position;
        var spawnRotation = arrowSpawnPoint.rotation;

        if (_playerController.FacingLeft) spawnRotation = Quaternion.Euler(0f, 180f, 0f) * arrowSpawnPoint.rotation;

        if (direction.y < 0)
        {
            spawnPosition = arrowSpawnPoint.position + new Vector3(0f, 0.5f, 0f);
            spawnRotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        }
        else if (direction is { y: > 0, x: < 0 } && _playerController.FacingLeft)
        {
            spawnPosition = arrowSpawnPoint.position + new Vector3(0f, -0.5f, 0f);
            spawnRotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        }

        Instantiate(arrowPrefab, spawnPosition, spawnRotation);
    }

    private void FlipWeapon()
    {
        if (_playerController == null || _activeWeapon == null)
            return;

        var mousePose = Input.mousePosition;
        if (Camera.main == null) return;
        var playerScreenPoint = Camera.main.WorldToScreenPoint(_playerController.transform.position);

        var activeWeaponTransform = _activeWeapon.transform;
        var localScale = activeWeaponTransform.localScale;
        localScale.x = mousePose.x < playerScreenPoint.x ? -1 : 1;
        activeWeaponTransform.localScale = localScale;

        // Update SpriteRenderer flipX based on the player's facing direction
        _spriteRenderer.flipX = _playerController.FacingLeft;

        Debug.Log("Local Scale: " + localScale);
    }

    private void DebugLog()
    {
        if (_activeWeapon == null)
            Debug.LogError(
                "ActiveWeapon reference not found! Make sure to assign it in the Unity Editor or set it through code.");

        if (_playerController == null)
            Debug.LogError(
                "PlayerController reference not found! Make sure to assign it in the Unity Editor or set it through code.");

        if (_bowAnimator == null)
            Debug.LogError("Animator component is null! Ensure it is assigned and not destroyed.");
    }
}