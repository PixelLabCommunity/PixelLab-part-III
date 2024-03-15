using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private Vector2 _moveDir;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
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