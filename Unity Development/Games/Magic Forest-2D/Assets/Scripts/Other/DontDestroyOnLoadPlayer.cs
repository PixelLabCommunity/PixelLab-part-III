using UnityEngine;

public class DontDestroyOnLoadPlayer : MonoBehaviour
{
    private static DontDestroyOnLoadPlayer _instance;

    private void Start()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        DontDestroyOnLoad(gameObject);
    }
}