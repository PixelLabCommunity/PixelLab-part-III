using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    private int _activeSlotIndexNumber;

    private PlayerControls _playerControls;

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void Start()
    {
        _playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
        ChangeActiveWeapon();
        _playerControls.Enable();
    }

    private void ToggleActiveSlot(int numberValue)
    {
        ToggleActiveHighlight(numberValue - 1);
    }

    private void ToggleActiveHighlight(int indexNumber)
    {
        _activeSlotIndexNumber = indexNumber;

        foreach (Transform inventorySlot in transform)
            inventorySlot.GetChild(0).gameObject.SetActive(false);

        transform.GetChild(indexNumber).GetChild(0).gameObject.SetActive(true);
        ChangeActiveWeapon();
    }

    private void ChangeActiveWeapon()
    {
        if (_activeSlotIndexNumber < 0 || _activeSlotIndexNumber >= transform.childCount)
        {
            Debug.LogError("Invalid active slot index: " + _activeSlotIndexNumber);
            return;
        }

        var activeSlot = transform.GetChild(_activeSlotIndexNumber)?.GetComponent<InventorySlot>();

        if (activeSlot == null)
        {
            Debug.LogError("Active slot component not found at index: " + _activeSlotIndexNumber);
            return;
        }

        var playerDirection = transform.root.localScale.x > 0 ? Vector2.right : Vector2.left;

        var weaponPrefab = activeSlot.GetWeaponInfo()?.weaponPrefab;
        if (weaponPrefab != null)
            ActiveWeapon.instance.SetCurrentActiveWeapon(weaponPrefab, playerDirection);
        else
            Debug.LogError("Weapon prefab not found in the active slot: " + _activeSlotIndexNumber);
    }
}