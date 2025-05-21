using UnityEngine;
using UnityEngine.SceneManagement;

public class CountManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static CountManager Instance { get; private set; }

    // 저장할 카운트 변수들
    private int totalTry = 0;
    private int totalClear = 0;
    private int totalItemCount = 0;
    private int totalClaim = 0;

    public int tryCount { get; private set; } 
    public int clearCount { get; private set; }
    public int itemCount { get; private set; }
    public int claimCount { get; private set; }

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

    public void AddTryCount(int count = 1)
    {
        tryCount += count;
    }
    public void AddClearCount(int count = 1)
    {
        clearCount += count;
    }

    // 아이템 획득 횟수 증가
    public void AddItemCount(int count)
    {
        itemCount += count;
        //Debug.Log("카운트"+itemCount);
    }
    public void AddClaimCount(int count = 1)
    {
        claimCount += count;
    }


    public void ApplyAllCounts()
    {
        //Day Clear 시 호출
        totalTry += tryCount;
        totalClear += clearCount;
        totalItemCount += itemCount;
        totalClaim += claimCount;
    }

    public void ResetTotal()
    {
        totalTry = 0;
        totalClear = 0;
        totalItemCount = 0;
        totalClaim = 0;
    }
    public void ResetCounts()
    {    // Day 시작할때 초기화
        tryCount = 0;
        clearCount = 0;
        itemCount = 0;
        claimCount = 0;
    }

    public int GetTotalTry() { return totalTry; }
    public int GetTotalClear() { return totalClear; }
    public int GetTotalItemCount() { return totalItemCount; }
    public int GetTotalClaim() { return totalClaim; }
}
