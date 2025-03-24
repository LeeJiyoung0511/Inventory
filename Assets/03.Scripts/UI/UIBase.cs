using UnityEngine;

public class UIBase : MonoBehaviour
{
    protected UIManager uiManager;

    protected virtual void Start()
    {
        uiManager = UIManager.Instance;
    }

    public bool IsShow //표시되어있는지
    {
        set
        {
            Display(value);
        }
    }

    protected virtual void Display(bool isShow) 
    {
        gameObject.SetActive(isShow);
    }
}
