using System.Collections;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool FacingLeft { get { return facingLeft; } }
    private const float DashCooldown = 0.25f;
    private const float DashTime = 0.2f;
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    private static PlayerController _instance;
    [SerializeField] private float playerSpeed = 4f;
    [SerializeField] private ParticleSystem dust;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer playerTrailRenderer;

    public VectorValue startingPosition;
    public GameObject activeWeaponPrefab;

    private bool _isDashing;
    private bool _isMoving;
    public PlayerController Instance { get; private set; }

    private Vector2 _movement;
    private Animator _playerAnimator;
    private PlayerControls _playerControls;
    private Rigidbody2D _playerRigidbody2D;
    private SpriteRenderer _playerSpriteRenderer;
    private bool facingLeft = false;

    private void Awake()
    {
        Instance = this;
        PlayerControllerEnable();
        _playerControls = new PlayerControls();
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
        PlayerControllerTracker();
        SceneManager.sceneLoaded += OnSceneLoaded;
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
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (_instance == null)
            _instance = Instantiate(this);
        else if (_instance != this) Destroy(gameObject);
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
        /*if (_playerSpriteRenderer == null || Camera.main == null) return;

        var mousePosition = Input.mousePosition;
        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        _playerSpriteRenderer.flipX = mousePosition.x < playerScreenPoint.x;
        ActiveWeapon.instance.SetCurrentActiveWeapon(activeWeaponPrefab);*/
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x) {
            _playerSpriteRenderer.flipX = true;
            facingLeft = true;
        } else {
            _playerSpriteRenderer.flipX = false;
            facingLeft = false;
        }
        ActiveWeapon.instance.SetCurrentActiveWeapon(activeWeaponPrefab);
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

    private void PlayerControllerEnable()
    {
        _instance = this;
        if (_instance != null && _instance != this) Destroy(transform.gameObject);
    }

    private void PlayerControllerTracker()
    {
        if (startingPosition != null) transform.position = startingPosition.initialValue;
    }
}