using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private UIMainMenu uiMainMenu;
    [SerializeField] private UIInventory uiInventory;
    [SerializeField] private UIStatus uiStatus;
    [SerializeField] private BounceEffect charcterBreathEffect;

    public UIMainMenu UIMainMenu => uiMainMenu;
    public UIInventory UIInventory => uiInventory;
    public UIStatus UIStatus => uiStatus;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        charcterBreathEffect.PlayBounceEffect();
    }
}
