using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpPower = 5f;
    [SerializeField] private float movementSpeed = 5f;
    private PlayerControls _playerControls;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnJump()
    {
        _rigidbody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    private void OnMove(InputValue inputValue)
    {
        _rigidbody2D.velocity = inputValue.Get<Vector2>() * movementSpeed;
    }
}