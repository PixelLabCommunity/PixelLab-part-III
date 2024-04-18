using System.Collections;
using UnityEngine;

public class Transparent : MonoBehaviour
{
    private const float ZeroTransparency = 1f;
    [Range(0, 1)] [SerializeField] private float transparencyValue = 0.8f;
    [SerializeField] private float fadeTime = 0.4f;

    private Material _material;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) StartCoroutine(FadeRoutine(transparencyValue));
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gameObject.activeSelf)
            StartCoroutine(FadeAndSetActiveRoutine(ZeroTransparency));
    }


    private IEnumerator FadeAndSetActiveRoutine(float targetAlpha)
    {
        yield return StartCoroutine(FadeRoutine(targetAlpha));
        yield return new WaitForSeconds(fadeTime);

        gameObject.SetActive(true);
    }


    private IEnumerator FadeRoutine(float targetAlpha)
    {
        var currentColor = _material.color;
        var currentAlpha = currentColor.a;
        var startTime = Time.time;

        while (Time.time < startTime + fadeTime)
        {
            var transparency = (Time.time - startTime) / fadeTime;
            var newColor = new Color(currentColor.r, currentColor.g, currentColor.b,
                Mathf.Lerp(currentAlpha, targetAlpha, transparency));
            _material.color = newColor;
            yield return null;
        }

        _material.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
    }
}