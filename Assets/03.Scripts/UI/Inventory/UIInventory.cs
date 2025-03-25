using UnityEngine;
using UnityEngine.UI;

public class UIInventory : UIBase
{
    [Header("UI")]
    [SerializeField] private Button returnButton;
    [SerializeField] private EquipmentInventory equipmentInventory;

    [Header("애니메이션")]
    [SerializeField] private ModifyHeightEffect scrollImageEffect; // 스크롤 이미지의 확장/축소
    [SerializeField] private ModifyHeightEffect scrollRectEffect; // scrollRect의 확장/축소

    protected override void Start()
    {
        base.Start();
        returnButton.onClick.AddListener(Return);
    }

    public override void Display()
    {
        scrollImageEffect.Expand(); //스크롤 이미지 확장
        scrollRectEffect.Expand(); //scrollRect 확장
        base.Display();
    }

    private void Return()
    {
        scrollImageEffect.Collapse(); //스크롤 이미지 축소
        scrollRectEffect.Collapse(); //scrollRect 축소
        returnButton.interactable = false; //버튼 비활성화
        Hide(EndCloseScrollEvent); //인벤토리 닫기
        SoundManager.Instance.PlaySE(SEType.Button); //버튼 효과음 재생
    }

    //스크롤 이미지 축소 애니메이션이 끝났을때 호출 
    private void EndCloseScrollEvent()
    {
        returnButton.interactable = true; //버튼 활성화
        uiManager.UIMainMenu.Display(); //메인 메뉴 표시
    }
}
