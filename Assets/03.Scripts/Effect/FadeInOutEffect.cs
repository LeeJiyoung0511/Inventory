using System;
using UnityEngine;

[Serializable]
public class FadeInOutEffect
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float duration; //애니메이션이 진행될 시간(초)

    public void FadeIn(Action endFadeInEvent = null)
    {
        EffectManager.PlayFadeIn(canvasGroup, duration, endFadeInEvent);
    }

    public void FadeOut(Action endFadeOutEvent = null)
    {
        EffectManager.PlayFadeIn(canvasGroup, duration, endFadeOutEvent);
    }
}
