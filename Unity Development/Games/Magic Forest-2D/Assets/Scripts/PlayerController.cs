using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 4f;
    private Vector2 _movement;

    private PlayerControls _playerControls;
    private Rigidbody2D _playerRigidbody2D;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PlayerInput();
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
    }

    private void Move()
    {
        _playerRigidbody2D.MovePosition(_playerRigidbody2D.position + _movement * (playerSpeed * Time.deltaTime));
    }
}