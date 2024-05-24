using UnityEngine;

public class DontDestroyOnLoadCamera : MonoBehaviour
{
    private static DontDestroyOnLoadCamera _instance;

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