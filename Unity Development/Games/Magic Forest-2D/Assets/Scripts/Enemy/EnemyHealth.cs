using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int spawnEnemyHealth = 3;

    private int _currentEnemyHealth;

    private void Start()
    {
        _currentEnemyHealth = spawnEnemyHealth;
    }

    public void Damage(int damage)
    {
        _currentEnemyHealth -= damage;
        Debug.LogWarning(_currentEnemyHealth);
        EnemyDeath();
    }

    private void EnemyDeath()
    {
        if (_currentEnemyHealth > 0) return;
        Destroy(gameObject);
        Debug.LogWarning("Enemy DEAD!!!");
    }
}