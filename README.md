# 🎒 Unity Inventory System

> **기능 중심으로 설계한 유니티 인벤토리 과제입니다.**  
> 장비 시스템, 대사 시스템, 사운드, 상태머신 기반 구조로 구성되어 있습니다.


## 📋 프로젝트 정보

| 항목 | 내용 |
|------|------|
| 🗓️ 제작 기간 | 2024.03.20 ~ 2024.03.25 (총 4일) |
| 💻 사용 언어 / 툴 | Unity (C#), DOTween,Newtonsoft JSON |


## 🔧 개발 포인트

- 코드 구조의 **확장성과 재사용성**에 중점을 둔 설계  
- 상태별 대사 처리를 위해 **커스텀 상태머신(FSM)** 구현  
- 대사/사운드/장비 데이터를 **ScriptableObject**로 유연하게 관리  
- **UI 연출 요소를 통합 관리**하는 부모 클래스(`UIBase`) 설계  
- **딕셔너리 데이터를 인스펙터에서 직렬화**하여, 기획자도 에디터 상에서 쉽게 수정 가능하도록 구성


## 🧩 주요 기능
<details>
<summary>✅장비 착용 및 해제 기능</summary>

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
</details>

- 🧠 **상태머신(FSM) 기반의 대사 처리 시스템**
- 💬 **ScriptableObject 기반 대사/사운드 데이터 관리**
- 🔄 **딕셔너리 직렬화 구조 (SerializableDictionary)**
- 🔊 **효과음 처리 시스템**
- 🎛️ **DOTween을 사용한 UI 애니메이션**
- 🧬 **장비에 따른 스탯 반영 시스템**
- 🔁 **이벤트 기반 UI 갱신**

## 📚 출처
- 사용된 사운드: Music by 브그미언 Track 별 https://youtu.be/SEhaCeUftJ8

