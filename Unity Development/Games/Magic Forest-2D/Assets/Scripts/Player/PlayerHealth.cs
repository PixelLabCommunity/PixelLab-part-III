using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private const float KnockBackPower = 15f;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float damageRecoveryTime = 1f;

    private bool _canTakeDamage = true;
    private PlayerFlash _flash;
    private PlayerKnockBack _knockback;
    private int CurrentHealth { get; set; }

    private void Awake()
    {
        _flash = GetComponent<PlayerFlash>();
        _knockback = GetComponent<PlayerKnockBack>();
    }

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Enemy") || !_canTakeDamage) return;

        TakeDamage(1);
        _knockback.GetKnockBack(other.transform, KnockBackPower);
        StartCoroutine(_flash.FlashRoutine());
    }

    private void TakeDamage(int damageAmount)
    {
        _canTakeDamage = false;
        CurrentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        _canTakeDamage = true;
    }
}