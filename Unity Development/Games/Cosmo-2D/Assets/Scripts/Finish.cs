using UnityEngine;

public class Finish : MonoBehaviour
{
    private static readonly int FinishTouched = Animator.StringToHash("finishTouched");
    private static readonly int FinishedPlayer = Animator.StringToHash("finishedPlayer");
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
        if (other.gameObject.CompareTag("Player"))
        {
            _animator.SetTrigger(FinishTouched);
            _animator.SetTrigger(FinishedPlayer);
        }

        finished = true;
    }

    public void LevelComplete()
    {
    }
}