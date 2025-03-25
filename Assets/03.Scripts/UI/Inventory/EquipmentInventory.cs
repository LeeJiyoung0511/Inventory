using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentInventory : MonoBehaviour
{
    [SerializeField] private ScrollRect equipmentListScroll;
    [SerializeField] private DataList equipmentList;
    [SerializeField] private EquipmentSlot equipmentSlotPrefab;
    [SerializeField] private int maxSlotCount = 30; //최대 슬롯수

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

    //슬롯 생성
    private void CreateSlots()
    {
        //최대 슬롯수까지 미리 생성 하고 비활성화
        for (int i = 0; i < maxSlotCount; i++)
        {
            var slot = Instantiate(equipmentSlotPrefab, equipmentListScroll.content);
            slot.gameObject.SetActive(false);
            equipmentSlots.Add(slot);
        }
    }

    //슬롯 데이터 설정
    public void SetItems()
    {
        //데이터 수 만큼 데이터 설정과 활성화
        for (int i = 0; i < equipmentList.EquipmentList.Length; i++)
        {
            equipmentSlots[i].ItemData = equipmentList.EquipmentList[i];
            equipmentSlots[i].OnClickEvent = ClickItem;
            equipmentSlots[i].gameObject.SetActive(true);
        }
    }

    private void ClickItem(EquipmentSlot slot)
    {
        // 같은 장비를 다시 클릭하면 해제
        if (equippedEquipment == slot)
        {
            UnEquip(slot); //장착 해제
            SoundManager.Instance.PlaySE(SEType.EquipUnEquip); //해제 효과음 재생
            DialogueManager.Instance.MoveNextSpeechState(SpeechType.UnEquip); //장착 해제 대사 표시
            return;
        }

        // 기존 장비가 있다면 해제
        if (equippedEquipment != null)
        {
            UnEquip(equippedEquipment); //장착 해제
        }
        else
        {
            DialogueManager.Instance.MoveNextSpeechState(SpeechType.Equip); //장착 대사 표시
        }

        SoundManager.Instance.PlaySE(SEType.EquipUnEquip);  //장착 효과음 재생
        Equip(slot); //장착
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
