using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : Singleton<UIFade>
{
    [SerializeField] private Image fadeScreen;
    [SerializeField] private float fadeSpeed = 1f;

    private IEnumerator _fadeRoutine;

    public void FadeToBlack()
    {
        if (_fadeRoutine != null) StopCoroutine(_fadeRoutine);

        _fadeRoutine = FadeRoutine(1);
        StartCoroutine(_fadeRoutine);
    }

    public void FadeToClear()
    {
        if (_fadeRoutine != null) StopCoroutine(_fadeRoutine);

        _fadeRoutine = FadeRoutine(0);
        StartCoroutine(_fadeRoutine);
    }

    private IEnumerator FadeRoutine(float targetAlpha)
    {
        while (!Mathf.Approximately(fadeScreen.color.a, targetAlpha))
        {
            var alpha = Mathf.MoveTowards(fadeScreen.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, alpha);
            yield return null;
        }
    }
}