using System;
using UnityEngine;

public class ScrollController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private int closeLayer = Animator.StringToHash("IsClose");
    public Action OnCloseScrollEvent = delegate { };

    public void PlayCloseScroll()
    {
        animator.SetTrigger(closeLayer);
    }

    public void OnCloseScroll()
    {
        OnCloseScrollEvent();
    }
}
