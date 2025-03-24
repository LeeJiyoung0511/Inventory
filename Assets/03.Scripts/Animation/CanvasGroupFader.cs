using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeInDuration;

    public Action OnEndFadeInEvent = delegate { };
    public Action OnEndFadeOutEvent = delegate { };

    public void FadeIn()
    {
        StartCoroutine(IFadeIn());
    }

    public void FadeOut()
    {
        StartCoroutine(IFadeOut());
    }

    private IEnumerator IFadeIn()
    {
        float time = 0.0f;
        while (time < fadeInDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, time);
            yield return null;
        }
        OnEndFadeInEvent();
    }

    private IEnumerator IFadeOut()
    {
        float time = 0.0f;
        while (time < fadeInDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, time);
            yield return null;
        }
        OnEndFadeOutEvent();
    }
}
