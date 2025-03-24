using System;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    [SerializeField] public CanvasGroup CanvasGroup;

    protected UIManager uiManager;

    protected virtual void Start()
    {
        uiManager = UIManager.Instance;
    }

    public virtual void Display()
    {
        gameObject.SetActive(true);
        EffectManager.PlayFadeIn(CanvasGroup, 0.3f);
    }

    public virtual void Hide(Action OnEndFadeOut = null)
    {
        Action endAction = () =>
        {
            gameObject.SetActive(false);
            OnEndFadeOut?.Invoke();
        };
        EffectManager.PlayFadeOut(CanvasGroup, 0.3f, endAction);
    }
}
