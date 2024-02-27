using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private const float FlipTrigger = 0f;
    private const float PositionYZero = 0f;
    private const float GravityBase = 0.01f;
    private const int MaxJumps = 2;
    private const string PlayerMove = "MovementState";
    private const float BaseValue = 0f;
    private static readonly int State = Animator.StringToHash(PlayerMove);
    [SerializeField] private float jumpPower = 5f;
    [SerializeField] private float movementSpeed = 5f;
    private int _jumpCount;
    private MovementState _movementState;
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

        /*var isMoving =
            moveInput.magnitude > BaseValue;

        _playerAnimator.SetInteger(State, isMoving);*/
    }

    private bool IsGrounded()
    {
        return Mathf.Abs(_rigidbody2D.velocity.y) < GravityBase;
    }

    private enum MovementState
    {
        Idle,
        Run,
        Jump,
        Falls
    }
}