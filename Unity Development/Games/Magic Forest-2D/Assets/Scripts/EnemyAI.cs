using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    private EnemyPathfinding _enemyPathfinding;
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
        EnemyRenderFlip();
    }

    private IEnumerator WanderingRoutine()
    {
        while (_state == State.Wandering)
        {
            var wanderingPosition = GetWanderingPosition();
            _enemyPathfinding.MoveTo(wanderingPosition);
            yield return new WaitForSeconds(2f);
        }
    }

    private static Vector2 GetWanderingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void EnemyRenderFlip()
    {
        var movementDirection = _enemyPathfinding.GetMovementDirection();
        _spriteRenderer.flipX = movementDirection.x < 0;
    }

    private enum State
    {
        Wandering
    }
}