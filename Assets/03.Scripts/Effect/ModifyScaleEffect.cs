using System;
using UnityEngine;

[Serializable]
public class ModifyScaleEffect
{
    [SerializeField] private Transform target;
    [SerializeField] private float duration;
    [SerializeField] private float startScale;
    [SerializeField] private float endScale;

    public void ExpandScaleY(Action endCallback = null)
    {
        EffectManager.DOScaleY(target, endScale, duration, endCallback);
    }

    public void CollapseScaleY(Action endCallback = null)
    {
        EffectManager.DOScaleY(target, startScale, duration, endCallback);
    }
}
