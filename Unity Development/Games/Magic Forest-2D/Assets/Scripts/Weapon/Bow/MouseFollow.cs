using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private readonly Vector3 flipBow = new(0f, 180f, 0f);
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

        if (_playerController.FacingLeft) transform.Rotate(flipBow);
    }
}