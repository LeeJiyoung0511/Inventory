# 🎒 Unity Inventory System

> **기능 중심으로 설계한 유니티 인벤토리 과제입니다.**  
> 장비 시스템, 대사 시스템, 사운드, 상태머신 기반 구조로 구성되어 있습니다.

📽️ 영상

![시연 영상](https://github.com/user-attachments/assets/3bfa9596-6ec3-49a4-9e48-0ba6091ba61a)

## 📋 프로젝트 정보

| 항목 | 내용 |
|------|------|
| 🗓️ 제작 기간 | 2025.03.20 ~ 2025.03.26 (총 5일) |
| 💻 사용 언어 / 툴 | Unity (C#), DOTween,Newtonsoft JSON |


## 🔧 개발 포인트

- **재사용성과 확장성을 고려한 설계**
- **제네릭 기반 커스텀 상태머신(FSM)**
- **ScriptableObject를 활용한 데이터 설계**
- **DOTween 기반 UI 연출 시스템 구성**
- **이벤트 기반 UI 갱신 시스템**
- **SerializableDictionary 기반 효과음 처리**


## 🧩 주요 기능
<details>
<summary>✅장비 착용 및 해제 기능</summary>
<br>

1.🔗[EquipmentInventory.cs](./Assets/03.Scripts/UI/Inventory/EquipmentInventory.cs)

👉 유저가 장비를 클릭했을 때 장비 착용/해제를 컨트롤하는 핵심 로직

```csharp
    private void ClickItem(EquipmentSlot slot)
    {
        // 같은 장비를 다시 클릭하면 해제
        if (equippedEquipment == slot)
        {
            UnEquip(slot); //장착 해제
            SoundManager.Instance.PlaySE(SEType.EquipUnEquip); //해제 효과음 재생
            DialogueManager.Instance.MoveNextSpeechState(SpeechType.UnEquip); //장착 해제 대사 표시
            return;
        }

        // 기존 장비가 있다면 해제
        if (equippedEquipment != null)
        {
            UnEquip(equippedEquipment); //장착 해제
        }
        else
        {
            DialogueManager.Instance.MoveNextSpeechState(SpeechType.Equip); //장착 대사 표시
        }

        SoundManager.Instance.PlaySE(SEType.EquipUnEquip);  //장착 효과음 재생
        Equip(slot); //장착
    }
```
>장비를 클릭했을 때 착용/해제를 처리하며, 사운드 및 대사 상태도 동시에 전환하는 핵심 상호작용 로직입니다.
</details>

<details>
<summary>🧬장비에 따른 스탯 반영 시스템</summary>
<br>
    
1.🔗[Player.cs](./Assets/03.Scripts/Player/Player.cs)

👉 장비 효과에 따라 능력치를 조정
```csharp
    private void ChangeStatus(EquipEffect[] equipEffects, int sign = 1)
    {
        foreach (var effect in equipEffects)
        {
            float value = effect.EffectValue * sign;

            switch (effect.EffectType)
            {
                case EquipEffectType.AP:
                    Character.ModifyAttackPower(value);
                    break;
                case EquipEffectType.DP:
                    Character.ModifyDefensePower(value);
                    break;
                case EquipEffectType.HP:
                    Character.ModifyHP(value);
                    break;
            }
        }
    }
```
>장비의 능력치를 하나의 메서드에서 통합 적용/해제하여, 구조적으로 일관성을 유지했습니다.


2.🔗[UIStatus.cs](./Assets/03.Scripts/UI/UIStatus.cs)

👉 스탯 변경 시 자동 UI 갱신을 위한 이벤트 등록
```csharp
    protected override void Start()
    {
        base.Start();

        player = GameManager.Instance.Player;
        player.OnUpdateStatusEvent += SetStatus; //스탯 업데이트시 호출되는 함수 설정
        SetStatus(player.Character); //플레이어 스탯 표시

        returnButton.onClick.AddListener(Return);
    }
```
>OnUpdateStatusEvent 델리게이트를 통해,
>데이터 변경이 발생할 때마다 UIStatus가 자동으로 갱신됩니다.
</details>

<details>
<summary>🧠상태머신(FSM) 기반의 대사 처리 시스템</summary>
<br>

1.🔗[StateMachine.cs](./Assets/03.Scripts/StateMachine/StateMachine.cs)

👉 상태를 바꾸면 자동으로 상태 전환
```csharp
    public StateBase<T> Current
    {
        get => current;
        set
        {
            current.OnExit();
            current = value;
            Current.StateMachine = this;
            current.OnEnter();
        }
    }
```
>제네릭 구조를 통해 **상태 타입에 관계없이 확장 가능한 상태 관리 시스템**을 구현했습니다.

2-1.🔗[SpeechStateBase.cs](./Assets/03.Scripts/StateMachine/Speech/SpeechStateBase.cs)

👉 상태 진입 시 대사 출력
```csharp
    public override void OnEnter()
    {
        DialogueManager.Instance.ShowDialogue(SpeechType, MoveNormalState); //대사 표시
    }
    public override void OnExit() { }

    private void MoveNormalState()
    {
        StateMachine.MoveNextState(SpeechType.Normal); //기본 상태로 상태 전환
    }
```

2-2.🔗[DialogueManager.cs](./Assets/03.Scripts/Manager/DialogueManager.cs)  

👉 일정 시간 후 Normal 상태로 복귀
```csharp
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
```
> 이 구조를 통해 상태 진입 시 대사가 출력되며,  
> 일정 시간이 지나면 자동으로 Normal 상태로 전환되도록 처리됩니다.
</details>

<details>
<summary>🔊효과음 처리 시스템 (SerializableDictionary 기반)</summary>
<br>

1.🔗[SerializableDic.cs](./Assets/03.Scripts/Common/SerializableDic.cs)

👉 Enum 기반 오디오 접근을 위한 인덱서 처리
```csharp
    // 인덱서 정의
    public T2 this[T1 key]
    {
        get
        {
            try //키값 반환 시도
            {
                return GetValue(key); //문제 없을시 키값 반환 
            }
            catch (KeyNotFoundException e)
            {
                Debug.LogError($"{e.Message}"); // 키값 반환에 문제가 있으면 에러 표시
                return default; //default 반환
            }
        }
        set
        {
            for (int i = 0; i < dataList.Count; i++)
            {
                if (dataList[i].Key.Equals(key))
                {
                    dataList[i].Value = value; //vaule값 설정
                    return;
                }
            }
            // 키가 없으면 새로 추가
            dataList.Add(new SerializableData<T1, T2>() { Key = key, Value = value });
        }
    }
```

2.🔗[SoundManager.cs](./Assets/03.Scripts/Manager/SoundManager.cs)

👉 Enum을 통한 간단한 효과음 재생
```csharp
    // SEType열거형을 매개변수로 받아 해당하는 효과음 클립을 재생
    public void PlaySE(SEType type)
    {
        audioSource.PlayOneShot(dataList.SEClips[type]);
    }
```

> SEType Enum을 기반으로 효과음을 재생하며,
> SerializableDictionary를 사용하여 인스펙터에서도 Enum-Clip 매핑을 쉽게 설정할 수 있도록 구성했습니다.
</details>

<details>
<summary>🎛️DOTween을 사용한 UI 애니메이션</summary>
<br>

1.🔗[EffectManager.cs](./Assets/03.Scripts/Manager/EffectManager.cs)

👉 공통 애니메이션 래퍼 유틸
```csharp
    //서서히 보이게 하는 애니메이션
    public static void PlayFadeIn(CanvasGroup target, float duration, Action endCallback = null)
    {
        AttachOnComplete(target.DOFade(1f, duration), endCallback);
    }
    //서서히 사라지게 하는 애니메이션
    public static void PlayFadeOut(CanvasGroup target, float duration, Action endCallback = null)
    {
        AttachOnComplete(target.DOFade(0f, duration), endCallback);
    }
    ... 생략
    private static void AttachOnComplete(Tween tween, Action endCallback)
    {
        tween.OnComplete(() => endCallback?.Invoke());
    }
```
2.🔗[FadeInOutEffect.cs](./Assets/03.Scripts/Effect/FadeInOutEffect.cs)

👉 캔버스 그룹 페이드 애니메이션
```csharp
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float duration; //애니메이션이 진행될 시간(초)

    public void FadeIn(Action endFadeInEvent = null)
    {
        EffectManager.PlayFadeIn(canvasGroup, duration, endFadeInEvent);
    }

    public void FadeOut(Action endFadeOutEvent = null)
    {
        EffectManager.PlayFadeIn(canvasGroup, duration, endFadeOutEvent);
    }
```
> EffectManager에서 DOTween을 래핑한 공통 애니메이션 메서드를 관리하며,
> 각 효과 클래스는 인스펙터에서 설정 가능한 구조로 만들어 재사용성과 협업 효율을 높였습니다.
</details>

## 📚 출처
- 사용된 사운드: Music by 브그미언 Track 별 https://youtu.be/SEhaCeUftJ8
