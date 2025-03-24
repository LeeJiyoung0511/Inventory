using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObject/Character")]
public class CharacterData : ScriptableObject
{
    public float BaseAP; 
    public float BaseDP;
    public float BaseHP;
}
