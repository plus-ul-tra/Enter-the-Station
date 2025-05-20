using UnityEngine;
using UnityEngine.SceneManagement;

public class CountManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static CountManager Instance { get; private set; }

    // 저장할 카운트 변수들
    public int clearCount { get; private set; }
    public int itemCount { get; private set; }

    void Awake()
    {
        // 싱글톤 패턴 적용
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // 미니게임 트라이 횟수

    // 클리어 횟수 증가
    public void AddClearCount(int amount = 1)
    {
        clearCount += amount;
    }

    // 아이템 획득 횟수 증가
    public void AddItemCount(int amount)
    {
        itemCount += amount;
        Debug.Log("카운트"+itemCount);
    }

    // 원하는 곳에서 리셋 가능
    public void ResetCounts()
    {
        clearCount = 0;
        itemCount = 0;
    }
}
