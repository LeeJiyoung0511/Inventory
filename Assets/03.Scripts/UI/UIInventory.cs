using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : UIBase
{
    [SerializeField] private ScrollController scrollController;
    [SerializeField] private Button returnButton;
    [SerializeField] private EquipmentInventory equipmentInventory;

    protected override void Start()
    {
        base.Start();
        returnButton.onClick.AddListener(Return);
        scrollController.OnCloseScrollEvent += OnCloseScroll;
    }

    protected override void Display(bool isShow)
    {
        base.Display(isShow);
        equipmentInventory.SetItems();
    }

    private void Return()
    {
        scrollController.PlayCloseScroll();
        returnButton.gameObject.SetActive(false);
    }

    private void OnCloseScroll()
    {
        Display(false);
        returnButton.gameObject.SetActive(true);
        uiManager.UIMainMenu.IsShow = true;
    }
}
