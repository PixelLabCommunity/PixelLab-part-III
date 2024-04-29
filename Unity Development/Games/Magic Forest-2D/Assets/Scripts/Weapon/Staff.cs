using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    public void Attack()
    {
        Debug.LogWarning("Staff Attack!");
        ActiveWeapon.instance.ToggleIsAttacking(false);
    }
}