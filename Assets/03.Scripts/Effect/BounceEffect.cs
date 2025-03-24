using UnityEngine;

[System.Serializable]
public class BounceEffect
{
    [SerializeField] private RectTransform targetRect;
    [SerializeField] private float endHeight;
    [SerializeField] private float duration;

    public void PlayBounceEffect()
    {
        EffectManager.DOHeightLoop(targetRect,endHeight,duration);
    }
}
