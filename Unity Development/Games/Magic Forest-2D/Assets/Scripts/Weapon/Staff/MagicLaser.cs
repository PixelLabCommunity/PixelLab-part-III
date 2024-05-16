using UnityEngine;

public class MagicLaser : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private float destroyDelay = 1.5f;
    [SerializeField] private GameObject destroyVFX;
    [SerializeField] private float vfxDestroyDelay = 1f;

    [SerializeField] private int arrowDamage = 1;

    private void Start()
    {
        Destroy(gameObject, destroyDelay);
    }

    private void Update()
    {
        MoveLaser();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.Damage(arrowDamage);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("UnDestructible"))
        {
            var vfxInstance = Instantiate(destroyVFX, transform.position, Quaternion.identity);
            Destroy(vfxInstance, vfxDestroyDelay);
            Destroy(gameObject);
        }
    }

    private void MoveLaser()
    {
        transform.Translate(Vector3.right * (Time.deltaTime * moveSpeed));
    }
}