using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    // ReSharper disable once NotAccessedField.Local
    private int _activeSlotIndexNumber;

    private PlayerControls _playerControls;

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void Start()
    {
        _playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void ToggleActiveSlot(int numberValue)
    {
        ToggleActiveHighlight(numberValue - 1);
    }

    private void ToggleActiveHighlight(int indexNumber)
    {
        _activeSlotIndexNumber = indexNumber;

        foreach (Transform inventorySlot in transform) inventorySlot.GetChild(0).gameObject.SetActive(false);

        transform.GetChild(indexNumber).GetChild(0).gameObject.SetActive(true);
        ChangeActiveWeapon();
    }

    private void ChangeActiveWeapon()
    {
        var activeSlot = transform.GetChild(_activeSlotIndexNumber).GetComponent<InventorySlot>();
        if (activeSlot != null)
        {
            var weaponPrefab = activeSlot.GetWeaponInfo().weaponPrefab;
            ActiveWeapon.instance.SetCurrentActiveWeapon(weaponPrefab);
        }
    }

    
    
}