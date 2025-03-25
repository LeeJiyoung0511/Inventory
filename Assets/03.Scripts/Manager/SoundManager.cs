using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private DataList dataList;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // SEType열거형을 매개변수로 받아 해당하는 효과음 클립을 재생
    public void PlaySE(SEType type)
    {
        audioSource.PlayOneShot(dataList.SEClips[type]);
    }
}
