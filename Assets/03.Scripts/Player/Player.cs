using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;

    private Character character;

    public Action<Character> OnUpdateStatusEvent = delegate { };

    private void Awake()
    {
        GameManager.Instance.Player = this;

        character = new Character(
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
        OnUpdateStatusEvent(character);
    }

    public void UnEquip(Equipment equipment)
    {
        if (equipment == null)
        {
            Debug.LogWarning("장비데이터가 null입니다");
            return;
        }
        ChangeStatus(equipment.EquipEffects, -1);
        OnUpdateStatusEvent(character);
    }

    private void ChangeStatus(EquipEffect[] equipEffects, int sign = 1)
    {
        foreach (var effect in equipEffects)
        {
            float value = effect.EffectValue * sign;

            switch (effect.EffectType)
            {
                case EquipEffectType.AP:
                    character.ModifyAttackPower(value);
                    break;
                case EquipEffectType.DP:
                    character.ModifyDefensePower(value);
                    break;
                case EquipEffectType.HP:
                    character.ModifyHP(value);
                    break;
            }
        }
    }
}
