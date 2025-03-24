using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private UIMainMenu uiMainMenu;
    [SerializeField] private UIInventory uiInventory;
    [SerializeField] private UIStatus uiStatus;

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
}
