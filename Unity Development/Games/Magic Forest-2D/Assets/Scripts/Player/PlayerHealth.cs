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
    private ScreenShakeManager _screenShakeManager;
    private int CurrentHealth { get; set; }

    private void Awake()
    {
        _flash = GetComponent<PlayerFlash>();
        _knockback = GetComponent<PlayerKnockBack>();
        _screenShakeManager = FindFirstObjectByType<ScreenShakeManager>();
    }

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((!other.gameObject.CompareTag("Enemy") && !other.gameObject.CompareTag("Bullet")) ||
            !_canTakeDamage) return;

        TakeDamage(1);
        _knockback.GetKnockBack(other.transform, KnockBackPower);
        StartCoroutine(_flash.FlashRoutine());
    }

    public void TakeDamage(int damageAmount)
    {
        if (_screenShakeManager != null) // Check if _screenShakeManager is not null
            _screenShakeManager.ShakeScreen();
        else
            Debug.LogWarning("ScreenShakeManager is not assigned.");

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