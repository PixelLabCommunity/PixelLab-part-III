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

    public void SetCurrentActiveWeapon(GameObject weaponPrefab)
    {
        if (weaponPrefab == null) return;
        DestroyCurrentActiveWeapon();
        var newWeapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
        newWeapon.transform.SetParent(transform); // Set the new weapon prefab as a child of ActiveWeapon
        currentActiveWeapon = newWeapon.GetComponent<MonoBehaviour>();
    }

    private void DestroyCurrentActiveWeapon()
    {
        if (currentActiveWeapon != null) Destroy(currentActiveWeapon.gameObject);
    }
}