using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    private const float WaitSeconds = 2f;
    private const float AttackRadius = 5f;
    private const float FireRate = 3f;
    private const float BulletDisappear = 2f;
    private const float BulletSpeed = 10f;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    private EnemyPathfinding _enemyPathfinding;
    private Transform _player;
    private Vector2 _spawnPosition;
    private SpriteRenderer _spriteRenderer;

    private State _state;

    private void Awake()
    {
        _enemyPathfinding = GetComponent<EnemyPathfinding>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spawnPosition = transform.position;
        _state = State.Wandering;
    }

    private void Start()
    {
        StartCoroutine(WanderingRoutine());
        StartCoroutine(AttackRoutine());
    }

    private void Update()
    {
        EnemyFlipRender();
    }

    private IEnumerator WanderingRoutine()
    {
        while (_state == State.Wandering)
        {
            var wanderingPosition = GetWanderingPosition();
            _enemyPathfinding.MoveTo(wanderingPosition);
            yield return new WaitForSeconds(WaitSeconds);
        }
    }

    private Vector3 GetWanderingPosition()
    {
        var randomX = Random.Range(-1f, 1f);
        var randomY = Random.Range(-1f, 1f);
        return _spawnPosition + new Vector2(randomX, randomY);
    }

    private void EnemyFlipRender()
    {
        var movementDirection = _enemyPathfinding.GetMovementDirection();
        _spriteRenderer.flipX = movementDirection.x < 0;
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
            if (IsPlayerInRange())
            {
                FireBullet();
                yield return new WaitForSeconds(FireRate);
            }
            else
            {
                yield return null;
            }
        // ReSharper disable once IteratorNeverReturns
    }

    private bool IsPlayerInRange()
    {
        if (_player == null)
        {
            var playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null) _player = playerObject.transform;
        }

        if (_player != null)
        {
            var distanceToPlayer = Vector2.Distance(transform.position, _player.position);
            return distanceToPlayer <= AttackRadius;
        }

        return false;
    }

    private void FireBullet()
    {
        if (firePoint == null || bulletPrefab == null) return;

        var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        if (bulletRigidbody != null && _player != null)
        {
            Vector2 direction = (_player.position - firePoint.position).normalized;
            bulletRigidbody.velocity = direction * BulletSpeed;
        }

        Destroy(bullet, BulletDisappear);
    }

    private enum State
    {
        Wandering
    }
}