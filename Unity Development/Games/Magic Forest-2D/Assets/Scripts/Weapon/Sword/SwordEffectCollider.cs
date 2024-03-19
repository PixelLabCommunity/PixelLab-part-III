using UnityEngine;

public class SwordEffectCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<EnemyAI>()) Debug.LogWarning("AAAAAAAAA");
    }
}