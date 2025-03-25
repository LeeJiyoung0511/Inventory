using UnityEngine;
using UnityEngine.UI;

public class UIStatus : UIBase
{
    [SerializeField] Text hpText;
    [SerializeField] Text attackPowerText;
    [SerializeField] Text defensePowerText;
    [SerializeField] Button returnButton;

    [Header("연출")]
    [SerializeField] ModifyHeightEffect scrollEffect;

    private Player player;

    protected override void Start()
    {
        base.Start();
        player = GameManager.Instance.Player;
        player.OnUpdateStatusEvent += SetStatus;
        SetStatus(player.Character);
        returnButton.onClick.AddListener(Return);
    }

    public override void Display()
    {
        scrollEffect.Expand();
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
        scrollEffect.Collapse();
        returnButton.interactable = false;
        Hide(EndCloseScrollEvent);
        SoundManager.Instance.PlaySE(SEType.Button);
    }

    private void EndCloseScrollEvent()
    {
        returnButton.interactable = true;
        uiManager.UIMainMenu.Display();
    }
}
