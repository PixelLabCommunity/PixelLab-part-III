using UnityEngine;
using UnityEngine.InputSystem;

public class MoveObject : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector2 movement;

    private void Update()
    {
        PlayerMoveObject();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    private void PlayerMoveObject()
    {
        var movementVector = new Vector3(movement.x, movement.y, 0.0f);
        transform.Translate(movementVector * (speed * Time.deltaTime));
    }
}