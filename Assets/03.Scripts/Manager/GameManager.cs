using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameManager").AddComponent<GameManager>(); ;
            }
            return instance;
        }
    }

    private static GameManager instance;

    public Player Player { get; set; }

    public DataManager DataManager { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DataManager = DataManager.Instance;
        }
    }
    private void OnApplicationQuit()
    {
        DataManager.Instance.SaveData(); //에디터 실행정지시 게임저장
    }
}
