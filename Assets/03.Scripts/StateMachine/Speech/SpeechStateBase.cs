public abstract class SpeechStateBase : StateBase<SpeechType>
{
    public abstract SpeechType SpeechType { get; }

    public override void OnEnter()
    {
        DialogueManager.Instance.ShowDialogue(SpeechType, MoveNormalState);
    }
    public override void OnExit() { }

    private void MoveNormalState()
    {
        StateMachine.MoveNextState(SpeechType.Normal);
    }
}
