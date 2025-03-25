using UnityEngine;

[CreateAssetMenu(fileName = "DataList", menuName = "ScriptableObject/DataList")]
public class DataList : ScriptableObject
{
    public Equipment[] EquipmentList;
    public SpeechData[] SpeechDataList;
    public SerializableDic<SEType, AudioClip> SEClips;
}
