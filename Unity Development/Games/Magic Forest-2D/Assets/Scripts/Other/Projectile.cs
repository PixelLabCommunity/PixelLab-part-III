using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleOnHitPrefabVFX;
    [SerializeField] private bool isEnemyProjectile;
    [SerializeField] private float projectileRange = 10f;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        var indestructible = other.gameObject.GetComponent<Indestructible>();
        var player = other.gameObject.GetComponent<PlayerHealth>();

        if (!other.isTrigger && (enemyHealth || indestructible || player))
        {
            if (player && isEnemyProjectile) player.TakeDamage(1);

            Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void UpdateProjectileRange(float projectileRangeNew)
    {
        projectileRange = projectileRangeNew;
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, _startPosition) > projectileRange) Destroy(gameObject);
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * (Time.deltaTime * moveSpeed));
    }
}