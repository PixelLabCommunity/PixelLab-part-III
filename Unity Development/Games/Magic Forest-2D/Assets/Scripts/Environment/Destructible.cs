using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;
    [SerializeField] private float vfxDestroyDelay = 1f;
    [SerializeField] private GameObject summonCoin;
    [SerializeField] private float coinDestroyDelay = 10f;
    [SerializeField] private int coinCount = 3;
    [SerializeField] private Vector2[] coinSpawnOffsets;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Damage"))
        {
            var vfxInstance = Instantiate(destroyVFX, transform.position, Quaternion.identity);
            Destroy(vfxInstance, vfxDestroyDelay);

            for (var i = 0; i < coinCount; i++)
            {
                Vector2 spawnPosition = transform.position;
                if (i < coinSpawnOffsets.Length) spawnPosition += coinSpawnOffsets[i];

                var coinInstance = Instantiate(summonCoin, spawnPosition, Quaternion.identity);
                Destroy(coinInstance, coinDestroyDelay);
            }

            Destroy(gameObject);
        }
    }
}