using UnityEngine;
using UnityEngine.InputSystem;

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
        _playerControls.Inventory.Keyboard.performed += OnInventoryKeyPressed;
        ChangeActiveWeapon();
        _playerControls.Enable();
    }

    private void OnDestroy()
    {
        // Unsubscribe from the input action
        _playerControls.Inventory.Keyboard.performed -= OnInventoryKeyPressed;

        // Disable PlayerControls to avoid leaks
        _playerControls.Disable();
    }

    private void OnInventoryKeyPressed(InputAction.CallbackContext ctx)
    {
        ToggleActiveSlot((int)ctx.ReadValue<float>());
    }

    private void ToggleActiveSlot(int numberValue)
    {
        ToggleActiveHighlight(numberValue - 1);
    }

    private void ToggleActiveHighlight(int indexNumber)
    {
        if (this == null || gameObject == null || transform == null)
        {
            Debug.LogWarning("Attempted to toggle active highlight on a destroyed object.");
            return;
        }

        _activeSlotIndexNumber = indexNumber;

        foreach (Transform inventorySlot in transform)
            if (inventorySlot.childCount > 0)
                inventorySlot.GetChild(0).gameObject.SetActive(false);

        if (indexNumber >= 0 && indexNumber < transform.childCount)
        {
            var slotTransform = transform.GetChild(indexNumber);
            if (slotTransform != null && slotTransform.childCount > 0)
                slotTransform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Invalid index number for active slot: " + indexNumber);
        }

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
        Debug.Log("Player Direction: " + playerDirection);

        var weaponPrefab = activeSlot.GetWeaponInfo()?.weaponPrefab;
        if (weaponPrefab != null)
        {
            Debug.Log("Changing active weapon to: " + weaponPrefab.name);
            ActiveWeapon.Instance.SetCurrentActiveWeapon(weaponPrefab);
        }
        else
        {
            Debug.LogError("Weapon prefab not found in the active slot: " + _activeSlotIndexNumber);
        }
    }
}