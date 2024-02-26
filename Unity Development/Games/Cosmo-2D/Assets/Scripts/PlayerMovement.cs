using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private const float FlipTrigger = 0f;
    private const int MaxJumps = 2;
    [SerializeField] private float jumpPower = 5f;
    [SerializeField] private float movementSpeed = 5f;
    private int _jumpCount;


    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Debug.Log("Player Position: " + transform.position);
    }

    private void OnJump()
    {
        if (IsGrounded()) _jumpCount = 0;

        if (_jumpCount >= MaxJumps) return;
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0f);
        _rigidbody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        _jumpCount++;
    }

    private void OnMove(InputValue inputValue)
    {
        var moveInput = inputValue.Get<Vector2>();
        _rigidbody2D.velocity = moveInput * movementSpeed;

        _spriteRenderer.flipX = moveInput.x switch
        {
            < FlipTrigger => true,
            > FlipTrigger => false,
            _ => _spriteRenderer.flipX
        };
    }

    private bool IsGrounded()
    {
        return Mathf.Abs(_rigidbody2D.velocity.y) < 0.01f;
    }
}