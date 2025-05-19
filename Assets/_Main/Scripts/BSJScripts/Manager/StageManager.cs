using System;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    //public static StageManager Instance { get; private set; }
    [Header("�÷��̾� HP (�ο�)")]
    public int playerMaxHp { get; private set; }
    public int playerCurHp { get; private set; }


    [Header("�������� ���� �ð�")]

    public float stageMaxTime { get; private set; }
    public float stageCurTime { get; private set; }

    // �÷��̾� Hp�� ��ȭ�� �� ����Ǵ� �̺�Ʈ
    public event Action<int> OnPlayerHpChanged;

    //--------------------------------------------------

    [Header("Ʃ�丮���� �� üũ")]
    [SerializeField] private bool isTutorial;

    // �ð� �帧 ����
    private bool isTimerActive = false;
    private float tutorialDelay = 10f; // Ʃ�丮���� �� ���� �ð�
    private float tutorialTimer = 0f;

    //--------------------------------------------------

    private FadeController fadeController;

    private void Awake()
    {

        // Hp �ʱ�ȭ
        playerMaxHp = 3;
        playerCurHp = playerMaxHp;

        // Stage Time �ʱ�ȭ
        stageMaxTime = 180f;
        stageCurTime = 0f;

        // FadeController �ʱ�ȭ
        fadeController = GetComponent<FadeController>();
    }

    private void Update()
    {
        // Ʃ�丮���̸� 10�� ��� �� �ð� �帧 ����
        if (isTutorial && !isTimerActive)
        {
            tutorialTimer += Time.deltaTime;
            if (tutorialTimer >= tutorialDelay)
            {
                isTimerActive = true;
            }
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

        else if (stageCurTime >= stageMaxTime)
        {
            // TODO : �������� Ŭ����
        }
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
        fadeController.DirectEndingFade();
    }

    /// <summary>
    /// �������� Ŭ����
    /// </summary>
    public void ClearStage()
    {

    }
}
