using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private WeaponInfo weaponInfo;

    public WeaponInfo GetWeaponInfo()
    {
        if (weaponInfo == null) Debug.LogWarning("Weapon doesn't Exist in Slot");
        return weaponInfo;
    }
}