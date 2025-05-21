using UnityEngine;
using UnityEngine.SceneManagement;

public class CountManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static CountManager Instance { get; private set; }

    // ������ ī��Ʈ ������
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
        // �̱��� ���� ����
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // �̴ϰ��� Ʈ���� Ƚ��

    // Ŭ���� Ƚ�� ����

    public void AddTryCount(int count = 1)
    {
        tryCount += count;
    }
    public void AddClearCount(int count = 1)
    {
        clearCount += count;
    }

    // ������ ȹ�� Ƚ�� ����
    public void AddItemCount(int count)
    {
        itemCount += count;
        //Debug.Log("ī��Ʈ"+itemCount);
    }
    public void AddClaimCount(int count = 1)
    {
        claimCount += count;
    }


    public void ApplyAllCounts()
    {
        //Day Clear �� ȣ��
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
    {    // Day �����Ҷ� �ʱ�ȭ
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
