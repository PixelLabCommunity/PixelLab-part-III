using TMPro;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private const string CollectableItem = "Fruit";
    [SerializeField] private TextMeshProUGUI fruitCount;

    private int _count;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag(CollectableItem)) return;
        Destroy(other.gameObject);
        _count++;
        fruitCount.text = "Fruits: " + _count; // Update text here
    }
}