using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UIBase
{
    [SerializeField] private Button inventoryButton; //인벤토리 표시 버튼
    [SerializeField] private Button statusButton; //스테이터스 표시 버튼

    protected override void Start()
    {
        base.Start();
        inventoryButton.onClick.AddListener(DisplayInventory);
        statusButton.onClick.AddListener(DisplayStatus);
    }

    private void DisplayInventory()
    {  
        Hide(uiManager.UIInventory.Display);
        SoundManager.Instance.PlaySE(SEType.Button);
    }

    private void DisplayStatus()
    {
        Hide(uiManager.UIStatus.Display);
        SoundManager.Instance.PlaySE(SEType.Button);
    }
}
