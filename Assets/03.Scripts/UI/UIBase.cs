using System;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    [SerializeField] private  FadeInOutEffect fadeInOutEffect;
    protected UIManager uiManager;

    protected virtual void Start()
    {
        uiManager = UIManager.Instance;
    }

    public virtual void Display()
    {
        gameObject.SetActive(true);
        fadeInOutEffect.FadeIn();
    }

    public virtual void Hide(Action OnEndFadeOut = null)
    {
        Action endAction = () =>
        {
            gameObject.SetActive(false);
            OnEndFadeOut?.Invoke();
        };
        fadeInOutEffect.FadeOut(endAction);
    }
}
