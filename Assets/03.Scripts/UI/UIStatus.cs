using UnityEngine;
using UnityEngine.UI;

public class UIStatus : UIBase
{
    [Header("UI")]
    [SerializeField] Text hpText;
    [SerializeField] Text attackPowerText;
    [SerializeField] Text defensePowerText;
    [SerializeField] Button returnButton;

    [Header("연출")]
    [SerializeField] ModifyHeightEffect scrollImageEffect; // 스크롤 이미지의 확장/축소

    private Player player;

    protected override void Start()
    {
        base.Start();

        player = GameManager.Instance.Player;
        player.OnUpdateStatusEvent += SetStatus; //스탯 업데이트시 호출되는 함수 설정
        SetStatus(player.Character); //플레이어 스탯 표시

        returnButton.onClick.AddListener(Return);
    }

    public override void Display()
    {
        scrollImageEffect.Expand(); //스크롤 이미지의 확장
        base.Display();
    }

    private void SetStatus(Character charcter)
    {
        hpText.text = $"체력 : {charcter.HP}";
        attackPowerText.text = $"공격력 : {charcter.AttackPower}";
        defensePowerText.text = $"방어력 : {charcter.DefensePower}";
    }

    private void Return()
    {
        scrollImageEffect.Collapse(); //스크롤 이미지의 축소
        returnButton.interactable = false; //버튼 비활성화
        Hide(EndCloseScrollEvent); //스탯창 닫기
        SoundManager.Instance.PlaySE(SEType.Button); //버튼 효과음 재생
    }

    private void EndCloseScrollEvent()
    {
        returnButton.interactable = true;
        uiManager.UIMainMenu.Display();
    }
}
