using UnityEngine;

public class DontDestroyOnLoadUI : MonoBehaviour
{
    private void Awake()
    {
        var objs = GameObject.FindGameObjectsWithTag("UI");

        if (objs.Length > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }
}