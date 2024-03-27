using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    private const float WaitSeconds = 2f;
    private EnemyPathfinding _enemyPathfinding;
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

    private enum State
    {
        Wandering
    }
}