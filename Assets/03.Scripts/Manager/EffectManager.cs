using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public static class EffectManager
{
    public static void PlayFadeIn(Graphic target, float duration, Action endCallback = null)
    {
        AttachOnComplete(target.DOFade(1f, duration), endCallback);
    }
    public static void PlayFadeOut(Graphic target, float duration, Action endCallback = null)
    {
        AttachOnComplete(target.DOFade(0f, duration), endCallback);
    }

    public static void PlayFadeIn(CanvasGroup target, float duration, Action endCallback = null)
    {
        AttachOnComplete(target.DOFade(1f, duration), endCallback);
    }
    public static void PlayFadeOut(CanvasGroup target, float duration, Action endCallback = null)
    {
        AttachOnComplete(target.DOFade(0f, duration), endCallback);
    }

    private static void AttachOnComplete(Tween tween, Action endCallback)
    {
        tween.OnComplete(() => endCallback?.Invoke());
    }

    public static void DOHeight(RectTransform rect, float newHeight, float duration, Action endCallback = null)
    {
        Vector2 newSize = new Vector2(rect.sizeDelta.x, newHeight);
        AttachOnComplete(rect.DOSizeDelta(newSize, duration), endCallback);
    }

    public static void DOWidth(RectTransform rect, float newWidth, float duration, Action endCallback = null)
    {
        Vector2 newSize = new Vector2(newWidth, rect.sizeDelta.y);
        AttachOnComplete(rect.DOSizeDelta(newSize, duration), endCallback);
    }

    public static void DOHeightLoop(RectTransform rect, float newHeight, float duration)
    {
        Vector2 newSize = new Vector2(rect.sizeDelta.x, newHeight);
        rect.DOSizeDelta(newSize, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}
