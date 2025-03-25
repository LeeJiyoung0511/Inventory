public class Speech_Normal : SpeechStateBase
{
    public override SpeechType SpeechType => SpeechType.Normal;

    public override void OnEnter()
    {
        DialogueManager.Instance.ShowDialogueLoop(SpeechType); //대사 반복 표시
    }

    public override void OnExit()
    {
        base.OnExit();
        DialogueManager.Instance.StopDialogueLoop(); //대사 반복 표시 정지
    }
}
