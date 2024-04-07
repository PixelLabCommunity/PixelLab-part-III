using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleportation : MonoBehaviour
{
    [SerializeField] private string targetSceneName;
    [SerializeField] private string destinationPortalName;
    private bool isTeleporting = false;

    public float distance = 0.2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTeleporting)
        {
            isTeleporting = true;
            // Save player state or relevant information
            SavePlayerState();

            // Load target scene asynchronously
            SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Additive).completed += OnSceneLoaded;
        }
    }

    private void SavePlayerState()
    {
        // Save player state here
    }

    private void OnSceneLoaded(AsyncOperation asyncOperation)
    {
        // Find the destination portal in the loaded scene
        GameObject destinationPortal = GameObject.Find(destinationPortalName);
        if (destinationPortal != null)
        {
            // Check if there is already a player in the scene
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length > 1)
            {
                // If there's more than one player, destroy the duplicate(s)
                for (int i = 1; i < players.Length; i++)
                {
                    Destroy(players[i]);
                }
            }

            // Teleport the player to the destination portal's position
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = destinationPortal.transform.position;
            }
            else
            {
                Debug.LogWarning("Player not found in the new scene.");
            }
        }

        // Unload previous scene if necessary
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
