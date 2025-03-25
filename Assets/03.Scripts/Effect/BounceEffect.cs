using UnityEngine;

[System.Serializable]
public class BounceEffect
{
    [SerializeField] private RectTransform targetRect;
    [SerializeField] private float endHeight; //애니메이션 종료 시 목표 높이
    [SerializeField] private float duration; //애니메이션이 진행될 시간(초)

    public void PlayBounceEffect()
    {
        EffectManager.DOHeightLoop(targetRect,endHeight,duration);
    }
}
