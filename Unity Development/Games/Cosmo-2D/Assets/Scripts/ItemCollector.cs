using TMPro;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private const string CollectableItem = "Fruit";
    [SerializeField] private TextMeshProUGUI fruitCount;
    public bool collected;

    private int _count;

    public static ItemCollector Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag(CollectableItem)) return;
        _count++;
        fruitCount.text = "Fruits: " + _count;
        collected = true;
    }
}