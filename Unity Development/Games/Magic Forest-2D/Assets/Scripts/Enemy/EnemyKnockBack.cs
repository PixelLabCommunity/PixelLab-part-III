using System.Collections;
using UnityEngine;

public class EnemyKnockBack : MonoBehaviour
{
    [SerializeField] private float knockBackTime = 0.2f;

    private Rigidbody2D _rigidbody2D;
    public bool GettingKnockBack { get; private set; }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void GetKnockBack(Transform playerPosition, float knockBackPower)
    {
        GettingKnockBack = true;
        var difference = (transform.position - playerPosition.position).normalized * knockBackPower
            * _rigidbody2D.mass;
        _rigidbody2D.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        _rigidbody2D.velocity = Vector2.zero;
        GettingKnockBack = false;
    }
}