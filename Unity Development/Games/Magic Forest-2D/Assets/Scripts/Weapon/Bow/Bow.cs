using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    private ActiveWeapon _activeWeapon;
    private PlayerController _playerController;

    private void Update()
    {
        FlipWeapon();
    }

    public void Attack()
    {
        Debug.LogWarning("Bow Attack!");
        ActiveWeapon.instance.ToggleIsAttacking(false);
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
    }
}