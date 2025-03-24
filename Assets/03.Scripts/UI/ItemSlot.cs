using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemSlot : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image icon; //아이템 이미지

    private void Start()
    {
        button.onClick.AddListener(OnClick);
    }

    public Item ItemData //슬롯에 표시할 아이템 데이터
    {
        get => itemData;
        set
        {
            itemData = value;
            SetData();
        }
    }

    private Item itemData = null;

    protected virtual void SetData()
    {
        icon.sprite = ItemData.Sprite;
    }

    protected abstract void OnClick();
}
