using System;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : ItemSlot
{
    [SerializeField] private Image equipImage; //장착시 표시될 프레임 이미지

    public Equipment Data => ItemData as Equipment;

    public Action<EquipmentSlot> OnClickEvent = delegate { };

    protected override void SetData()
    {
        base.SetData();
        SetEquipImageVisible(false);
    }

    //장착 프레임 표시 비표시 
    public void SetEquipImageVisible(bool isVisible)
    {
        equipImage.gameObject.SetActive(isVisible);
    }

    protected override void OnClick()
    {
        OnClickEvent(this);
    }
}
