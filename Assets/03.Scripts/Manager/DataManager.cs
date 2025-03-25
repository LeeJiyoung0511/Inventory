using Newtonsoft.Json;
using UnityEngine;

public class DataManager
{
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DataManager();
            }
            return instance;
        }
    }

    private static DataManager instance = null; 
    private static readonly string SAVEDATA_KEY = "SaveData"; //저장키값
    private static bool isInitialized = false; //초기화 했는지

    private DataManager()
    {
        LoadData();
    }

    //저장할 데이터---------------------------
    public Equipment CurrentEquipment; //현재 착용하고 있는 장비
    //-------------------------------------

    //데이터 저장
    public void SaveData()
    {
        string json = JsonConvert.SerializeObject(instance);
        PlayerPrefs.SetString(SAVEDATA_KEY, json);
        PlayerPrefs.Save();
    }

    //데이터 불러오기
    public  void LoadData()
    {
        if (isInitialized) return; //초기화 되어있다면
        if (!PlayerPrefs.HasKey(SAVEDATA_KEY)) return; //저장키가 없다면
        isInitialized = true; //초기화 완료
        string json = PlayerPrefs.GetString(SAVEDATA_KEY); 
        var saveData = JsonConvert.DeserializeObject<DataManager>(json);
        JsonConvert.PopulateObject(json, this); // 싱글톤 인스턴스에 값만 덮어씀
    }
}
