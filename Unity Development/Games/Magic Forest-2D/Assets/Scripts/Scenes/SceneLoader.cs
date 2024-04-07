using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SceneLoader : MonoBehaviour
{
    public string targetSceneName;

    public void LoadScene()
    {
        // Deactivate or remove AudioListener in the current scene
        DeactivateAudioListener();

        // Load the target scene asynchronously
        SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Additive).completed += OnSceneLoaded;
    }

    private void DeactivateAudioListener()
    {
        // Find and deactivate AudioListener in the current scene
        AudioListener[] audioListeners = FindObjectsOfType<AudioListener>();
        foreach (AudioListener listener in audioListeners)
        {
            listener.enabled = false;
        }
    }

    private void OnSceneLoaded(AsyncOperation asyncOperation)
    {
        // Activate AudioListener in the newly loaded scene if necessary
        AudioListener[] audioListeners = SceneManager.GetActiveScene().GetRootGameObjects().SelectMany(go => go.GetComponentsInChildren<AudioListener>()).ToArray();
        foreach (AudioListener listener in audioListeners)
        {
            listener.enabled = true;
        }
    }
}