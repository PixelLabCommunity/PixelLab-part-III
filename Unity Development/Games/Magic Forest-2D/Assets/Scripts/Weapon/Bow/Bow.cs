using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    public void Attack()
    {
        Debug.LogWarning("Bow Attack!");
        ActiveWeapon.instance.ToggleIsAttacking(false);
    }
}