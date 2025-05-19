using System;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    //public static StageManager Instance { get; private set; }
    [Header("플레이어 HP (민원)")]
    public int playerMaxHp { get; private set; }
    public int playerCurHp { get; private set; }


    [Header("스테이지 제한 시간")]

    public float stageMaxTime { get; private set; }
    public float stageCurTime { get; private set; }

    // 플레이어 Hp가 변화할 때 실행되는 이벤트
    public event Action<int> OnPlayerHpChanged;

    //--------------------------------------------------

    [Header("튜토리얼일 때 체크")]
    [SerializeField] private bool isTutorial;

    // 시간 흐름 관련
    private bool isTimerActive = false;
    private float tutorialDelay = 10f; // 튜토리얼일 때 지연 시간
    private float tutorialTimer = 0f;

    //--------------------------------------------------

    private FadeController fadeController;

    private void Awake()
    {

        // Hp 초기화
        playerMaxHp = 3;
        playerCurHp = playerMaxHp;

        // Stage Time 초기화
        stageMaxTime = 180f;
        stageCurTime = 0f;

        // FadeController 초기화
        fadeController = GetComponent<FadeController>();
    }

    private void Update()
    {
        // 튜토리얼이면 10초 대기 후 시간 흐름 시작
        if (isTutorial && !isTimerActive)
        {
            tutorialTimer += Time.deltaTime;
            if (tutorialTimer >= tutorialDelay)
            {
                isTimerActive = true;
            }
            return;
        }

        // 튜토리얼이 아니면 처음부터 시간 흐름
        if (!isTutorial)
        {
            isTimerActive = true;
        }

        // 시간이 계속 증가하는 로직
        if (isTimerActive && stageCurTime < stageMaxTime)
        {
            stageCurTime += Time.deltaTime;
        }

        else if (stageCurTime >= stageMaxTime)
        {
            // TODO : 스테이지 클리어
        }
    }

    /// <summary>
    /// 플레이어의 HP를 감소시키는 함수
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
    /// 스테이지 실패
    /// </summary>
    public void FailStage()
    {
        fadeController.DirectEndingFade();
    }

    /// <summary>
    /// 스테이지 클리어
    /// </summary>
    public void ClearStage()
    {

    }
}
