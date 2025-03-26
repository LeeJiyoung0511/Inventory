using DG.Tweening;
using System;
using UnityEngine;

public static class EffectManager
{
    //서서히 보이게 하는 애니메이션
    public static void PlayFadeIn(CanvasGroup target, float duration, Action endCallback = null)
    {
        AttachOnComplete(target.DOFade(1f, duration), endCallback);
    }
    //서서히 사라지게 하는 애니메이션
    public static void PlayFadeOut(CanvasGroup target, float duration, Action endCallback = null)
    {
        AttachOnComplete(target.DOFade(0f, duration), endCallback);
    }
    //높이 값 조정 애니메이션
    public static void DOHeight(RectTransform rect, float newHeight, float duration, Action endCallback = null)
    {
        Vector2 newSize = new Vector2(rect.sizeDelta.x, newHeight);
        AttachOnComplete(rect.DOSizeDelta(newSize, duration), endCallback);
    }
    //너비 값 조정 애니메이션
    public static void DOWidth(RectTransform rect, float newWidth, float duration, Action endCallback = null)
    {
        Vector2 newSize = new Vector2(newWidth, rect.sizeDelta.y);
        AttachOnComplete(rect.DOSizeDelta(newSize, duration), endCallback);
    }
    //상하 운동 루프 애니메이션
    public static void DOHeightLoop(RectTransform rect, float newHeight, float duration)
    {
        Vector2 newSize = new Vector2(rect.sizeDelta.x, newHeight);
        rect.DOSizeDelta(newSize, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
    //스케일 Y 조정 애니메이션
    public static void DOScaleY(Transform transform, float newScaleY, float duration, Action endCallback = null)
    {
        AttachOnComplete(transform.DOScaleY(newScaleY, duration), endCallback);
    }
    private static void AttachOnComplete(Tween tween, Action endCallback)
    {
        tween.OnComplete(() => endCallback?.Invoke());
    }
}
