using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private const float DashCooldown = 0.25f;
    private const float DashTime = 0.2f;
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    [SerializeField] private float playerSpeed = 4f;
    [SerializeField] private ParticleSystem dust;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer playerTrailRenderer;

    private bool _isDashing;
    private bool _isMoving;

    private Vector2 _movement;
    private Animator _playerAnimator;
    private PlayerControls _playerControls;
    private Rigidbody2D _playerRigidbody2D;
    private SpriteRenderer _playerSpriteRenderer;

    public VectorValue startingPosition;

    // Static variable to hold the reference to the player instance
    private static PlayerController _instance;

    private void Awake()
    {
        _instance = this;
        // If an instance already exists, destroy this instance
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        

        _playerControls = new PlayerControls();
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
        if (startingPosition != null)
        {
            transform.position = startingPosition.initialValue;
        }

        SceneManager.sceneLoaded += OnSceneLoaded; // Register the scene loaded event
    }

    private void Start()
    {
        _playerControls.Combat.Dash.performed += _ => StartDash();
    }

    private void Update()
    {
        if (_playerAnimator == null) return;
        PlayerInput();
        PlayerFlipRender();
    }

    private void FixedUpdate()
    {
        if (_playerRigidbody2D != null)
            Move();
    }

    private void OnEnable()
    {
        _playerControls?.Enable();
    }

    private void OnDisable()
    {
        _playerControls?.Disable();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unregister the scene loaded event
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Ensure the player exists in the new scene
        if (_instance == null)
        {
            _instance = Instantiate(this);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void PlayerInput()
    {
        _movement = _playerControls.Movement.Move.ReadValue<Vector2>();
        if (_playerAnimator != null)
        {
            _playerAnimator.SetFloat(MoveX, _movement.x);
            _playerAnimator.SetFloat(MoveY, _movement.y);
        }

        _isMoving = _movement.magnitude > 0;
    }

    private void Move()
    {
        if (_playerRigidbody2D == null) return;
        _playerRigidbody2D.MovePosition(_playerRigidbody2D.position + _movement * (playerSpeed * Time.deltaTime));

        if (_isMoving)
            CreateDust();
        else
            StopDust();
    }

    private void PlayerFlipRender()
    {
        if (_playerSpriteRenderer == null ||
            Camera.main ==
            null) return;
        var mousePosition = Input.mousePosition;
        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        _playerSpriteRenderer.flipX = mousePosition.x < playerScreenPoint.x;
    }

    private void CreateDust()
    {
        if (dust != null &&
            !dust.isPlaying)
            dust.Play();
    }

    private void StopDust()
    {
        if (dust != null &&
            dust.isPlaying)
            dust.Stop();
    }

    private void StartDash()
    {
        if (_isDashing || this == null) return;
        _isDashing = true;
        StartCoroutine(DashRoutine());
    }

    private void EnableEmitting()
    {
        if (playerTrailRenderer != null)
            playerTrailRenderer.emitting = true;
    }

    private void DisableEmitting()
    {
        if (playerTrailRenderer != null)
            playerTrailRenderer.emitting = false;
    }

    private IEnumerator DashRoutine()
    {
        if (playerTrailRenderer == null) yield break;
        var trailWasEmitting = playerTrailRenderer.emitting;
        EnableEmitting();
        var initialSpeed = playerSpeed;
        playerSpeed += dashSpeed;

        yield return new WaitForSeconds(DashTime);

        DisableEmitting();
        playerSpeed = initialSpeed;
        playerTrailRenderer.emitting = trailWasEmitting;
        yield return new WaitForSeconds(DashCooldown);

        _isDashing = false;
    }
}
