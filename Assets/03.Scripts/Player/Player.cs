using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;

    public Character Character;
    public Action<Character> OnUpdateStatusEvent = delegate { };

    private void Awake()
    {
        GameManager.Instance.Player = this;

        Character = new Character(
            characterData.BaseAP, 
            characterData.BaseDP,
            characterData.BaseHP);
    }

    public void Equip(Equipment equipment)
    {
        if (equipment == null)
        {
            Debug.LogWarning("장비데이터가 null입니다");
            return;
        }
        ChangeStatus(equipment.EquipEffects);
        OnUpdateStatusEvent(Character);
    }

    public void UnEquip(Equipment equipment)
    {
        if (equipment == null)
        {
            Debug.LogWarning("장비데이터가 null입니다");
            return;
        }
        ChangeStatus(equipment.EquipEffects, -1);
        OnUpdateStatusEvent(Character);
    }

    //스탯 조정
    private void ChangeStatus(EquipEffect[] equipEffects, int sign = 1)
    {
        foreach (var effect in equipEffects)
        {
            float value = effect.EffectValue * sign;

            switch (effect.EffectType)
            {
                case EquipEffectType.AP:
                    Character.ModifyAttackPower(value);
                    break;
                case EquipEffectType.DP:
                    Character.ModifyDefensePower(value);
                    break;
                case EquipEffectType.HP:
                    Character.ModifyHP(value);
                    break;
            }
        }
    }
}
