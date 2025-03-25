using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentInventory : MonoBehaviour
{
    [SerializeField] private ScrollRect equipmentListScroll;
    [SerializeField] private DataList equipmentList;
    [SerializeField] private EquipmentSlot equipmentSlotPrefab;
    [SerializeField] private int MaxSlotCount = 30;

    private List<EquipmentSlot> equipmentSlots = new();
    private EquipmentSlot equippedEquipment = null;
    private Player player;

    private void Awake()
    {
        CreateSlots();
        SetItems();
    }

    private void Start()
    {
        player = GameManager.Instance.Player;
    }

    private void CreateSlots()
    {
        for (int i = 0; i < MaxSlotCount; i++)
        {
            var slot = Instantiate(equipmentSlotPrefab, equipmentListScroll.content);
            slot.gameObject.SetActive(false);
            equipmentSlots.Add(slot);
        }
    }

    public void SetItems()
    {
        //데이터 설정
        for (int i = 0; i < equipmentList.EquipmentList.Length; i++)
        {
            equipmentSlots[i].ItemData = equipmentList.EquipmentList[i];
            equipmentSlots[i].OnClickEvent = ClickItem;
            equipmentSlots[i].gameObject.SetActive(true);
        }
    }

    private void ClickItem(EquipmentSlot slot)
    {
        if (equippedEquipment == slot)
        {
            UnEquip(slot);
            SoundManager.Instance.PlaySE(SEType.EquipUnEquip);
            DialogueManager.Instance.MoveNextSpeechState(SpeechType.UnEquip);
        }
        else
        {
            if (equippedEquipment != null)
            {
                UnEquip(equippedEquipment);
            }
            else
            {
                DialogueManager.Instance.MoveNextSpeechState(SpeechType.Equip);
            }
            SoundManager.Instance.PlaySE(SEType.EquipUnEquip);
            Equip(slot);
        }
    }

    private void Equip(EquipmentSlot slot)
    {
        equippedEquipment = slot;
        slot.SetEquipImageVisible(true);   //장착 프레임 활성화
        player.Equip(slot.Data);   //스테이터스 증가
    }

    private void UnEquip(EquipmentSlot slot)
    {
        equippedEquipment = null;
        slot.SetEquipImageVisible(false);  //장착 프레임 비활성화
        player.UnEquip(slot.Data); //스테이터스 감소
    }
}
