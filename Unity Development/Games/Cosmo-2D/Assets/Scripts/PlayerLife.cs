using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    private const string DamageDeal = "DamageDeal";
    private static readonly int Death = Animator.StringToHash("death");
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(DamageDeal)) PlayerDie();
    }

    private void PlayerDie()
    {
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        _animator.SetTrigger(Death);
    }
}