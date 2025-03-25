using UnityEngine;

[System.Serializable]
public class SpeechData
{
    public SpeechType SpeechType;
    [TextArea]
    public string[] DialogueText;

    public string GetDialogueText()
    {
        var randomIndex = Random.Range(0, DialogueText.Length);
        return DialogueText[randomIndex];
    }
}
