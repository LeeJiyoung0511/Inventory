public abstract class SpeechStateBase : StateBase<SpeechType>
{
    public abstract SpeechType SpeechType { get; }

    public override void OnEnter()
    {
        DialogueManager.Instance.ShowDialogue(SpeechType, MoveNormalState); //대사 표시
    }
    public override void OnExit() { }

    private void MoveNormalState()
    {
        StateMachine.MoveNextState(SpeechType.Normal); //기본 상태로 상태 전환
    }
}
