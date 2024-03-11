using TMPro;
using UnityEngine;

public class LevelStart : MonoBehaviour
{
    private const float DisappearTime = 3f;
    [SerializeField] private TextMeshProUGUI startText;

    private void Awake()
    {
        Invoke(nameof(TextDisappear), DisappearTime);
    }


    private void TextDisappear()
    {
        startText.enabled = false;
    }
}