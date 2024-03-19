using UnityEngine;

public class SwordEffectCollider : MonoBehaviour
{
    [SerializeField] private int swordDamage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        enemyHealth.Damage(swordDamage);
    }
}