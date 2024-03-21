using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    private const float WaitSeconds = 2f;
    private EnemyPathfinding _enemyPathfinding;
    private bool _enemyWandering = true;
    private SpriteRenderer _spriteRenderer;

    private State _state;

    private void Awake()
    {
        _enemyPathfinding = GetComponent<EnemyPathfinding>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

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
        while (_state == State.Wandering && _enemyWandering)
        {
            var wanderingPosition = GetWanderingPosition();
            _enemyPathfinding.MoveTo(wanderingPosition);
            yield return new WaitForSeconds(WaitSeconds);
        }
    }

    private static Vector2 GetWanderingPosition()
    {
        var randomX = Random.Range(-1f, 1f);
        var randomY = Random.Range(-1f, 1f);
        return new Vector2(randomX, randomY);
    }

    private void EnemyFlipRender()
    {
        var movementDirection = _enemyPathfinding.GetMovementDirection();
        _spriteRenderer.flipX = movementDirection.x < 0;
    }

    public void SetEnemyWandering(bool value)
    {
        _enemyWandering = value;
    }

    private enum State
    {
        Wandering
    }
}