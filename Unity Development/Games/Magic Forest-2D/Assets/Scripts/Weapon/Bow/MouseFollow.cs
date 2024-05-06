using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = FindFirstObjectByType<PlayerController>();
    }

    private void Update()
    {
        FaceMouse();
    }

    private void FaceMouse()
    {
        if (_playerController == null) return;


        var mousePosition = Input.mousePosition;
        if (Camera.main != null) mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = transform.position - mousePosition;

        transform.right = -direction;

        if (_playerController.facingLeft) transform.Rotate(0f, 180f, 0f);
    }
}