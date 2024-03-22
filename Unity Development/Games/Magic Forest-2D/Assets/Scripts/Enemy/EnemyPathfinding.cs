using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private Animator _animator;
    private EnemyHealth _enemyHealth;
    private EnemyKnockBack _enemyKnockBack;
    private Vector2 _moveDir;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _enemyKnockBack = GetComponent<EnemyKnockBack>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    private void FixedUpdate()
    {
        if (_enemyKnockBack.gettingKnockBack || _enemyHealth.stateDying) return;
        _rigidbody2D.MovePosition(_rigidbody2D.position + _moveDir * (moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPosition)
    {
        _moveDir = (targetPosition - (Vector2)transform.position).normalized;
    }

    public Vector2 GetMovementDirection()
    {
        return _moveDir;
    }
}