using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;
    [SerializeField] private float vfxDestroyDelay = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (destroyVFX != null)
            {
                var vfxInstance = Instantiate(destroyVFX, transform.position, Quaternion.identity);
                Destroy(vfxInstance, vfxDestroyDelay);
            }

            Destroy(gameObject);
        }
    }
}