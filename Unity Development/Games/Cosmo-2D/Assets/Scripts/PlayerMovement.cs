using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private const float FlipTrigger = 0f;
    private const float PositionYZero = 0f;
    private const float GravityBase = 0.01f;
    private const int MaxJumps = 2;
    private const float BaseValue = 0f;
    [SerializeField] private float jumpPower = 5f;
    [SerializeField] private float movementSpeed = 5f;
    private readonly float _baseValueX = 0f;
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
        Debug.Log("Player Position: " + transform.position);
        AnimationState();
    }

    private void OnJump()
    {
        if (IsGrounded()) _jumpCount = 0;

        if (_jumpCount >= MaxJumps) return;
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, PositionYZero);
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
        _moving = moveInput.magnitude > BaseValue;
    }

    private bool IsGrounded()
    {
        return Mathf.Abs(_rigidbody2D.velocity.y) < GravityBase;
    }

    private void AnimationState()
    {
        MovementState state;
        if (_moving)
            state = MovementState.Run;
        else
            state = MovementState.Idle;

        /*if (_rigidbody2D.velocity.y > 0.1f)
            state = MovementState.Jump;
        else if (_rigidbody2D.velocity.y < 0.1f) state = MovementState.Falls;*/
        _playerAnimator.SetInteger("state", (int)state);
    }

    private enum MovementState
    {
        Idle,
        Run,
        Jump,
        Falls
    }
}