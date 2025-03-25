public class Speech_Normal : SpeechStateBase
{
    public override SpeechType SpeechType => SpeechType.Normal;

    public override void OnEnter()
    {
        DialogueManager.Instance.ShowDialogueLoop(SpeechType);
    }

    public override void OnExit()
    {
        base.OnExit();
        DialogueManager.Instance.StopDialogueLoop();
    }
}
