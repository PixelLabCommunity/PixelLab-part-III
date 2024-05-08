using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private float destroyDelay = 1.5f;

    [SerializeField] private int arrowDamage = 1;

    private void Start()
    {
        Destroy(gameObject, destroyDelay);
    }

    private void Update()
    {
        MoveArrow();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        enemyHealth.Damage(arrowDamage);
        Destroy(gameObject);
    }

    private void MoveArrow()
    {
        transform.Translate(Vector3.right * (Time.deltaTime * moveSpeed));
    }
}