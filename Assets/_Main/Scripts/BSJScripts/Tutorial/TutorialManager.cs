using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
using System.Xml;
using DG.Tweening.Core.Easing;

public class TutorialManager : MonoBehaviour
{
    [Header("대사 텍스트")]
    [SerializeField] private TMP_Text speechText;

    [Header("말풍선 캔버스 그룹")]
    [SerializeField] CanvasGroup canvasGroup;

    // 스크립트 참조
    [Header("무전기 트윈 이펙트")]
    [SerializeField] private T_TalkleEffect t_talkleEffect;
    private WaitForSeconds speechWaitTime = new WaitForSeconds(0.5f);    // 말하기 까지 대기시간

    [Header("화살표")]
    [SerializeField] private EventDirectionArrow eventDirectionArrow;

    // ----------------------------------------

    [Header("태스크 매니저")]
    [SerializeField] private TaskManager taskManager;

    [Header("플레이어 컨트롤러")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerAnimator playerAnimator;

    [Header("돌발상황(미니게임) 오브젝트")] // 리스트로 관리할까 하다가 헷갈릴까봐 따로 객체하나씩 뺴놓음.
    [Header("에스컬레이터 고장 _ 튜토리얼 01")]
    [SerializeField] private RandomEventObject tutorialEventObject_01;
    [Header("취객 깨우기 _ 튜토리얼 02")]
    [SerializeField] private RandomEventObject tutorialEventObject_02;
    [Header("진상 고객 제압 _ 튜토리얼 03")]
    [SerializeField] private RandomEventObject tutorialEventObject_03;
    [Header("지도 안내 _ 튜토리얼 04")]
    [SerializeField] private RandomEventObject tutorialEventObject_04;
    [Header("엘리베이터 고장 _ 튜토리얼 05")]
    [SerializeField] private RandomEventObject tutorialEventObject_05;
    [Header("승객 추락 구조 _ 튜토리얼 06")]
    [SerializeField] private RandomEventObject tutorialEventObject_06;
    [Header("심장마비 _ 튜토리얼 07")]
    [SerializeField] private RandomEventObject tutorialEventObject_07;
    [Header("진상 취객 깨우기 _ 튜토리얼 08")]
    [SerializeField] private RandomEventObject tutorialEventObject_08;

    // 생성된 돌발상황 관리 리스트
    [Header("생성된 돌발상황 관리 리스트")]
    [SerializeField] private List<RandomEventObject> createdEventList;

    [Header("돌발상황 생성 포인트")]
    [SerializeField] private List<Transform> spawnPoint;

    // ----------------------------------------

    // 이벤트별 플래그
    private bool isEvent01 = false;     // 01번 이벤트  오프닝 컷신 이후
    private bool isEvent02 = false;     // 02번 이벤트  튜토리얼 미니게임 1  (에스컬레이터 고장)
    private bool isEvent03 = false;     // 03번 이벤트  튜토리얼 미니게임 2  (취객 깨우기)
    private bool isEvent04 = false;     // 04번 이벤트  튜토리얼 미니게임 3  (진상 고객 제압)
    private bool isEvent05 = false;     // 05번 이벤트  튜토리얼 미니게임 4  (지도 안내)
    private bool isEvent06 = false;     // 06번 이벤트  튜토리얼 미니게임 5  (엘레베이터 고장)
    private bool isEvent07 = false;     // 유실물 획득
    private bool isEvent08 = false;     // 08번 이벤트  튜토리얼 미니게임 7  (승객 추락 구조)
    private bool isEvent09 = false;     // 09번 이벤트  튜토리얼 미니게임 8  (심장마비)
    private bool isEvent10 = false;     // 10번 이벤트  튜토리얼 미니게임 9  (진상 취객 깨우기)
    private bool isEvent11 = false;     // 11번 이벤트  튜토리얼 종료

    // 대사 리스트들
    private List<string> textLines;
    private List<string> event01_Lines;    // 오프닝 컷신 이후
    private List<string> event02_Lines;    // 튜토리얼 미니게임 1  (에스컬레이터 고장)
    private List<string> event03_Lines;    // 튜토리얼 미니게임 2  (취객 깨우기)
    private List<string> event04_Lines;    // 튜토리얼 미니게임 3  (진상 고객 제압)
    private List<string> event05_Lines;    // 튜토리얼 미니게임 4  (지도 안내)
    private List<string> event06_Lines;    // 튜토리얼 미니게임 5  (엘레베이터 고장)
    private List<string> event07_Lines;    // 유실물 획득
    private List<string> event08_Lines;    // 튜토리얼 미니게임 7  (승객 추락 구조)
    private List<string> event09_Lines;    // 튜토리얼 미니게임 8  (심장마비)
    private List<string> event10_Lines;    // 튜토리얼 미니게임 9  (진상 취객 깨우기)
    private List<string> event11_Lines;    // 튜토리얼 종료

    private int currentLine = -1;

    private Coroutine typingCoroutine;
    private bool isTyping = false;

    // ----------------------------------------

    private FadeController fadeController;

    // ----------------------------------------

    // 튜토리얼을 한번이라도 클리어했는가?
    private bool[] tutorialClears = { false, false, false, false, false, false, false, false};

    void Start()
    {
        // 오프닝 컷신 이후
        event01_Lines = new List<string>
        {
            "너가 새로운 수습이구나.",
            "우리 역은\n돌발 상황이 가끔 생기거든?",
            "설명은 한 번만 해줄거야.",
            "해결 실패해서 민원 들어오면\n합격 못한다~"
        };

        // 화살표 설명 + 튜토리얼 미니게임 1  (에스컬레이터 고장)
        event02_Lines = new List<string>
        {
            "열쇠를 넣고,\n표시된 각도까지만 잡고 돌려.",
            "너무 감으로 돌리지 마.",
        };

        // 튜토리얼 미니게임 2  (취객 깨우기)
        event03_Lines = new List<string>
        {
            "방향키를 눌러서 취객을 깨워봐.",
        };

        // 튜토리얼 미니게임 3  (진상 고객 제압)
        event04_Lines = new List<string>
        {
            "게이지 범위에 맞춰서 딱 누르면 돼.\n떨면 끝.",
        };

        // 튜토리얼 미니게임 4  (지도 안내)
        event05_Lines = new List<string>
        {
            "역에서 시작해서\n길을 따라 목적지를 이어.",
            "목적지는 배경이 다를거야.",
        };

        // 튜토리얼 미니게임 5  (엘레베이터 고장)
        event06_Lines = new List<string>
        {
            "전선? 색깔 맞춰서 잇는 거야.\n딱보면 모르겠어?",
        };

        // 유실물 획득
        event07_Lines = new List<string>
        {
            "아 참,",
            "역에 가끔 유실물이\n떨어진 경우가 있어.",
            "발견하면 역무실로 가져와~",
        };

        // 튜토리얼 미니게임 7  (승객 추락 구조)
        event08_Lines = new List<string>
        {
            "연타해서 구조 게이지 채워!",
        };

        // 튜토리얼 미니게임 8  (심장마비)
        event09_Lines = new List<string>
        {
            "버튼을 눌러 심장박동\n 게이지를 올리고 유지해!",
            "네 손에 달렸다.",
        };

        // 튜토리얼 미니게임 9  (진상 취객 깨우기)
        event10_Lines = new List<string>
        {
            "진상 취객한테 \n움찔하면 지는 거다.",
            "웃는 타이밍에 맞춰서\n 버튼을 눌러",
        };

        // 튜토리얼 종료
        event11_Lines = new List<string>
        {
            "설명 끝. 내일부터 출근해.",
            "근무시간은\n12시부터 0시까지야 잊지마.",
            "내일부턴 일에 시간제한도 걸려있어.\n각오하고 오는게 좋을걸?",
        };

        fadeController = GetComponent<FadeController>();
        canvasGroup.alpha = 0;

        // 오브젝트 전체 생성
        // 튜토리얼 1 생성
        CreateMinigameObject(tutorialEventObject_01, spawnPoint[0].position, 1);
        CreateMinigameObject(tutorialEventObject_02, spawnPoint[1].position, 1);
        CreateMinigameObject(tutorialEventObject_03, spawnPoint[2].position, 1);
        CreateMinigameObject(tutorialEventObject_04, spawnPoint[3].position, 1);
        CreateMinigameObject(tutorialEventObject_05, spawnPoint[4].position, 1);
        CreateMinigameObject(tutorialEventObject_06, spawnPoint[5].position, 2);
        CreateMinigameObject(tutorialEventObject_07, spawnPoint[6].position, 2);
        CreateMinigameObject(tutorialEventObject_08, spawnPoint[7].position, 2);

        // 10초 후 이벤트1 시작
        Invoke(nameof(StartEvent1), 10f);
    }

    void Update()
    {
        if (isEvent01 || isEvent02 || isEvent03 || isEvent04 || isEvent05 || isEvent06
             || isEvent07 || isEvent08 || isEvent09 || isEvent10 || isEvent11)
        {
            if (playerController != null)
                playerController.canMove = false;
            if (playerAnimator != null)
                playerAnimator.SetMoved(false);

            if (Input.GetMouseButtonDown(0))
                HandleTypingInput();
        }

        // 테스트 ( 튜토리얼 미니게임 인풋 끝난 다음에 콜백 받아서 다음 이벤트 실행시켜야함 )
        if (Input.GetKeyDown(KeyCode.F1))
            StartEvent1();
        else if (Input.GetKeyDown(KeyCode.F2))
            StartEvent2();
        else if (Input.GetKeyDown(KeyCode.F3))
            StartEvent3();
        else if (Input.GetKeyDown(KeyCode.F4))
            StartEvent4();
        else if (Input.GetKeyDown(KeyCode.F5))
            StartEvent5();
        else if (Input.GetKeyDown(KeyCode.F6))
            StartEvent6();
        else if (Input.GetKeyDown(KeyCode.F7))
            StartEvent7();
        else if (Input.GetKeyDown(KeyCode.F8))
            StartEvent8();
        else if (Input.GetKeyDown(KeyCode.F9))
            StartEvent9();
        else if (Input.GetKeyDown(KeyCode.F10))
            StartEvent10();
        else if (Input.GetKeyDown(KeyCode.F11))
            StartEvent11();
    }

    /// <summary>
    /// 타이핑 핸들러
    /// </summary>
    void HandleTypingInput()
    {
        if (isTyping)
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                typingCoroutine = null;
            }
            speechText.text = textLines[currentLine];
            isTyping = false;
        }
        else
        {
            StartCoroutine(ShowNextLine());
        }
    }

    /// <summary>
    /// 다음 줄 텍스트 보이게 하기
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowNextLine()
    {
        if (currentLine >= textLines.Count - 1)
        {
            // 이벤트별 플래그 false 처리 및 종료 애니메이션, 페이드 아웃 처리

            if (isEvent01)
            {
                isEvent01 = false;
                yield return EndEvent1_Routine();
            }
            else if (isEvent02)
            {
                isEvent02 = false;
                yield return EndEvent2_Routine();
            }
            else if (isEvent03)
            {
                isEvent03 = false;
                yield return EndEvent3_Routine();
            }
            else if (isEvent04)
            {
                isEvent04 = false;
                yield return EndEvent4_Routine();
            }
            else if (isEvent05)
            {
                isEvent05 = false;
                yield return EndEvent5_Routine();
            }
            else if (isEvent06)
            {
                isEvent06 = false;
                yield return EndEvent6_Routine();
            }
            else if (isEvent07)
            {
                isEvent07 = false;
                yield return EndEvent7_Routine();
            }
            else if (isEvent08)
            {
                isEvent08 = false;
                yield return EndEvent8_Routine();
            }
            else if (isEvent09)
            {
                isEvent09 = false;
                yield return EndEvent9_Routine();
            }
            else if (isEvent10)
            {
                isEvent10 = false;
                yield return EndEvent10_Routine();
            }
            else if (isEvent11)
            {
                isEvent11 = false;
                yield return EndEvent11_Routine();
            }

            // 플레이어가 다시 이동할 수 있게
            if(playerController != null)
            {
                playerController.canMove = true;
            }
            yield break;
        }

        currentLine++;
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeTextCoroutine(textLines[currentLine]));
    }

    /// <summary>
    /// 텍스트가 타이핑 되는 효과처럼 보이는 코루틴
    /// </summary>
    /// <param name="fullText"></param>
    /// <returns></returns>
    IEnumerator TypeTextCoroutine(string fullText)
    {
        isTyping = true;
        speechText.text = "";

        for (int i = 0; i < fullText.Length; i++)
        {
            speechText.text += fullText[i];
            yield return new WaitForSeconds(0.05f);
        }

        isTyping = false;

        yield return new WaitForSeconds(1f);
    }

    #region 이벤트
    #region 이벤트 1 오프닝 컷신 이후
    public void StartEvent1()
    {
        textLines = event01_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(StartEvent1WithDelay());
    }

    private IEnumerator StartEvent1WithDelay()
    {
        yield return new WaitForSeconds(1f);  // 1초 대기

        canvasGroup.alpha = 1;
        currentLine = -1;

        isEvent01 = true;
        StartCoroutine(ShowNextLine());
    }
    IEnumerator EndEvent1_Routine()
    {
        // 무전기 내려가는 트윈
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // 팝업 페이드아웃
        canvasGroup.DOFade(0, 1f);
        speechText.text = "";
    }
    #endregion

    #region 이벤트 2 에스컬레이터 고장
    public void StartEvent2()
    {
        textLines = event02_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(StartEvent2WithDelay());
    }

    private IEnumerator StartEvent2WithDelay()
    {
        yield return new WaitForSeconds(1f);  // 1초 대기

        canvasGroup.alpha = 1;
        currentLine = -1;

        isEvent02 = true;
        StartCoroutine(ShowNextLine());
    }

    IEnumerator EndEvent2_Routine()
    {
        // 무전기 내려가는 트윈
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // 팝업 페이드아웃
        canvasGroup.DOFade(0, 1f);
        speechText.text = "";

        // 튜토리얼을 모두 진행했으면 10초 후에 튜토리얼 종료
        if (AllTutorialsCleared()) { Invoke(nameof(StartEvent11), 10f); }
    }
    #endregion

    #region 이벤트 3 취객 깨우기
    public void StartEvent3()
    {
        textLines = event03_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(StartEvent3WithDelay());
    }

    private IEnumerator StartEvent3WithDelay()
    {
        yield return new WaitForSeconds(1f);  // 1초 대기

        canvasGroup.alpha = 1;
        currentLine = -1;

        isEvent03 = true;
        StartCoroutine(ShowNextLine());
    }

    IEnumerator EndEvent3_Routine()
    {
        // 무전기 내려가는 트윈
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // 팝업 페이드아웃
        canvasGroup.DOFade(0, 1f);
        speechText.text = "";

        // 튜토리얼을 모두 진행했으면 10초 후에 튜토리얼 종료
        if (AllTutorialsCleared()) { Invoke(nameof(StartEvent11), 10f); }
    }
    #endregion

    #region 이벤트 4 진상 고객 제압
    public void StartEvent4()
    {
        textLines = event04_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(StartEvent4WithDelay());
    }

    private IEnumerator StartEvent4WithDelay()
    {
        yield return new WaitForSeconds(1f);  // 1초 대기

        canvasGroup.alpha = 1;
        currentLine = -1;

        isEvent04 = true;
        StartCoroutine(ShowNextLine());
    }

    IEnumerator EndEvent4_Routine()
    {
        // 무전기 내려가는 트윈
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // 팝업 페이드아웃
        canvasGroup.DOFade(0, 1f);
        speechText.text = "";

        // 튜토리얼을 모두 진행했으면 10초 후에 튜토리얼 종료
        if (AllTutorialsCleared()) { Invoke(nameof(StartEvent11), 10f); }
    }
    #endregion

    #region 이벤트 5 지도 안내
    public void StartEvent5()
    {
        textLines = event05_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(StartEvent5WithDelay());
    }

    private IEnumerator StartEvent5WithDelay()
    {
        yield return new WaitForSeconds(1f);  // 1초 대기

        canvasGroup.alpha = 1;
        currentLine = -1;

        isEvent05 = true;
        StartCoroutine(ShowNextLine());
    }

    IEnumerator EndEvent5_Routine()
    {
        // 무전기 내려가는 트윈
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // 팝업 페이드아웃
        canvasGroup.DOFade(0, 1f);
        speechText.text = "";

        // 튜토리얼을 모두 진행했으면 10초 후에 튜토리얼 종료
        if (AllTutorialsCleared()) { Invoke(nameof(StartEvent11), 10f); }
    }
    #endregion

    #region 이벤트 6 엘리베이터 고장
    public void StartEvent6()
    {
        textLines = event06_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(StartEvent6WithDelay());
    }

    private IEnumerator StartEvent6WithDelay()
    {
        yield return new WaitForSeconds(1f);  // 1초 대기

        canvasGroup.alpha = 1;
        currentLine = -1;

        isEvent06 = true;
        StartCoroutine(ShowNextLine());
    }

    IEnumerator EndEvent6_Routine()
    {
        // 무전기 내려가는 트윈
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // 팝업 페이드아웃
        canvasGroup.DOFade(0, 1f);
        speechText.text = "";

        // 튜토리얼을 모두 진행했으면 10초 후에 튜토리얼 종료
        if (AllTutorialsCleared()) { Invoke(nameof(StartEvent11), 10f); }
    }
    #endregion

    #region 이벤트 7 유실물
    public void StartEvent7()
    {
        textLines = event07_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(StartEvent7WithDelay());
    }

    private IEnumerator StartEvent7WithDelay()
    {
        yield return new WaitForSeconds(1f);  // 1초 대기

        canvasGroup.alpha = 1;
        currentLine = -1;

        isEvent07 = true;
        StartCoroutine(ShowNextLine());
    }

    IEnumerator EndEvent7_Routine()
    {
        // 무전기 내려가는 트윈
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // 팝업 페이드아웃
        canvasGroup.DOFade(0, 1f);
        speechText.text = "";
    }
    #endregion

    #region 이벤트 8 승객 추락 구조
    public void StartEvent8()
    {
        textLines = event08_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(StartEvent8WithDelay());
    }

    private IEnumerator StartEvent8WithDelay()
    {
        yield return new WaitForSeconds(1f);  // 1초 대기

        canvasGroup.alpha = 1;
        currentLine = -1;

        isEvent08 = true;
        StartCoroutine(ShowNextLine());
    }

    IEnumerator EndEvent8_Routine()
    {
        // 무전기 내려가는 트윈
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // 팝업 페이드아웃
        canvasGroup.DOFade(0, 1f);
        speechText.text = "";

        // 튜토리얼을 모두 진행했으면 10초 후에 튜토리얼 종료
        if (AllTutorialsCleared()) { Invoke(nameof(StartEvent11), 10f); }
    }
    #endregion

    #region 이벤트 9 심장마비
    public void StartEvent9()
    {
        textLines = event09_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(StartEvent9WithDelay());
    }

    private IEnumerator StartEvent9WithDelay()
    {
        yield return new WaitForSeconds(1f);  // 1초 대기

        canvasGroup.alpha = 1;
        currentLine = -1;

        isEvent09 = true;
        StartCoroutine(ShowNextLine());
    }

    IEnumerator EndEvent9_Routine()
    {
        // 무전기 내려가는 트윈
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // 팝업 페이드아웃
        canvasGroup.DOFade(0, 1f);
        speechText.text = "";

        // 튜토리얼을 모두 진행했으면 10초 후에 튜토리얼 종료
        if (AllTutorialsCleared()) { Invoke(nameof(StartEvent11), 10f); }
    }
    #endregion

    #region 이벤트 10 진상 취객 깨우기
    public void StartEvent10()
    {
        textLines = event10_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(StartEvent10WithDelay());
    }

    private IEnumerator StartEvent10WithDelay()
    {
        yield return new WaitForSeconds(1f);  // 1초 대기

        canvasGroup.alpha = 1;
        currentLine = -1;

        isEvent10 = true;
        StartCoroutine(ShowNextLine());
    }

    IEnumerator EndEvent10_Routine()
    {
        // 무전기 내려가는 트윈
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // 팝업 페이드아웃
        canvasGroup.DOFade(0, 1f);
        speechText.text = "";

        // 튜토리얼을 모두 진행했으면 10초 후에 튜토리얼 종료
        if(AllTutorialsCleared()) { Invoke(nameof(StartEvent11), 10f); }
    }
    #endregion

    #region 이벤트 11 튜토리얼 끝
    public void StartEvent11()
    {
        textLines = event11_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(StartEvent11WithDelay());
    }

    private IEnumerator StartEvent11WithDelay()
    {
        yield return new WaitForSeconds(1f);  // 1초 대기

        canvasGroup.alpha = 1;
        currentLine = -1;

        isEvent11 = true;
        StartCoroutine(ShowNextLine());
    }

    IEnumerator EndEvent11_Routine()
    {
        // 무전기 내려가는 트윈
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // 팝업 페이드아웃
        canvasGroup.DOFade(0, 1f);
        speechText.text = "";

        // 페이드 아웃 엔딩씬
        if (fadeController != null)
        {
            fadeController.DirectEndingFade(false);
        }
    }
    #endregion
    #endregion

    // --------------------------------------------------

    /// <summary>
    /// 튜토리얼 미니게임 생성
    /// </summary>
    /// <param name="_eventObject">미니게임 유형</param>
    /// <param name="_eventPosition">생성위치</param>
    /// <param name="tutorialIndex">튜토리얼 번호</param>
    private void CreateMinigameObject(RandomEventObject _eventObject, Vector3 _eventPosition, int tutorialIndex)
    {
        GameObject eventObject = Instantiate(_eventObject.gameObject, _eventPosition, Quaternion.identity);
        if (eventObject.TryGetComponent<RandomEventObject>(out RandomEventObject randomEvent))
        {
            createdEventList.Add(randomEvent);

            // 콜백 (상호작용 성공 / 실패) 등록
            randomEvent.onEventSuccess += OnRandomEventSuccess;
            randomEvent.onEventFailed += OnRandomEventInteractFailed;

            // 미니게임 생성용 참조
            randomEvent.ReferTaskManager(taskManager);

            // 화살표 생성 (튜토리얼은 화살표 생성 X)
            // eventDirectionArrow.CreateArrow(randomEvent, tutorialIndex);
        }
    }

    // 랜덤 돌발상황 상호작용 성공 감지
    private void OnRandomEventSuccess(RandomEventObject successEvent)
    {
        Debug.Log("이벤트 상호작용 성공 감지 : " + successEvent.name);

        // 대사 시작
        if(successEvent.task == KindOfTask.TurningKey)
        {
            StartEvent2();
            tutorialClears[0] = true;
        }
        else if(successEvent.task == KindOfTask.ArrowMatch)
        {
            StartEvent3();
            tutorialClears[1] = true;
        }
        else if (successEvent.task == KindOfTask.MovingCircle)
        {
            StartEvent4();
            tutorialClears[2] = true;
        }
        else if (successEvent.task == KindOfTask.MapGuide)
        {
            StartEvent5();
            tutorialClears[3] = true;
        }
        else if (successEvent.task == KindOfTask.FixWire)
        {
            StartEvent6();
            tutorialClears[4] = true;
        }
        else if (successEvent.task == KindOfTask.StackingGauge)
        {
            StartEvent8();
            tutorialClears[5] = true;
        }
        else if (successEvent.task == KindOfTask.MaintainingGauge)
        {
            StartEvent9();
            tutorialClears[6] = true;
        }
        else if (successEvent.task == KindOfTask.Swinging)
        {
            StartEvent10();
            tutorialClears[7] = true;
        }

        // 이벤트 해제 (누수 방지)
        successEvent.onEventSuccess -= OnRandomEventSuccess;
        successEvent.onEventFailed -= OnRandomEventInteractFailed;

        createdEventList.Remove(successEvent);
        eventDirectionArrow.RemoveArrow(successEvent);
        Destroy(successEvent.gameObject);
    }

    // 랜덤 돌발상황 상호작용 실패 감지
    private void OnRandomEventInteractFailed(RandomEventObject failedEvent)
    {
        Debug.Log("이벤트 상호작용 실패 감지 : " + failedEvent.name);

        // 이벤트 해제 (누수 방지)
        failedEvent.onEventSuccess -= OnRandomEventSuccess;
        failedEvent.onEventFailed -= OnRandomEventInteractFailed;

        createdEventList.Remove(failedEvent);           // 생성된 돌발상황 오브젝트 삭제
        eventDirectionArrow.RemoveArrow(failedEvent);   // 추적하는 화살표 삭제
        Destroy(failedEvent.gameObject);                // 오브젝트 파괴
    }

    // 모든 튜토리얼을 클리어했는지 체크하는 함수
    private bool AllTutorialsCleared()
    {
        for (int i = 0; i < tutorialClears.Length; i++)
        {
            if (!tutorialClears[i])
                return false;
        }
        return true;
    }
}