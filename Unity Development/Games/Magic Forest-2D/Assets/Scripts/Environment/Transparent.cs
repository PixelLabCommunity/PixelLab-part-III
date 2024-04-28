using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentDetection : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] private float transparencyAmount = 0.8f;

    [SerializeField] private float fadeTime = .4f;

    private SpriteRenderer _spriteRenderer;
    private Tilemap _tilemap;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!gameObject.activeInHierarchy)
            {
                Debug.LogWarning("Cannot start coroutine because the GameObject is inactive.");
                return;
            }

            if (_spriteRenderer)
                StartCoroutine(FadeRoutine(_spriteRenderer, fadeTime, _spriteRenderer.color.a, transparencyAmount));
            else if (_tilemap) StartCoroutine(FadeRoutine(_tilemap, fadeTime, _tilemap.color.a, transparencyAmount));
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_spriteRenderer || _tilemap)
            {
                var topObject = GameObject.Find("Top");
                if (topObject == null)
                {
                    Debug.LogWarning("Top GameObject not found.");
                    return;
                }

                if (!topObject.activeInHierarchy)
                {
                    Debug.Log("Top GameObject is inactive.");
                    return;
                }

                if (_spriteRenderer)
                    StartCoroutine(FadeRoutine(_spriteRenderer, fadeTime, _spriteRenderer.color.a, 1f));

                if (_tilemap) StartCoroutine(FadeRoutine(_tilemap, fadeTime, _tilemap.color.a, 1f));
            }
            else
            {
                Debug.LogWarning("Neither SpriteRenderer nor Tilemap component found.");
            }
        }
    }


    private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue,
        float targetTransparency)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            var newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b,
                newAlpha);
            yield return null;
        }
    }

    private IEnumerator FadeRoutine(Tilemap tilemap, float fadeTime, float startValue, float targetTransparency)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            var newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            yield return null;
        }
    }
}