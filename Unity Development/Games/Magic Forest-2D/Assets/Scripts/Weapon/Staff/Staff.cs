using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject magicLaserPrefab;
    [SerializeField] private Transform magicLaserSpawnPoint;
    [SerializeField] private float attackCooldown = 1f;

    private readonly int _fireHash = Animator.StringToHash("Fire");

    private ActiveWeapon _activeWeapon;
    private float _lastAttackTime;
    private PlayerController _playerController;
    private PlayerControls _playerControls;
    private Animator _staffAnimator;

    private void Awake()
    {
        _playerControls = new PlayerControls();

        _activeWeapon = FindFirstObjectByType<ActiveWeapon>();
        _playerController = FindFirstObjectByType<PlayerController>();
        _staffAnimator = GetComponent<Animator>();
    }


    private void Start()
    {
        _playerControls.Combat.Attack.started += _ => Attack();
        _lastAttackTime = -attackCooldown;

        if (_activeWeapon == null)
            Debug.LogError(
                "ActiveWeapon reference not found! Make sure to assign it in the Unity Editor " +
                "or set it through code.");

        if (_playerController == null)
            Debug.LogError(
                "PlayerController reference not found! Make sure to assign it in the Unity Editor " +
                "or set it through code.");
    }

    private void Update()
    {
        FlipWeapon();
    }

    public void Attack()
    {
        Debug.LogWarning("Staff Attack!");
        if (!(Time.time - _lastAttackTime >= attackCooldown)) return;
        _staffAnimator.SetTrigger(_fireHash);
        SpawnLaser();
        _lastAttackTime = Time.time;
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    private void SpawnLaser()
    {
        var mousePosition = Input.mousePosition;
        if (Camera.main == null) return;

        var worldMousePosition =
            Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
        var direction = (worldMousePosition - magicLaserSpawnPoint.position).normalized;

        var spawnPosition = magicLaserSpawnPoint.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (_playerController.facingLeft)
            angle -= 360;

        var spawnRotation = Quaternion.Euler(0f, 0f, angle);

        Instantiate(magicLaserPrefab, spawnPosition, spawnRotation);
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
        localScale = new Vector3(
            mousePose.x < playerScreenPoint.x ? -1 : 1,
            localScale.y,
            localScale.z
        );
        activeWeaponTransform.localScale = localScale;

        // Debugging the scale applied
        Debug.Log("Local Scale: " + localScale);
    }
}