using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private static readonly int Start = Animator.StringToHash("Start");
    private static readonly int End = Animator.StringToHash("End");
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private GameObject canvas;
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (canvas != null) canvas.gameObject.SetActive(true);
        playerStorage.initialValue = playerPosition;
        if (transitionAnimator != null)
            transitionAnimator.enabled = true;

        StartCoroutine(LoadLevel());
    }

    private IEnumerator LoadLevel()
    {
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger(End);
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(sceneToLoad);
            if (transitionAnimator != null) transitionAnimator.SetTrigger(Start);
        }
        else
        {
            Debug.LogWarning("Add Transition Animator into the Scene Transition!");
        }
    }
}