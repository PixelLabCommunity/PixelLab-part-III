using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    private ActiveWeapon _activeWeapon;
    private PlayerController _playerController;


    private void Start()
    {
        _activeWeapon = FindFirstObjectByType<ActiveWeapon>();
        _playerController = FindFirstObjectByType<PlayerController>();

        if (_activeWeapon == null)
            Debug.LogError(
                "ActiveWeapon reference not found! Make sure to assign it in the Unity Editor or set it through code.");

        if (_playerController == null)
            Debug.LogError(
                "PlayerController reference not found! Make sure to assign it in the Unity Editor or set it through code.");
    }

    private void Update()
    {
        FlipWeapon();
    }

    public void Attack()
    {
        Debug.LogWarning("Staff Attack!");
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
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