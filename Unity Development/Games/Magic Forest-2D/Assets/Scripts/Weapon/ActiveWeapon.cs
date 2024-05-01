using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    [SerializeField] private MonoBehaviour currentActiveWeapon;

    private bool _attackButtonDown, _isAttacking;

    private PlayerControls _playerControls;

    public static ActiveWeapon instance { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        instance = this;
        _playerControls = new PlayerControls();
    }

    private void Start()
    {
        _playerControls.Combat.Attack.started += _ => StartAttacking();
        _playerControls.Combat.Attack.canceled += _ => StartAttacking();
    }

    private void Update()
    {
        Attack();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    public void ToggleIsAttacking(bool value)
    {
        _isAttacking = value;
    }

    private void StartAttacking()
    {
        _attackButtonDown = true;
    }

    private void StopAttacking()
    {
        _attackButtonDown = false;
    }

    private void Attack()
    {
        if (!_attackButtonDown && _isAttacking) return;
        _isAttacking = true;
        (currentActiveWeapon as IWeapon)?.Attack();
    }

    public void SetCurrentActiveWeapon(GameObject weaponPrefab, Vector3 playerDirection)
    {
        if (weaponPrefab == null) return;

        // Check if a weapon is already active
        if (currentActiveWeapon != null)
        {
            // If a weapon is already active, update its reference
            currentActiveWeapon.gameObject.SetActive(false); // Disable the current weapon
            Destroy(currentActiveWeapon.gameObject); // Destroy the game object
        }

        // Spawn the new weapon
        var rotation = playerDirection.x > 0 ? Quaternion.identity : Quaternion.Euler(0, 180, 0);
        var newWeapon = Instantiate(weaponPrefab, transform.position, rotation);
        newWeapon.transform.SetParent(transform);
        currentActiveWeapon = newWeapon.GetComponent<MonoBehaviour>();
    }


    private void DestroyCurrentActiveWeapon()
    {
        if (currentActiveWeapon != null) Destroy(currentActiveWeapon.gameObject);
    }
}