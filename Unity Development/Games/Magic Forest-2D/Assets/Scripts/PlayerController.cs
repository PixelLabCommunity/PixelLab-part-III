using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    [SerializeField] private float playerSpeed = 4f;
    private Vector2 _movement;
    private Animator _playerAnimator;

    private PlayerControls _playerControls;
    private Rigidbody2D _playerRigidbody2D;
    private SpriteRenderer _playerSpriteRenderer;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        Move();
        PlayerFlipRender();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void PlayerInput()
    {
        _movement = _playerControls.Movement.Move.ReadValue<Vector2>();
        _playerAnimator.SetFloat(MoveX, _movement.x);
        _playerAnimator.SetFloat(MoveY, _movement.y);
    }

    private void Move()
    {
        _playerRigidbody2D.MovePosition(_playerRigidbody2D.position + _movement * (playerSpeed * Time.deltaTime));
    }

    private void PlayerFlipRender()
    {
        var mousePosition = Input.mousePosition;
        if (Camera.main == null) return;
        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);


        _playerSpriteRenderer.flipX = mousePosition.x < playerScreenPoint.x;
    }
}