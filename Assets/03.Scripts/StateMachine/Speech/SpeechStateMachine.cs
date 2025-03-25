using System.Collections.Generic;

public class SpeechStateMachine : StateMachine<SpeechType>
{
    protected override Dictionary<SpeechType, StateBase<SpeechType>> CreateStateDic()
    {
        return new Dictionary<SpeechType, StateBase<SpeechType>>()
                {
                    { SpeechType.Equip,  new Speech_Equip() },
                    { SpeechType.UnEquip,  new Speech_UnEquip() },
                    { SpeechType.Normal,  new Speech_Normal() },
                };
    }
}
