using System;
using UnityEngine;

[System.Serializable]
public class ModifyHeightEffect
{
    [SerializeField] RectTransform targetRect;
    [SerializeField] float duration;
    [SerializeField] float startHeight;
    [SerializeField] float endHeight;

    public Action OnEndExpandEvent = delegate { };
    public Action OnEndCollapseEvent = delegate { };

    //확장
    public void Expand()
    {
        EffectManager.DOHeight(targetRect, endHeight, duration, OnEndExpandEvent);
    }

    //축소
    public void Collapse()
    {
        EffectManager.DOHeight(targetRect, startHeight, duration, OnEndCollapseEvent);
    }
}
