using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance { get; set; }

    protected virtual void Awake()
    {
        if (instance != null && gameObject != null)
            Destroy(gameObject);
        else
            instance = (T)this;

        if (!gameObject.transform.parent) DontDestroyOnLoad(gameObject);
    }
}