using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;
    [SerializeField] private float vfxDestroyDelay = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Damage"))
        {
            var vfxInstance = Instantiate(destroyVFX, transform.position, Quaternion.identity);
            Destroy(vfxInstance, vfxDestroyDelay);
        }

        Destroy(gameObject);
    }
}