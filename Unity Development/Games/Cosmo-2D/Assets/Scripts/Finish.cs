using UnityEngine;

public class Finish : MonoBehaviour
{
    private static readonly int FinishTouched = Animator.StringToHash("finishTouched");
    public bool finished;
    private Animator _animator;
    public static Finish Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
        _animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) _animator.SetTrigger(FinishTouched);
        finished = true;
    }
}