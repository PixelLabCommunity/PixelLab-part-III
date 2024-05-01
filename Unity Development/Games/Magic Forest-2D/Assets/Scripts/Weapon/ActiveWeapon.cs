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

    public void SetCurrentActiveWeapon(GameObject weaponPrefab, Vector2 playerDirection)
    {
        if (weaponPrefab == null) return;

        if (currentActiveWeapon != null)
        {
            currentActiveWeapon.gameObject.SetActive(false);
            DestroyCurrentActiveWeapon();
        }

        // Find the GameObject with the "ActiveWeapon" tag
        var activeWeaponObject = GameObject.FindGameObjectWithTag("ActiveWeapon");
        if (activeWeaponObject != null)
        {
            var rotation = playerDirection.x > 0 ? Quaternion.identity : Quaternion.Euler(0, 180, 0);
            var newWeapon = Instantiate(weaponPrefab, activeWeaponObject.transform.position, rotation);
            newWeapon.transform.SetParent(activeWeaponObject.transform);
            currentActiveWeapon = newWeapon.GetComponent<MonoBehaviour>();
        }
        else
        {
            Debug.LogError("No GameObject with the tag 'ActiveWeapon' found.");
        }
    }

    private void DestroyCurrentActiveWeapon()
    {
        if (currentActiveWeapon != null) Destroy(currentActiveWeapon.gameObject);
    }
}