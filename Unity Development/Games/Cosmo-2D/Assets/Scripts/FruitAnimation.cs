using UnityEngine;

public class FruitAnimation : MonoBehaviour
{
    private static readonly int Collected = Animator.StringToHash("collected");
    private Animator _animator;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) _animator.SetTrigger(Collected);
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}