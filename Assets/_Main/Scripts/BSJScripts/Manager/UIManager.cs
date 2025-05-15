using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("�Ŵ���")]
    [SerializeField] private StageManager stageManager;

    [Header("HP �̹�����")]
    public Image[] hpImages;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (stageManager != null)
            stageManager.OnPlayerHpChanged += UpdateHpUI;
    }

    private void OnDisable()
    {
        if (stageManager != null)
            stageManager.OnPlayerHpChanged -= UpdateHpUI;
    }

    private void UpdateHpUI(int currentHp)
    {
        for (int i = 0; i < hpImages.Length; i++)
        {
            hpImages[i].enabled = (i < currentHp);
        }
    }
}
