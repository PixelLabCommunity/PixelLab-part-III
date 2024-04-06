using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        var playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
            LoadNextScene();
    }

    private static void LoadNextScene()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var nextSceneIndex = currentSceneIndex == 0 ? 1 : 0;

        var playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
            Destroy(playerObject);
        SceneManager.LoadScene(nextSceneIndex);
    }
}