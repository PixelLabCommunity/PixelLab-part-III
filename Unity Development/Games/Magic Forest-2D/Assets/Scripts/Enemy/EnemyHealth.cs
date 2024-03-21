using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private const float ResetDamageAnimationTimer = 0.3f;
    private const float KnockBackPower = 15f;
    private static readonly int Death = Animator.StringToHash("death");
    private static readonly int Damage1 = Animator.StringToHash("damage");
    [SerializeField] private int spawnEnemyHealth = 3;
    private Animator _animator;

    private int _currentEnemyHealth;
    private EnemyAI _enemyAI;
    private EnemyKnockBack _enemyKnockBack;
    private PlayerController _playerController;

    private void Awake()
    {
        _enemyKnockBack = GetComponent<EnemyKnockBack>();
        _playerController = FindFirstObjectByType<PlayerController>();
        _animator = GetComponent<Animator>();
        _enemyAI = GetComponent<EnemyAI>();
    }

    private void Start()
    {
        _currentEnemyHealth = spawnEnemyHealth;
    }

    public void Damage(int damage)
    {
        _currentEnemyHealth -= damage;
        _animator.SetBool(Damage1, true);
        Debug.LogWarning(_currentEnemyHealth);
        if (_playerController != null)
            _enemyKnockBack.GetKnockBack(_playerController.transform, KnockBackPower);
        else
            Debug.LogError("PlayerController not found!");

        StartCoroutine(ResetDamageAnimation());

        if (_currentEnemyHealth > 0) return;
        _animator.SetBool(Death, true);
        _enemyAI.SetEnemyWandering(false);
    }

    private IEnumerator ResetDamageAnimation()
    {
        yield return new WaitForSeconds(ResetDamageAnimationTimer);

        _animator.SetBool(Damage1, false);
    }

    private void EnemyDeath()
    {
        Destroy(gameObject);
        Debug.LogWarning("Enemy DEAD!!!");
    }
}