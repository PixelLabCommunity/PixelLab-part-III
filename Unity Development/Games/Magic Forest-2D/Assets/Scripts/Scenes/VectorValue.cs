using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject
{
    public Vector2 initialValue;

    private void OnEnable()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            initialValue = player.transform.position;
        }
        else
        {
            Debug.LogWarning("No GameObject with tag 'Player' found.");
        }
    }
}