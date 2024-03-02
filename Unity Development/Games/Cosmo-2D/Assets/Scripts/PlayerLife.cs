using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private const string DamageDeal = "DamageDeal";
    private const string FallCollider = "FallCollider";
    private static readonly int Death = Animator.StringToHash("death");
    private Animator _animator;
    private PlayerInput _playerInput;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(DamageDeal) || other.gameObject.CompareTag(FallCollider)) PlayerDie();
    }

    private void PlayerDie()
    {
        _playerInput.enabled = false;
        _animator.SetTrigger(Death);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        _playerInput.enabled = true;
    }
}