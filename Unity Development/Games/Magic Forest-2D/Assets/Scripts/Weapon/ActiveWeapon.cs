using System.Collections;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    [SerializeField] private MonoBehaviour currentActiveWeapon;
    private bool _attackButtonDown, _isAttacking;
    private PlayerController _playerController;
    private PlayerControls _playerControls;
    private SpriteRenderer _spriteRenderer;
    private float _timeBetweenAttacks;

    public static ActiveWeapon Instance { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
        _playerControls = new PlayerControls();
    }

    private void Start()
    {
        _playerControls.Combat.Attack.started += _ => StartAttacking();
        _playerControls.Combat.Attack.canceled += _ => StopAttacking();
        _playerController = FindFirstObjectByType<PlayerController>();
        AttackCooldown();
    }

    private void Update()
    {
        Attack();
        FlipWeapon();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void AttackCooldown()
    {
        _isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());
    }

    private IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(_timeBetweenAttacks);
        _isAttacking = false;
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
        if (!_attackButtonDown || _isAttacking) return;
        AttackCooldown();
        (currentActiveWeapon as IWeapon)?.Attack();
    }

    public void SetCurrentActiveWeapon(GameObject weaponPrefab)
    {
        if (weaponPrefab == null) return;

        if (currentActiveWeapon != null)
        {
            // Check if the new weapon prefab has the same tag as the current active weapon
            /*if (currentActiveWeapon.CompareTag(weaponPrefab.tag))
            {
                Debug.Log("Same weapon type already active.");
                return;
            }*/

            currentActiveWeapon.gameObject.SetActive(false);
            DestroyCurrentActiveWeapon();
        }

        if (_playerController == null)
        {
            Debug.LogError("PlayerController not found.");
            return;
        }

        var isFacingLeft = _playerController.FacingLeft;
        var activeWeaponObject = GameObject.FindGameObjectWithTag("ActiveWeapon");

        if (activeWeaponObject != null)
        {
            var rotation = isFacingLeft ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
            var newWeapon = Instantiate(weaponPrefab, activeWeaponObject.transform.position, rotation);
            newWeapon.transform.SetParent(activeWeaponObject.transform);
            currentActiveWeapon = newWeapon.GetComponent<MonoBehaviour>();
            _spriteRenderer = newWeapon.GetComponent<SpriteRenderer>();
            _timeBetweenAttacks = ((IWeapon)currentActiveWeapon).GetWeaponInfo().weaponCoolDown;
        }
        else
        {
            Debug.LogError("No GameObject with the tag 'ActiveWeapon' found.");
        }
    }

    private void FlipWeapon()
    {
        if (_playerController == null || _spriteRenderer == null) return;

        var isFacingLeft = _playerController.FacingLeft;
        _spriteRenderer.flipX = isFacingLeft;
    }

    private void DestroyCurrentActiveWeapon()
    {
        if (currentActiveWeapon != null) Destroy(currentActiveWeapon.gameObject);
    }
}