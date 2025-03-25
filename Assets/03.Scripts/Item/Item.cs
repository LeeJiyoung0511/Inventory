using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string ItemName; //아이템 이름
    public string ItemDesc; // 아이템 설명
    public ItemType ItemType; //아이템 타입
    [JsonIgnore]
    public Sprite Sprite; //아이템 이미지
}

[System.Serializable]
public class Equipment : Item
{
    public EquipEffect[] EquipEffects; //장착시 적용할 효과
}

[System.Serializable]
public class EquipEffect
{
    public EquipEffectType EffectType; //효과타입
    public float EffectValue; //효과값
}

