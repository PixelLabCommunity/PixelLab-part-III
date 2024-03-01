using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private const string CollectableItem = "Fruit";
    private int _count;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(CollectableItem)) Destroy(other.gameObject);
        _count++;
        Debug.LogWarning("Collected: " + _count);
    }
}