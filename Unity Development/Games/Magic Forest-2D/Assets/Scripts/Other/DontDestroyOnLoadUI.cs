using UnityEngine;

public class DontDestroyOnLoadUI : MonoBehaviour
{
    private static DontDestroyOnLoadUI _instance;

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