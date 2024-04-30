using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    private int _activeSlotIndexNumber; // Default value

    private PlayerControls _playerControls;

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void Start()
    {
        _playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
        ChangeActiveWeapon(); // Moved here
        _playerControls.Enable(); // Enable player controls
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
        // Ensure that _activeSlotIndexNumber is within valid range
        if (_activeSlotIndexNumber < 0 || _activeSlotIndexNumber >= transform.childCount)
        {
            Debug.LogError("Invalid active slot index: " + _activeSlotIndexNumber);
            return;
        }

        // Get the child at the specified index
        var activeSlot = transform.GetChild(_activeSlotIndexNumber)?.GetComponent<InventorySlot>();

        // Check if activeSlot is null
        if (activeSlot == null)
        {
            Debug.LogError("Active slot component not found at index: " + _activeSlotIndexNumber);
            return;
        }

        // Proceed with changing the active weapon
        var weaponPrefab = activeSlot.GetWeaponInfo()?.weaponPrefab;
        if (weaponPrefab != null)
            ActiveWeapon.instance.SetCurrentActiveWeapon(weaponPrefab);
        else
            Debug.LogError("Weapon prefab not found in the active slot: " + _activeSlotIndexNumber);
    }
}