using UnityEngine;
using UnityEngine.SceneManagement;

public class CountManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static CountManager Instance { get; private set; }

    // ������ ī��Ʈ ������
    public int clearCount { get; private set; }
    public int itemCount { get; private set; }

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
    public void AddClearCount(int amount = 1)
    {
        clearCount += amount;
    }

    // ������ ȹ�� Ƚ�� ����
    public void AddItemCount(int amount)
    {
        itemCount += amount;
        Debug.Log("ī��Ʈ"+itemCount);
    }

    // ���ϴ� ������ ���� ����
    public void ResetCounts()
    {
        clearCount = 0;
        itemCount = 0;
    }
}
