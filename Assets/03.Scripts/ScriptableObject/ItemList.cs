using UnityEngine;

[CreateAssetMenu(fileName = "ItemList", menuName = "ScriptableObject/ItemList")]
public class ItemList : ScriptableObject
{
    public Equipment[] EquipmentList;
}
