using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    //public static StageManager Instance { get; private set; }
    [Header("�÷��̾� HP (�ο�)")]
    public int playerMaxHp { get; private set; }
    public int playerCurHp { get; private set; }


    [Header("�������� ���� �ð�")]
    [field: SerializeField]
    public float stageMaxTime { get; private set; }
    public float stageCurTime { get; private set; }

    // �÷��̾� Hp�� ��ȭ�� �� ����Ǵ� �̺�Ʈ
    public event Action<int> OnPlayerHpChanged;

    //--------------------------------------------------

    [Header("Ʃ�丮���� �� üũ")]
    [SerializeField] private bool isTutorial;

    // �ð� �帧 ����
    private bool isTimerActive = false;

    //--------------------------------------------------

    private FadeController fadeController;
    private bool isStageClear = false;

    private void Awake()
    {
        // Hp �ʱ�ȭ
        playerMaxHp = 3;
        playerCurHp = playerMaxHp;

        // Stage Time �ʱ�ȭ
        // stageMaxTime = 60f; (�ν����Ϳ��� �ʱ�ȭ)
        stageCurTime = 0f;

        // FadeController �ʱ�ȭ
        fadeController = GetComponent<FadeController>();
    }

    private void Update()
    {
        // Ʃ�丮���̸� �ð��� �帣�� ����
        if (isTutorial)
        {
            return;
        }

        // Ʃ�丮���� �ƴϸ� ó������ �ð� �帧
        if (!isTutorial)
        {
            isTimerActive = true;
        }

        // �ð��� ��� �����ϴ� ����
        if (isTimerActive && stageCurTime < stageMaxTime)
        {
            stageCurTime += Time.deltaTime;
        }

        else if (!isStageClear && stageCurTime >= stageMaxTime)
        {
            isStageClear = true;

            // �������� Ŭ����
            ClearStage();
        }
    }

    /// <summary>
    /// ���� ���������� Ŭ�����ߴ��� ����
    /// </summary>
    /// <returns></returns>
    public bool GetIsClear()
    {
        return isStageClear;
    }

    /// <summary>
    /// �÷��̾��� HP�� ���ҽ�Ű�� �Լ�
    /// </summary>
    /// <param name="value"></param>
    public void DecreasePlayerHp(int value = 1)
    {
        playerCurHp -= value;
        OnPlayerHpChanged?.Invoke(playerCurHp);

        if (playerCurHp <= 0)
        {
            FailStage();
        }
    }

    /// <summary>
    /// �������� ����
    /// </summary>
    public void FailStage()
    {
        fadeController.DirectEndingFade(false);
    }

    /// <summary>
    /// �������� Ŭ����
    /// </summary>
    public void ClearStage()
    {
        fadeController.DirectEndingFade(true);
    }
}