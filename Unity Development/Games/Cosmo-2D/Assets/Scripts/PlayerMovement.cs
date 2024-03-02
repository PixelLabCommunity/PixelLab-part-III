using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private const float FlipTrigger = 0f;
    private const float PositionYZero = 0f;
    private const float GravityBase = 0.01f;
    private const int MaxJumps = 2;
    private const float BaseValue = 0f;
    private static readonly int State = Animator.StringToHash("state");
    [SerializeField] private float jumpPower = 6f;
    [SerializeField] private float movementSpeed = 6f;
    [SerializeField] private LayerMask groundLayer;
    private bool _doubleJumpAvailable = true;

    private int _jumpCount;
    private MovementState _movementState;
    private bool _moving;
    private Animator _playerAnimator;


    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        AnimationState();
    }

    private void OnJump()
    {
        if (IsGrounded())
        {
            _jumpCount = 0;
            _doubleJumpAvailable = true;
        }

        if (_jumpCount >= MaxJumps || (!_doubleJumpAvailable && _jumpCount > 0))
            return;

        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, PositionYZero);
        _rigidbody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

        _jumpCount++;

        if (_jumpCount > 1) _doubleJumpAvailable = false;
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
        _moving = moveInput.magnitude > BaseValue;
    }


    private bool IsGrounded()
    {
        var raycastHit2D = Physics2D.Raycast(transform.position, Vector2.down,
            GravityBase, groundLayer);

        if (raycastHit2D.collider == null) return raycastHit2D.collider != null;
        _jumpCount = 0;
        _doubleJumpAvailable = true;

        return raycastHit2D.collider != null;
    }


    private void AnimationState()
    {
        var state = _rigidbody2D.velocity.y switch
        {
            > 0.1f => MovementState.Jump,
            < -0.1f when IsGrounded() => MovementState.Idle,
            < -0.1f => MovementState.Fall,
            _ => _moving ? MovementState.Run : MovementState.Idle
        };

        if (_jumpCount > 1)
        {
            if (_rigidbody2D.velocity.y > 0.1f)
                state = MovementState.DoubleJump;
            else
                state = IsGrounded() ? MovementState.Idle : MovementState.Fall;
        }

        _playerAnimator.SetInteger(State, (int)state);
    }

    private enum MovementState
    {
        Idle,
        Run,
        Jump,
        DoubleJump,
        Fall
    }
}