using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private const float FlipTrigger = 0f;
    [SerializeField] private float jumpPower = 5f;
    [SerializeField] private float movementSpeed = 5f;

    private PlayerControls _playerControls;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Debug.Log("Player Position: " + transform.position);
    }

    private void OnJump()
    {
        _rigidbody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    private void OnMove(InputValue inputValue)
    {
        var moveInput = inputValue.Get<Vector2>();
        _rigidbody2D.velocity = moveInput * movementSpeed;

        if (moveInput.x < FlipTrigger)
            _spriteRenderer.flipX = true;
        else if (moveInput.x > FlipTrigger)
            _spriteRenderer.flipX = false;
    }
}