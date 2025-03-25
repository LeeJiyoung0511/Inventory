using System;
using UnityEngine;

[System.Serializable]
public class ModifyHeightEffect
{
    [SerializeField] RectTransform targetRect;
    [SerializeField] float duration; //애니메이션이 진행될 시간(초)
    [SerializeField] float startHeight; //초기 높이
    [SerializeField] float endHeight; //애니메이션 종료 시 목표 높이

    public Action OnEndExpandEvent = delegate { }; //확장이 끝난뒤에 호출되는 이벤트
    public Action OnEndCollapseEvent = delegate { }; //축소가 끝난뒤에 호출되는 이벤트

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
