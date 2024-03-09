using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private const float DelayTimer = 5f;
    private static readonly int FinishTouched = Animator.StringToHash("finishTouched");
    private static readonly int FinishedPlayer = Animator.StringToHash("finishedPlayer");
    public bool finished;
    private Animator _animator;
    private bool _playFinished; // Added variable to track if play has finished
    public static Finish Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player") || finished || _playFinished) return;

        _animator.SetTrigger(FinishTouched);
        _animator.SetTrigger(FinishedPlayer);
        finished = true;

        if (_playFinished) return;
        _playFinished = true;
        Invoke(nameof(LevelComplete), DelayTimer);
    }

    private void LevelComplete()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}