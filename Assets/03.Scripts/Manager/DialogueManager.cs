using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] private Text dialogueText;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private DataList dataList;

    [SerializeField] private float displayTime; //말풍선 표시 지속시간
    [SerializeField] private float displayDelayTime; //말풍선의 표시 쿨타임 

    private SpeechStateMachine speechStateMachine = new(); //대사 상태 머신
    private Coroutine showDialogueLoopCoroutine;
    private Coroutine hideDialogueCoroutine;

    private Action OnEndDelayEvent = delegate { };  //말풍선 표시시간이 끝났을때 호출되는 함수

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        MoveNextSpeechState(SpeechType.Normal); //초기 상태
    }

    //대사 상태 전환
    public void MoveNextSpeechState(SpeechType type)
    {
        speechStateMachine.MoveNextState(type);
    }

    // 일반 대사 루프 출력
    public void ShowDialogueLoop(SpeechType type)
    {
        OnEndDelayEvent = null;
        showDialogueLoopCoroutine = StartCoroutine(IShowDialogueLoop(type));
    }

    //일반 대사 루프 출력 정지
    public void StopDialogueLoop()
    {
        if (showDialogueLoopCoroutine != null)
        {
            StopCoroutine(showDialogueLoopCoroutine);
            showDialogueLoopCoroutine = null;
        }
        canvasGroup.alpha = 0;
        dialogueText.text = string.Empty;
    }

    //대사 출력
    public void ShowDialogue(SpeechType type, Action endEffectEvent = null)
    {
        OnEndDelayEvent = endEffectEvent;
        EffectManager.PlayFadeIn(canvasGroup, 0.3f, HideDialogue);
        dialogueText.text = GetDialogue(type);
    }

    //대사 비표시
    private void HideDialogue()
    {
        if (hideDialogueCoroutine != null)
        {
            StopCoroutine(hideDialogueCoroutine);
        }
        hideDialogueCoroutine = StartCoroutine(IHideDialogue());
    }

    private IEnumerator IHideDialogue()
    {
        yield return new WaitForSeconds(displayTime); //대사 표시후 일정 시간 대기
        EffectManager.PlayFadeOut(canvasGroup, 0.3f, OnEndDelayEvent); //일정 시간 후 말풍선 서서히 사라지기
        dialogueText.text = string.Empty; //텍스트 비우기
    }

    public IEnumerator IShowDialogueLoop(SpeechType type)
    {
        while (true)
        {
            yield return new WaitForSeconds(displayDelayTime); //일정 시간 대기
            ShowDialogue(type); // 일정시간 대기후 다시 일반 대사 표시
        }
    }

    //대사 데이터 반환
    private string GetDialogue(SpeechType type)
    {
        var data = dataList.SpeechDataList.FirstOrDefault(x => x.SpeechType == type);
        return data.GetDialogueText();
    }
}
