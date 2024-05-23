using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private readonly Vector3 _flipBow = new(180f, 0f, 0f);
    private readonly Vector3 _flipBow2 = new(-180f, 0f, 0f);
    private PlayerController _playerController;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _playerController = FindFirstObjectByType<PlayerController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
            Debug.LogError("SpriteRenderer component not found! Make sure to add it to the GameObject.");
    }

    private void Update()
    {
        FaceMouse();
    }

    private void FaceMouse()
    {
        if (_playerController == null || _spriteRenderer == null) return;

        var mousePosition = Input.mousePosition;
        if (Camera.main != null) mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = transform.position - mousePosition;

        transform.right = -direction;

        /*if (_playerController.FacingLeft) transform.Rotate(_flipBow);
        /*_spriteRenderer.flipX = false;#1#
        if (!_playerController.FacingLeft) transform.Rotate(_flipBow2);
        /*_spriteRenderer.flipX = false;#1#
        /*else
        {
            _spriteRenderer.flipX = true;
        }#1#*/
    }
}