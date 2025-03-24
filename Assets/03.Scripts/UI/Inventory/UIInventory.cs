using UnityEngine;
using UnityEngine.UI;

public class UIInventory : UIBase
{
    [SerializeField] private Button returnButton;
    [SerializeField] private EquipmentInventory equipmentInventory;

    [Header("연출")]
    [SerializeField] private ModifyHeightEffect scrollEffect;
    [SerializeField] private ModifyHeightEffect scrollViewEffect;

    protected override void Start()
    {
        base.Start();
        returnButton.onClick.AddListener(Return);
    }

    public override void Display()
    {
        scrollEffect.Expand(); //스크롤 확장
        scrollViewEffect.Expand();
        base.Display();
    }

    private void Return()
    {
        scrollEffect.Collapse();
        scrollViewEffect.Collapse();
        returnButton.interactable = false;
        Hide(EndCloseScrollEvent);
    }

    private void EndCloseScrollEvent()
    {
        returnButton.interactable = true;
        uiManager.UIMainMenu.Display();
    }
}
