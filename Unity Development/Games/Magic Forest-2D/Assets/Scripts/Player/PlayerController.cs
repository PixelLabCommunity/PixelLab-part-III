using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    [SerializeField] private float playerSpeed = 4f;
    [SerializeField] private ParticleSystem dust;
    private bool _isMoving;

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
        PlayerFlipRender();
    }

    private void FixedUpdate()
    {
        Move();
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

        _isMoving = _movement.magnitude > 0;
    }

    private void Move()
    {
        _playerRigidbody2D.MovePosition(_playerRigidbody2D.position + _movement * (playerSpeed * Time.deltaTime));

        if (_isMoving)
            CreateDust();
        else
            StopDust();
    }

    private void PlayerFlipRender()
    {
        var mousePosition = Input.mousePosition;
        if (Camera.main == null) return;
        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        _playerSpriteRenderer.flipX = mousePosition.x < playerScreenPoint.x;
    }

    private void CreateDust()
    {
        if (!dust.isPlaying)
            dust.Play();
    }

    private void StopDust()
    {
        if (dust.isPlaying)
            dust.Stop();
    }
}