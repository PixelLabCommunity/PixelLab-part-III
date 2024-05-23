using UnityEngine;
using UnityEngine.InputSystem;

public class NewMoveObject : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5.0f;
    private Vector2 _movement;


    private void Update()
    {
        MoveObject();
    }


    // This method should be assigned to the "Move" action in the Input Action Asset
    public void OnMove(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }

    private void MoveObject()
    {
        var moveHorizontal = _movement.x;
        var moveVertical = _movement.y;
        var movementVector = new Vector3(moveHorizontal, moveVertical, 0.0f);
        transform.Translate(movementVector * (movementSpeed * Time.deltaTime));
    }
}