using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int spawnEnemyHealth = 3;

    private int _currentEnemyHealth;
    private EnemyKnockBack _enemyKnockBack;
    private PlayerController _playerController;

    private void Awake()
    {
        _enemyKnockBack = GetComponent<EnemyKnockBack>();
        _playerController = FindFirstObjectByType<PlayerController>();
    }

    private void Start()
    {
        _currentEnemyHealth = spawnEnemyHealth;
    }

    public void Damage(int damage)
    {
        _currentEnemyHealth -= damage;
        Debug.LogWarning(_currentEnemyHealth);
        if (_playerController != null)
            _enemyKnockBack.GetKnockBack(_playerController.transform, 15f);
        else
            Debug.LogError("PlayerController not found!");
        EnemyDeath();
    }

    private void EnemyDeath()
    {
        if (_currentEnemyHealth > 0) return;
        Destroy(gameObject);
        Debug.LogWarning("Enemy DEAD!!!");
    }
}