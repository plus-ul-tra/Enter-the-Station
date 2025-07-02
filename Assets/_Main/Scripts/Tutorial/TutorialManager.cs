using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("��� �ؽ�Ʈ")]
    [SerializeField] private TMP_Text speechText;

    [Header("��ǳ�� ĵ���� �׷�")]
    [SerializeField] CanvasGroup canvasGroup;

    // ��ũ��Ʈ ����
    [Header("������ Ʈ�� ����Ʈ")]
    [SerializeField] private T_TalkleEffect t_talkleEffect;
    private WaitForSeconds speechWaitTime = new WaitForSeconds(0.5f);    // ���ϱ� ���� ���ð�

    [Header("ȭ��ǥ")]
    [SerializeField] private EventDirectionArrow eventDirectionArrow;

    // ----------------------------------------

    [Header("�½�ũ �Ŵ���")]
    [SerializeField] private TaskManager taskManager;

    [Header("�÷��̾� ��Ʈ�ѷ�")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private PlayerFootsteps playerFootsteps;

    [Header("���߻�Ȳ(�̴ϰ���) ������Ʈ")] // ����Ʈ�� �����ұ� �ϴٰ� �򰥸���� ���� ��ü�ϳ��� ������.
    [Header("�����÷����� ���� _ Ʃ�丮�� 01")]
    [SerializeField] private RandomEventObject tutorialEventObject_01;
    [Header("�밴 ����� _ Ʃ�丮�� 02")]
    [SerializeField] private RandomEventObject tutorialEventObject_02;
    [Header("���� �� ���� _ Ʃ�丮�� 03")]
    [SerializeField] private RandomEventObject tutorialEventObject_03;
    [Header("���� �ȳ� _ Ʃ�丮�� 04")]
    [SerializeField] private RandomEventObject tutorialEventObject_04;
    [Header("���������� ���� _ Ʃ�丮�� 05")]
    [SerializeField] private RandomEventObject tutorialEventObject_05;
    [Header("�°� �߶� ���� _ Ʃ�丮�� 06")]
    [SerializeField] private RandomEventObject tutorialEventObject_06;
    [Header("���帶�� _ Ʃ�丮�� 07")]
    [SerializeField] private RandomEventObject tutorialEventObject_07;
    [Header("���� �밴 ����� _ Ʃ�丮�� 08")]
    [SerializeField] private RandomEventObject tutorialEventObject_08;

    // ������ ���߻�Ȳ ���� ����Ʈ
    [Header("������ ���߻�Ȳ ���� ����Ʈ")]
    [SerializeField] private List<RandomEventObject> createdEventList;

    [Header("���߻�Ȳ ���� ����Ʈ")]
    [SerializeField] private List<Transform> spawnPoint;

    // ----------------------------------------

    // �̺�Ʈ�� �÷���
    private bool isEvent01 = false;     // 01�� �̺�Ʈ  ������ �ƽ� ����
    private bool isEvent02 = false;     // 02�� �̺�Ʈ  Ʃ�丮�� �̴ϰ��� 1  (�����÷����� ����)
    private bool isEvent03 = false;     // 03�� �̺�Ʈ  Ʃ�丮�� �̴ϰ��� 2  (�밴 �����)
    private bool isEvent04 = false;     // 04�� �̺�Ʈ  Ʃ�丮�� �̴ϰ��� 3  (���� �� ����)
    private bool isEvent05 = false;     // 05�� �̺�Ʈ  Ʃ�丮�� �̴ϰ��� 4  (���� �ȳ�)
    private bool isEvent06 = false;     // 06�� �̺�Ʈ  Ʃ�丮�� �̴ϰ��� 5  (���������� ����)
    private bool isEvent07 = false;     // ���ǹ� ȹ��
    private bool isEvent08 = false;     // 08�� �̺�Ʈ  Ʃ�丮�� �̴ϰ��� 7  (�°� �߶� ����)
    private bool isEvent09 = false;     // 09�� �̺�Ʈ  Ʃ�丮�� �̴ϰ��� 8  (���帶��)
    private bool isEvent10 = false;     // 10�� �̺�Ʈ  Ʃ�丮�� �̴ϰ��� 9  (���� �밴 �����)
    private bool isEvent11 = false;     // 11�� �̺�Ʈ  Ʃ�丮�� ����

    // ��� ����Ʈ��
    private List<string> textLines;
    private List<string> event01_Lines;    // ������ �ƽ� ����
    private List<string> event02_Lines;    // Ʃ�丮�� �̴ϰ��� 1  (�����÷����� ����)
    private List<string> event03_Lines;    // Ʃ�丮�� �̴ϰ��� 2  (�밴 �����)
    private List<string> event04_Lines;    // Ʃ�丮�� �̴ϰ��� 3  (���� �� ����)
    private List<string> event05_Lines;    // Ʃ�丮�� �̴ϰ��� 4  (���� �ȳ�)
    private List<string> event06_Lines;    // Ʃ�丮�� �̴ϰ��� 5  (���������� ����)
    private List<string> event07_Lines;    // ���ǹ� ȹ��
    private List<string> event08_Lines;    // Ʃ�丮�� �̴ϰ��� 7  (�°� �߶� ����)
    private List<string> event09_Lines;    // Ʃ�丮�� �̴ϰ��� 8  (���帶��)
    private List<string> event10_Lines;    // Ʃ�丮�� �̴ϰ��� 9  (���� �밴 �����)
    private List<string> event11_Lines;    // Ʃ�丮�� ����

    // �̺�Ʈ�� �÷���
    private bool istalkleEvent01 = false;     // 01�� �̺�Ʈ  ������ �ƽ� ����
    private bool istalkleEvent02 = false;     // 02�� �̺�Ʈ  Ʃ�丮�� �̴ϰ��� 1  (�����÷����� ����)
    private bool istalkleEvent03 = false;     // 03�� �̺�Ʈ  Ʃ�丮�� �̴ϰ��� 2  (�밴 �����)
    private bool istalkleEvent04 = false;     // 04�� �̺�Ʈ  Ʃ�丮�� �̴ϰ��� 3  (���� �� ����)
    private bool istalkleEvent05 = false;     // 05�� �̺�Ʈ  Ʃ�丮�� �̴ϰ��� 4  (���� �ȳ�)
    private bool istalkleEvent06 = false;     // 06�� �̺�Ʈ  Ʃ�丮�� �̴ϰ��� 5  (���������� ����)
    private bool istalkleEvent07 = false;     // ���ǹ� ȹ��
    private bool istalkleEvent08 = false;     // 08�� �̺�Ʈ  Ʃ�丮�� �̴ϰ��� 7  (�°� �߶� ����)
    private bool istalkleEvent09 = false;     // 09�� �̺�Ʈ  Ʃ�丮�� �̴ϰ��� 8  (���帶��)
    private bool istalkleEvent10 = false;     // 10�� �̺�Ʈ  Ʃ�丮�� �̴ϰ��� 9  (���� �밴 �����)
    private bool istalkleEvent11 = false;     // 11�� �̺�Ʈ  Ʃ�丮�� ����

    // �������� �Ѿ�� ��� ����Ʈ
    private List<string> miniGameStart01_Lines;     // Ʃ�丮�� �̴ϰ��� 1 ���� ���    (�����÷����� ����)   02�� �̺�Ʈ
    private List<string> miniGameStart02_Lines;     // Ʃ�丮�� �̴ϰ��� 2 ���� ���    (�밴 �����)        03�� �̺�Ʈ
    private List<string> miniGameStart03_Lines;     // Ʃ�丮�� �̴ϰ��� 3 ���� ���    (���� �� ����)      04�� �̺�Ʈ
    private List<string> miniGameStart04_Lines;     // Ʃ�丮�� �̴ϰ��� 4 ���� ���    (���� �ȳ�)          05�� �̺�Ʈ
    private List<string> miniGameStart05_Lines;     // Ʃ�丮�� �̴ϰ��� 5 ���� ���    (���������� ����)     06�� �̺�Ʈ
    private List<string> miniGameStart06_Lines;     // Ʃ�丮�� �̴ϰ��� 7 ���� ���    (�°� �߶� ����)      08�� �̺�Ʈ
    private List<string> miniGameStart07_Lines;     // Ʃ�丮�� �̴ϰ��� 8 ���� ���    (���帶��)           09�� �̺�Ʈ
    private List<string> miniGameStart08_Lines;     // Ʃ�丮�� �̴ϰ��� 9 ���� ���    (���� �밴 �����)    10�� �̺�Ʈ

    private int currentLine = -1;

    private Coroutine typingCoroutine;
    private bool isTyping = false;

    // ----------------------------------------

    private FadeController fadeController;

    // ----------------------------------------

    // Ʃ�丮���� �ѹ��̶� Ŭ�����ߴ°�?
    private bool[] tutorialClears = { false, false, false, false, false, false, false, false };
    private List<GameObject> createdObjects = new List<GameObject>();

    void Start()
    {
        // ������ �ƽ� ����
        event01_Lines = new List<string>
        {
            "�ʰ� ���ο� �����̱���.",
            "�츮���� ���߻�Ȳ��\n����~�� ����ŵ�?",
            "������ ���� ��Ȳ��\n�ذ��� ���ž�~",
            "���� �ѹ� �ѷ�����!",
        };

        // ȭ��ǥ ���� + Ʃ�丮�� �̴ϰ��� 1  (�����÷����� ����)
        event02_Lines = new List<string>
        {
            "���踦 �ְ�,\nǥ�õ� ���������� ��� ����.",
            "�ʹ� ������ ������ ��.",
        };

        // Ʃ�丮�� �̴ϰ��� 2  (�밴 �����)
        event03_Lines = new List<string>
        {
            "����Ű�� ������ �밴�� ������.",
        };

        // Ʃ�丮�� �̴ϰ��� 3  (���� �� ����)
        event04_Lines = new List<string>
        {
            "������ ������ ���缭 �� ������ ��.\n���� �󱼶� �Ǽ��ϸ� ���̾�.",
        };

        // Ʃ�丮�� �̴ϰ��� 4  (���� �ȳ�)
        event05_Lines = new List<string>
        {
            "������ ���콺 Ŭ������ �����ؼ�\n �巡�׷� ���� ���� �������� �̾�.",
            "�������� �ʷϻ� ǥ�þ�.",
            "��Ȯ�� ���� ���󰡾���!",

        };

        // Ʃ�丮�� �̴ϰ��� 5  (���������� ����)
        event06_Lines = new List<string>
        {
            "����? ���ʿ��� ���������� \n���� ���缭 �մ� �ž�.",
            "������ �𸣰ھ�?",
        };

        // ���ǹ� ȹ��
        event07_Lines = new List<string>
        {
            "�� ��,",
            "���� ���� �нǹ���\n������ ��찡 �־�.",
            "�߰��ϸ� �������̳� �ν��� ������~",
        };

        // Ʃ�丮�� �̴ϰ��� 7  (�°� �߶� ����)
        event08_Lines = new List<string>
        {
            "��Ÿ�ؼ� ���� ������ ä��!",
        };

        // Ʃ�丮�� �̴ϰ��� 8  (���帶��)
        event09_Lines = new List<string>
        {
            "��ư�� ���� ����ڵ�\n �������� �ø��� ������!",
        };

        // Ʃ�丮�� �̴ϰ��� 9  (���� �밴 �����)
        event10_Lines = new List<string>
        {
            "���� �밴���� \n�����ϸ� ���� �Ŵ�.",
            "���� Ÿ�ֿ̹� ���缭\n �����̽��� ����",
        };

        // Ʃ�丮�� ����
        event11_Lines = new List<string>
        {
            "���� ��. ���Ϻ��� �����.",
            "�ٹ��ð���\n12�ú��� 0�ñ����� ������.",
            "���Ϻ��� �Ͽ� �ð����ѵ� �ɷ��־�.\n�����ϰ� ���°� ������?",
        };

        // Ʃ�丮�� �̴ϰ��� 1 �����÷����� ���� ���� ��� (event 02)
        miniGameStart01_Lines = new List<string>
        {
            "��, �����÷����� ����"
        };

        // Ʃ�丮�� �̴ϰ��� 2 �밴 ����� ���� ��� (event 03)
        miniGameStart02_Lines = new List<string>
        {
            "��, �밴 �����"
        };

        // Ʃ�丮�� �̴ϰ��� 3 ���� �� ���� ���� ��� (event 04)
        miniGameStart03_Lines = new List<string>
        {
            "��, ���� �� ����"
        };

        // Ʃ�丮�� �̴ϰ��� 4 ���� �ȳ� ���� ��� (event 05)
        miniGameStart04_Lines = new List<string>
        {
            "��, ���� �ȳ�"
        };

        // Ʃ�丮�� �̴ϰ��� 5 ���������� ���� ���� ��� (event 06)
        miniGameStart05_Lines = new List<string>
        {
            "��, ���������� ����"
        };

        // Ʃ�丮�� �̴ϰ��� 7 �°� �߶� ���� ���� ��� (event 08)
        miniGameStart06_Lines = new List<string>
        {
            "��, �°� �߶� ����"
        };

        // Ʃ�丮�� �̴ϰ��� 8 ���帶�� ���� ��� (event 09)
        miniGameStart07_Lines = new List<string>
        {
            "��, ���帶��"
        };

        // Ʃ�丮�� �̴ϰ��� 9 ���� �밴 ����� ���� ��� (event 10)
        miniGameStart08_Lines = new List<string>
        {
            "��, ���� �밴 �����"
        };

        fadeController = GetComponent<FadeController>();
        canvasGroup.alpha = 0;

        //// ������Ʈ ��ü ����
        //RespawnEventObject(0);

        // 10�� �� �̺�Ʈ1 ����
        Invoke(nameof(StartEvent1), 10f);
    }

    void Update()
    {
        if (isEvent01 || isEvent02 || isEvent03 || isEvent04 || isEvent05 || isEvent06
             || isEvent07 || isEvent08 || isEvent09 || isEvent10 || isEvent11
             || istalkleEvent01 || istalkleEvent02 || istalkleEvent03 || istalkleEvent04
             || istalkleEvent05 || istalkleEvent06 || istalkleEvent07 || istalkleEvent08
             || istalkleEvent09 || istalkleEvent10 || istalkleEvent11 )
        {
            if (Input.GetMouseButtonDown(0))
                HandleTypingInput();
        }

        //if (isEvent01 || isEvent07)
        if (isEvent01 || isEvent02 || isEvent03 || isEvent04 || isEvent05 || isEvent06
             || isEvent07 || isEvent08 || isEvent09 || isEvent10 || isEvent11
             || istalkleEvent01 || istalkleEvent02 || istalkleEvent03 || istalkleEvent04
             || istalkleEvent05 || istalkleEvent06 || istalkleEvent07 || istalkleEvent08
             || istalkleEvent09 || istalkleEvent10 || istalkleEvent11)
        {
            // �÷��̾ �̵� ���ϰ�
            if (playerController != null)
            {
                playerController.canMove = false;
            }

            if (playerAnimator != null)
            {
                playerAnimator.SetMoved(false);
            }

            playerFootsteps.StopfootstepsSound();
        }

        // �׽�Ʈ ( Ʃ�丮�� �̴ϰ��� ��ǲ ���� ������ �ݹ� �޾Ƽ� ���� �̺�Ʈ ������Ѿ��� )
        //if (Input.GetKeyDown(KeyCode.F1))
        //    StartEvent1();
        //else if (Input.GetKeyDown(KeyCode.F2))
        //    StartEvent2();
        //else if (Input.GetKeyDown(KeyCode.F3))
        //    StartEvent3();
        //else if (Input.GetKeyDown(KeyCode.F4))
        //    StartEvent4();
        //else if (Input.GetKeyDown(KeyCode.F5))
        //    StartEvent5();
        //else if (Input.GetKeyDown(KeyCode.F6))
        //    StartEvent6();
        //else if (Input.GetKeyDown(KeyCode.F7))
        //    StartEvent7();
        //else if (Input.GetKeyDown(KeyCode.F8))
        //    StartEvent8();
        //else if (Input.GetKeyDown(KeyCode.F9))
        //    StartEvent9();
        //else if (Input.GetKeyDown(KeyCode.F10))
        //    StartEvent10();
        //else if (Input.GetKeyDown(KeyCode.F11))
        //    StartEvent11();
    }

    /// <summary>
    /// Ÿ���� �ڵ鷯
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
    /// ���� �� �ؽ�Ʈ ���̰� �ϱ�
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowNextLine()
    {
        if (currentLine >= textLines.Count - 1)
        {
            // �̺�Ʈ�� �÷��� false ó�� �� ���� �ִϸ��̼�, ���̵� �ƿ� ó��
            #region �̺�Ʈ
            if (isEvent01)
            {
                isEvent01 = false;

                // �÷��̾ �ٽ� �̵��� �� �ְ�
                if (playerController != null)
                {
                    playerController.canMove = true;
                }

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

                // �÷��̾ �ٽ� �̵��� �� �ְ�
                if (playerController != null)
                {
                    playerController.canMove = true;
                }

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
            #endregion �̺�Ʈ

            #region
            // ���� �̺�Ʈ ���� �÷���
            if (istalkleEvent02)
            {
                istalkleEvent02 = false;
                yield return EndEventTalkle_Routine();
            }

            if (istalkleEvent03)
            {
                istalkleEvent03 = false;
                yield return EndEventTalkle_Routine();
            }

            if (istalkleEvent04)
            {
                istalkleEvent04 = false;
                yield return EndEventTalkle_Routine();
            }

            if (istalkleEvent05)
            {
                istalkleEvent05 = false;
                yield return EndEventTalkle_Routine();
            }

            if (istalkleEvent06)
            {
                istalkleEvent06 = false;
                yield return EndEventTalkle_Routine();
            }

            if (istalkleEvent07)
            {
                istalkleEvent07 = false;
                yield return EndEventTalkle_Routine();
            }

            if (istalkleEvent08)
            {
                istalkleEvent08 = false;
                yield return EndEventTalkle_Routine();
            }

            if (istalkleEvent09)
            {
                istalkleEvent09 = false;
                yield return EndEventTalkle_Routine();
            }

            if (istalkleEvent10)
            {
                istalkleEvent10 = false;
                yield return EndEventTalkle_Routine();
            }

            if (istalkleEvent11)
            {
                istalkleEvent11 = false;
                yield return EndEventTalkle_Routine();
            }

            #endregion

            // �÷��̾ �ٽ� �̵��� �� �ְ�
            if (playerController != null)
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
    /// �ؽ�Ʈ�� Ÿ���� �Ǵ� ȿ��ó�� ���̴� �ڷ�ƾ
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

    #region �̺�Ʈ
    #region �̺�Ʈ 1 ������ �ƽ� ����
    public void StartEvent1()
    {
        textLines = event01_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(StartEvent1WithDelay());
    }

    private IEnumerator StartEvent1WithDelay()
    {
        yield return new WaitForSeconds(1f);  // 1�� ���

        canvasGroup.alpha = 1;
        currentLine = -1;

        isEvent01 = true;
        StartCoroutine(ShowNextLine());
    }
    IEnumerator EndEvent1_Routine()
    {
        // ������ �������� Ʈ��
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // �˾� ���̵�ƿ�
        canvasGroup.DOFade(0, 1f);
        speechText.text = "";

        // 5�� �� ���� �̺�Ʈ ����
        Invoke("WaitEvent2", 5f);
    }

    void WaitEvent2()
    {
        CreateMinigameObject(tutorialEventObject_01, spawnPoint[0].position, 1);

        PlayRandomEventTalkle_02();
    }

    // 02 _ �����÷����� ���� �̺�Ʈ ����
    void PlayRandomEventTalkle_02()
    {
        textLines = miniGameStart01_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(PlayRandomEventTalkleDelay_02());
    }

    private IEnumerator PlayRandomEventTalkleDelay_02()
    {
        yield return new WaitForSeconds(1f);  // 1�� ���

        canvasGroup.alpha = 1;
        currentLine = -1;

        istalkleEvent02 = true;
        StartCoroutine(ShowNextLine());
    }
    #endregion

    #region �̺�Ʈ 2 �����÷����� ����
    public void StartEvent2()
    {
        // ������Ʈ �����
        // RespawnEventObject(1);

        textLines = event02_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(StartEvent2WithDelay());
    }

    private IEnumerator StartEvent2WithDelay()
    {
        yield return new WaitForSeconds(1f);  // 1�� ���

        canvasGroup.alpha = 1;
        currentLine = -1;

        isEvent02 = true;
        StartCoroutine(ShowNextLine());
    }

    IEnumerator EndEvent2_Routine()
    {
        // ������ �������� Ʈ��
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // �˾� ���̵�ƿ�
        canvasGroup.DOFade(0, 1f);
        speechText.text = "";

        // Ʃ�丮���� ��� ���������� 10�� �Ŀ� Ʃ�丮�� ����
        if (AllTutorialsCleared()) { Invoke(nameof(StartEvent11), 10f); }

        // �ƴϸ� 5�� �� ���� �̺�Ʈ ����
        else { Invoke("WaitEvent3", 5f); }
    }

    void WaitEvent3()
    {
        CreateMinigameObject(tutorialEventObject_02, spawnPoint[1].position, 1);

        PlayRandomEventTalkle_03();
    }

    // 03 _ �밴 ����� �̺�Ʈ ����
    void PlayRandomEventTalkle_03()
    {
        textLines = miniGameStart02_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(PlayRandomEventTalkleDelay_03());
    }

    private IEnumerator PlayRandomEventTalkleDelay_03()
    {
        yield return new WaitForSeconds(1f);  // 1�� ���

        canvasGroup.alpha = 1;
        currentLine = -1;

        istalkleEvent03 = true;
        StartCoroutine(ShowNextLine());
    }
    #endregion

    #region �̺�Ʈ 3 �밴 �����
    public void StartEvent3()
    {
        // ������Ʈ �����
        // RespawnEventObject(2);

        textLines = event03_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(StartEvent3WithDelay());
    }

    private IEnumerator StartEvent3WithDelay()
    {
        yield return new WaitForSeconds(1f);  // 1�� ���

        canvasGroup.alpha = 1;
        currentLine = -1;

        isEvent03 = true;
        StartCoroutine(ShowNextLine());
    }

    IEnumerator EndEvent3_Routine()
    {
        // ������ �������� Ʈ��
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // �˾� ���̵�ƿ�
        canvasGroup.DOFade(0, 1f);
        speechText.text = "";

        // Ʃ�丮���� ��� ���������� 10�� �Ŀ� Ʃ�丮�� ����
        if (AllTutorialsCleared()) { Invoke(nameof(StartEvent11), 10f); }

        // 5�� �� ���� �̺�Ʈ ����
        else { Invoke("WaitEvent4", 5f); }
    }

    void WaitEvent4()
    {
        CreateMinigameObject(tutorialEventObject_03, spawnPoint[2].position, 1);

        PlayRandomEventTalkle_04();
    }

    // 04 _ ���� �� ���� �̺�Ʈ ����
    void PlayRandomEventTalkle_04()
    {
        textLines = miniGameStart03_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(PlayRandomEventTalkleDelay_04());
    }

    private IEnumerator PlayRandomEventTalkleDelay_04()
    {
        yield return new WaitForSeconds(1f);  // 1�� ���

        canvasGroup.alpha = 1;
        currentLine = -1;

        istalkleEvent04 = true;
        StartCoroutine(ShowNextLine());
    }
    #endregion

    #region �̺�Ʈ 4 ���� �� ����
    public void StartEvent4()
    {
        // ������Ʈ �����
        // RespawnEventObject(3);

        textLines = event04_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(StartEvent4WithDelay());
    }

    private IEnumerator StartEvent4WithDelay()
    {
        yield return new WaitForSeconds(1f);  // 1�� ���

        canvasGroup.alpha = 1;
        currentLine = -1;

        isEvent04 = true;
        StartCoroutine(ShowNextLine());
    }

    IEnumerator EndEvent4_Routine()
    {
        // ������ �������� Ʈ��
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // �˾� ���̵�ƿ�
        canvasGroup.DOFade(0, 1f);
        speechText.text = "";

        // Ʃ�丮���� ��� ���������� 10�� �Ŀ� Ʃ�丮�� ����
        if (AllTutorialsCleared()) { Invoke(nameof(StartEvent11), 10f); }

        // 5�� �� ���� �̺�Ʈ ����
        else { Invoke("WaitEvent5", 5f); }
    }

    void WaitEvent5()
    {
        CreateMinigameObject(tutorialEventObject_04, spawnPoint[3].position, 1);

        PlayRandomEventTalkle_05();
    }

    // 05 _ ���� �ȳ� �̺�Ʈ ����
    void PlayRandomEventTalkle_05()
    {
        textLines = miniGameStart04_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(PlayRandomEventTalkleDelay_05());
    }

    private IEnumerator PlayRandomEventTalkleDelay_05()
    {
        yield return new WaitForSeconds(1f);  // 1�� ���

        canvasGroup.alpha = 1;
        currentLine = -1;

        istalkleEvent05 = true;
        StartCoroutine(ShowNextLine());
    }
    #endregion

    #region �̺�Ʈ 5 ���� �ȳ�
    public void StartEvent5()
    {
        // ������Ʈ �����
        // RespawnEventObject(4);

        textLines = event05_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(StartEvent5WithDelay());
    }

    private IEnumerator StartEvent5WithDelay()
    {
        yield return new WaitForSeconds(1f);  // 1�� ���

        canvasGroup.alpha = 1;
        currentLine = -1;

        isEvent05 = true;
        StartCoroutine(ShowNextLine());
    }

    IEnumerator EndEvent5_Routine()
    {
        // ������ �������� Ʈ��
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // �˾� ���̵�ƿ�
        canvasGroup.DOFade(0, 1f);
        speechText.text = "";

        // Ʃ�丮���� ��� ���������� 10�� �Ŀ� Ʃ�丮�� ����
        if (AllTutorialsCleared()) { Invoke(nameof(StartEvent11), 10f); }

        // 5�� �� ���� �̺�Ʈ ����
        else { Invoke("WaitEvent6", 5f); }
    }

    void WaitEvent6()
    {
        CreateMinigameObject(tutorialEventObject_05, spawnPoint[4].position, 1);

        PlayRandomEventTalkle_06();
    }

    // 06 _ ���������� �̺�Ʈ ����
    void PlayRandomEventTalkle_06()
    {
        textLines = miniGameStart05_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(PlayRandomEventTalkleDelay_06());
    }

    private IEnumerator PlayRandomEventTalkleDelay_06()
    {
        yield return new WaitForSeconds(1f);  // 1�� ���

        canvasGroup.alpha = 1;
        currentLine = -1;

        istalkleEvent06 = true;
        StartCoroutine(ShowNextLine());
    }
    #endregion

    #region �̺�Ʈ 6 ���������� ����
    public void StartEvent6()
    {
        // ������Ʈ �����
        // RespawnEventObject(5);

        textLines = event06_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(StartEvent6WithDelay());
    }

    private IEnumerator StartEvent6WithDelay()
    {
        yield return new WaitForSeconds(1f);  // 1�� ���

        canvasGroup.alpha = 1;
        currentLine = -1;

        isEvent06 = true;
        StartCoroutine(ShowNextLine());
    }

    IEnumerator EndEvent6_Routine()
    {
        // ������ �������� Ʈ��
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // �˾� ���̵�ƿ�
        canvasGroup.DOFade(0, 1f);
        speechText.text = "";

        // Ʃ�丮���� ��� ���������� 10�� �Ŀ� Ʃ�丮�� ����
        if (AllTutorialsCleared()) { Invoke(nameof(StartEvent11), 10f); }

        // 5�� �� ���� �̺�Ʈ ����
        else { Invoke("WaitEvent8", 5f); }
    }

    void WaitEvent8()
    {
        CreateMinigameObject(tutorialEventObject_06, spawnPoint[5].position, 2);

        PlayRandomEventTalkle_08();
    }

    // 08 _ �°� �߶� ���� �̺�Ʈ ����
    void PlayRandomEventTalkle_08()
    {
        textLines = miniGameStart06_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(PlayRandomEventTalkleDelay_08());
    }

    private IEnumerator PlayRandomEventTalkleDelay_08()
    {
        yield return new WaitForSeconds(1f);  // 1�� ���

        canvasGroup.alpha = 1;
        currentLine = -1;

        istalkleEvent08 = true;
        StartCoroutine(ShowNextLine());
    }


    #endregion

    #region �̺�Ʈ 7 �нǹ�
    public void StartEvent7()
    {
        textLines = event07_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(StartEvent7WithDelay());
    }

    private IEnumerator StartEvent7WithDelay()
    {
        yield return new WaitForSeconds(1f);  // 1�� ���

        canvasGroup.alpha = 1;
        currentLine = -1;

        isEvent07 = true;
        StartCoroutine(ShowNextLine());
    }

    IEnumerator EndEvent7_Routine()
    {
        // ������ �������� Ʈ��
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // �˾� ���̵�ƿ�
        canvasGroup.DOFade(0, 1f);
        speechText.text = "";
    }
    #endregion

    #region �̺�Ʈ 8 �°� �߶� ����
    public void StartEvent8()
    {
        // ������Ʈ �����
        // RespawnEventObject(6);

        textLines = event08_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(StartEvent8WithDelay());
    }

    private IEnumerator StartEvent8WithDelay()
    {
        yield return new WaitForSeconds(1f);  // 1�� ���

        canvasGroup.alpha = 1;
        currentLine = -1;

        isEvent08 = true;
        StartCoroutine(ShowNextLine());
    }

    IEnumerator EndEvent8_Routine()
    {
        // ������ �������� Ʈ��
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // �˾� ���̵�ƿ�
        canvasGroup.DOFade(0, 1f);
        speechText.text = "";

        // Ʃ�丮���� ��� ���������� 10�� �Ŀ� Ʃ�丮�� ����
        if (AllTutorialsCleared()) { Invoke(nameof(StartEvent11), 10f); }

        // 5�� �� ���� �̺�Ʈ ����
        else { Invoke("WaitEvent9", 5f); }
    }

    void WaitEvent9()
    {
        CreateMinigameObject(tutorialEventObject_07, spawnPoint[6].position, 2);

        PlayRandomEventTalkle_09();
    }

    // 09 _ ���帶�� �̺�Ʈ ����
    void PlayRandomEventTalkle_09()
    {
        textLines = miniGameStart07_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(PlayRandomEventTalkleDelay_09());
    }

    private IEnumerator PlayRandomEventTalkleDelay_09()
    {
        yield return new WaitForSeconds(1f);  // 1�� ���

        canvasGroup.alpha = 1;
        currentLine = -1;

        istalkleEvent09 = true;
        StartCoroutine(ShowNextLine());
    }
    #endregion

    #region �̺�Ʈ 9 ���帶��
    public void StartEvent9()
    {
        // ������Ʈ �����
        // RespawnEventObject(7);

        textLines = event09_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(StartEvent9WithDelay());
    }

    private IEnumerator StartEvent9WithDelay()
    {
        yield return new WaitForSeconds(1f);  // 1�� ���

        canvasGroup.alpha = 1;
        currentLine = -1;

        isEvent09 = true;
        StartCoroutine(ShowNextLine());
    }

    IEnumerator EndEvent9_Routine()
    {
        // ������ �������� Ʈ��
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // �˾� ���̵�ƿ�
        canvasGroup.DOFade(0, 1f);
        speechText.text = "";

        // Ʃ�丮���� ��� ���������� 10�� �Ŀ� Ʃ�丮�� ����
        if (AllTutorialsCleared()) { Invoke(nameof(StartEvent11), 10f); }

        // 5�� �� ���� �̺�Ʈ ����
        else { Invoke("WaitEvent10", 5f); }
    }

    void WaitEvent10()
    {
        CreateMinigameObject(tutorialEventObject_08, spawnPoint[7].position, 2);

        PlayRandomEventTalkle_10();
    }

    // 09 _ ���帶�� �̺�Ʈ ����
    void PlayRandomEventTalkle_10()
    {
        textLines = miniGameStart08_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(PlayRandomEventTalkleDelay_10());
    }

    private IEnumerator PlayRandomEventTalkleDelay_10()
    {
        yield return new WaitForSeconds(1f);  // 1�� ���

        canvasGroup.alpha = 1;
        currentLine = -1;

        istalkleEvent10 = true;
        StartCoroutine(ShowNextLine());
    }
    #endregion

    #region �̺�Ʈ 10 ���� �밴 �����
    public void StartEvent10()
    {
        // ������Ʈ �����
        // RespawnEventObject(8);

        textLines = event10_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(StartEvent10WithDelay());
    }

    private IEnumerator StartEvent10WithDelay()
    {
        yield return new WaitForSeconds(1f);  // 1�� ���

        canvasGroup.alpha = 1;
        currentLine = -1;

        isEvent10 = true;
        StartCoroutine(ShowNextLine());
    }

    IEnumerator EndEvent10_Routine()
    {
        // ������ �������� Ʈ��
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // �˾� ���̵�ƿ�
        canvasGroup.DOFade(0, 1f);
        speechText.text = "";

        // Ʃ�丮���� ��� ���������� 10�� �Ŀ� Ʃ�丮�� ����
        if (AllTutorialsCleared()) { Invoke(nameof(StartEvent11), 10f); }
    }
    #endregion

    #region �̺�Ʈ 11 Ʃ�丮�� ��
    public void StartEvent11()
    {
        textLines = event11_Lines;
        t_talkleEffect.MoveUp();
        StartCoroutine(StartEvent11WithDelay());
    }

    private IEnumerator StartEvent11WithDelay()
    {
        yield return new WaitForSeconds(1f);  // 1�� ���

        canvasGroup.alpha = 1;
        currentLine = -1;

        isEvent11 = true;
        StartCoroutine(ShowNextLine());
    }

    IEnumerator EndEvent11_Routine()
    {
        // ������ �������� Ʈ��
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // �˾� ���̵�ƿ�
        canvasGroup.DOFade(0, 1f);
        speechText.text = "";

        // ���̵� �ƿ� ������
        if (fadeController != null)
        {
            playerController.canMove = false;
            fadeController.DirectEndingFade(true);
        }
    }
    #endregion
    #endregion

    IEnumerator EndEventTalkle_Routine()
    {
        // ������ �������� Ʈ��
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // �˾� ���̵�ƿ�
        canvasGroup.DOFade(0, 1f);
        speechText.text = "";
    }

    // --------------------------------------------------

    /// <summary>
    /// Ʃ�丮�� �̴ϰ��� ����
    /// </summary>
    /// <param name="_eventObject">�̴ϰ��� ����</param>
    /// <param name="_eventPosition">������ġ</param>
    /// <param name="tutorialIndex">Ʃ�丮�� ��ȣ</param>
    private void CreateMinigameObject(RandomEventObject _eventObject, Vector3 _eventPosition, int tutorialIndex)
    {
        GameObject eventObject = Instantiate(_eventObject.gameObject, _eventPosition, Quaternion.identity);
        if (eventObject.TryGetComponent<RandomEventObject>(out RandomEventObject randomEvent))
        {
            createdEventList.Add(randomEvent);

            // �ݹ� (��ȣ�ۿ� ���� / ����) ���
            randomEvent.onEventSuccess += OnRandomEventSuccess;
            randomEvent.onEventFailed += OnRandomEventInteractFailed;

            // �̴ϰ��� ������ ����
            randomEvent.ReferTaskManager(taskManager);

            // ȭ��ǥ ���� (Ʃ�丮���� ȭ��ǥ ���� X)
            eventDirectionArrow.CreateArrow(randomEvent, tutorialIndex);

            // ������ ������Ʈ�� ���� ����Ʈ
            createdObjects.Add(eventObject);
        }
    }

    // ���� ���߻�Ȳ ��ȣ�ۿ� ���� ����
    private void OnRandomEventSuccess(RandomEventObject successEvent)
    {
        Debug.Log("�̺�Ʈ ��ȣ�ۿ� ���� ���� : " + successEvent.name);

        // ��� ����
        if (successEvent.task == KindOfTask.TurningKey)
        {
            StartEvent2();
            tutorialClears[0] = true;
        }
        else if (successEvent.task == KindOfTask.ArrowMatch)
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

        // �̺�Ʈ ���� (���� ����)
        successEvent.onEventSuccess -= OnRandomEventSuccess;
        successEvent.onEventFailed -= OnRandomEventInteractFailed;

        createdEventList.Remove(successEvent);
        eventDirectionArrow.RemoveArrow(successEvent);
        Destroy(successEvent.gameObject);
    }

    // ���� ���߻�Ȳ ��ȣ�ۿ� ���� ����
    private void OnRandomEventInteractFailed(RandomEventObject failedEvent)
    {
        Debug.Log("�̺�Ʈ ��ȣ�ۿ� ���� ���� : " + failedEvent.name);

        // �̺�Ʈ ���� (���� ����)
        failedEvent.onEventSuccess -= OnRandomEventSuccess;
        failedEvent.onEventFailed -= OnRandomEventInteractFailed;

        createdEventList.Remove(failedEvent);           // ������ ���߻�Ȳ ������Ʈ ����
        eventDirectionArrow.RemoveArrow(failedEvent);   // �����ϴ� ȭ��ǥ ����
        Destroy(failedEvent.gameObject);                // ������Ʈ �ı�
    }

    // ��� Ʃ�丮���� Ŭ�����ߴ��� üũ�ϴ� �Լ�
    private bool AllTutorialsCleared()
    {
        for (int i = 0; i < tutorialClears.Length; i++)
        {
            if (!tutorialClears[i])
                return false;
        }
        return true;
    }

    //// LEGACY : Ʃ�丮�� ��������� �ʱ�� ��. 2025.07.02 �����
    ///// <summary>
    ///// ���߻�Ȳ Ʃ�丮���� ������ϴ� �ڵ�
    ///// </summary>
    ///// <param name="currentIndex">���� Ʃ�丮�� �ε���</param>
    //private void RespawnEventObject(int currentIndex = 0)
    //{
    //    // ������ ������Ʈ ���� ����Ʈ Ŭ����
    //    foreach (GameObject obj in createdObjects)
    //    {
    //        if (obj != null)
    //            Destroy(obj);
    //    }

    //    createdObjects.Clear();

    //    switch (currentIndex)
    //    {
    //        case 0: // ��ü ������
    //            CreateMinigameObject(tutorialEventObject_01, spawnPoint[0].position, 1);
    //            CreateMinigameObject(tutorialEventObject_02, spawnPoint[1].position, 1);
    //            CreateMinigameObject(tutorialEventObject_03, spawnPoint[2].position, 1);
    //            CreateMinigameObject(tutorialEventObject_04, spawnPoint[3].position, 1);
    //            CreateMinigameObject(tutorialEventObject_05, spawnPoint[4].position, 1);
    //            CreateMinigameObject(tutorialEventObject_06, spawnPoint[5].position, 2);
    //            CreateMinigameObject(tutorialEventObject_07, spawnPoint[6].position, 2);
    //            CreateMinigameObject(tutorialEventObject_08, spawnPoint[7].position, 2);
    //            break;
    //        case 1: // �����÷����� �����ϰ� ������
    //            //CreateMinigameObject(tutorialEventObject_01, spawnPoint[0].position, 1);
    //            CreateMinigameObject(tutorialEventObject_02, spawnPoint[1].position, 1);
    //            CreateMinigameObject(tutorialEventObject_03, spawnPoint[2].position, 1);
    //            CreateMinigameObject(tutorialEventObject_04, spawnPoint[3].position, 1);
    //            CreateMinigameObject(tutorialEventObject_05, spawnPoint[4].position, 1);
    //            CreateMinigameObject(tutorialEventObject_06, spawnPoint[5].position, 2);
    //            CreateMinigameObject(tutorialEventObject_07, spawnPoint[6].position, 2);
    //            CreateMinigameObject(tutorialEventObject_08, spawnPoint[7].position, 2);
    //            break;
    //        case 2: // �밴 ����� �����ϰ� ������
    //            CreateMinigameObject(tutorialEventObject_01, spawnPoint[0].position, 1);
    //            //CreateMinigameObject(tutorialEventObject_02, spawnPoint[1].position, 1);
    //            CreateMinigameObject(tutorialEventObject_03, spawnPoint[2].position, 1);
    //            CreateMinigameObject(tutorialEventObject_04, spawnPoint[3].position, 1);
    //            CreateMinigameObject(tutorialEventObject_05, spawnPoint[4].position, 1);
    //            CreateMinigameObject(tutorialEventObject_06, spawnPoint[5].position, 2);
    //            CreateMinigameObject(tutorialEventObject_07, spawnPoint[6].position, 2);
    //            CreateMinigameObject(tutorialEventObject_08, spawnPoint[7].position, 2);
    //            break;
    //        case 3: // ���� �� ����� �����ϰ� ������
    //            CreateMinigameObject(tutorialEventObject_01, spawnPoint[0].position, 1);
    //            CreateMinigameObject(tutorialEventObject_02, spawnPoint[1].position, 1);
    //            //CreateMinigameObject(tutorialEventObject_03, spawnPoint[2].position, 1);
    //            CreateMinigameObject(tutorialEventObject_04, spawnPoint[3].position, 1);
    //            CreateMinigameObject(tutorialEventObject_05, spawnPoint[4].position, 1);
    //            CreateMinigameObject(tutorialEventObject_06, spawnPoint[5].position, 2);
    //            CreateMinigameObject(tutorialEventObject_07, spawnPoint[6].position, 2);
    //            CreateMinigameObject(tutorialEventObject_08, spawnPoint[7].position, 2);
    //            break;
    //        case 4: // ���� �ȳ� �����ϰ� ������
    //            CreateMinigameObject(tutorialEventObject_01, spawnPoint[0].position, 1);
    //            CreateMinigameObject(tutorialEventObject_02, spawnPoint[1].position, 1);
    //            CreateMinigameObject(tutorialEventObject_03, spawnPoint[2].position, 1);
    //            //CreateMinigameObject(tutorialEventObject_04, spawnPoint[3].position, 1);
    //            CreateMinigameObject(tutorialEventObject_05, spawnPoint[4].position, 1);
    //            CreateMinigameObject(tutorialEventObject_06, spawnPoint[5].position, 2);
    //            CreateMinigameObject(tutorialEventObject_07, spawnPoint[6].position, 2);
    //            CreateMinigameObject(tutorialEventObject_08, spawnPoint[7].position, 2);
    //            break;
    //        case 5: // ���������� ���� �����ϰ� ������
    //            CreateMinigameObject(tutorialEventObject_01, spawnPoint[0].position, 1);
    //            CreateMinigameObject(tutorialEventObject_02, spawnPoint[1].position, 1);
    //            CreateMinigameObject(tutorialEventObject_03, spawnPoint[2].position, 1);
    //            CreateMinigameObject(tutorialEventObject_04, spawnPoint[3].position, 1);
    //            //CreateMinigameObject(tutorialEventObject_05, spawnPoint[4].position, 1);
    //            CreateMinigameObject(tutorialEventObject_06, spawnPoint[5].position, 2);
    //            CreateMinigameObject(tutorialEventObject_07, spawnPoint[6].position, 2);
    //            CreateMinigameObject(tutorialEventObject_08, spawnPoint[7].position, 2);
    //            break;
    //        case 6: // �°� �߶� ���� �����ϰ� ������
    //            CreateMinigameObject(tutorialEventObject_01, spawnPoint[0].position, 1);
    //            CreateMinigameObject(tutorialEventObject_02, spawnPoint[1].position, 1);
    //            CreateMinigameObject(tutorialEventObject_03, spawnPoint[2].position, 1);
    //            CreateMinigameObject(tutorialEventObject_04, spawnPoint[3].position, 1);
    //            CreateMinigameObject(tutorialEventObject_05, spawnPoint[4].position, 1);
    //            //CreateMinigameObject(tutorialEventObject_06, spawnPoint[5].position, 2);
    //            CreateMinigameObject(tutorialEventObject_07, spawnPoint[6].position, 2);
    //            CreateMinigameObject(tutorialEventObject_08, spawnPoint[7].position, 2);
    //            break;
    //        case 7: // ���帶�� �����ϰ� ������
    //            CreateMinigameObject(tutorialEventObject_01, spawnPoint[0].position, 1);
    //            CreateMinigameObject(tutorialEventObject_02, spawnPoint[1].position, 1);
    //            CreateMinigameObject(tutorialEventObject_03, spawnPoint[2].position, 1);
    //            CreateMinigameObject(tutorialEventObject_04, spawnPoint[3].position, 1);
    //            CreateMinigameObject(tutorialEventObject_05, spawnPoint[4].position, 1);
    //            CreateMinigameObject(tutorialEventObject_06, spawnPoint[5].position, 2);
    //            //CreateMinigameObject(tutorialEventObject_07, spawnPoint[6].position, 2);
    //            CreateMinigameObject(tutorialEventObject_08, spawnPoint[7].position, 2);
    //            break;
    //        case 8: // ���� �밴 ����� �����ϰ� ������
    //            CreateMinigameObject(tutorialEventObject_01, spawnPoint[0].position, 1);
    //            CreateMinigameObject(tutorialEventObject_02, spawnPoint[1].position, 1);
    //            CreateMinigameObject(tutorialEventObject_03, spawnPoint[2].position, 1);
    //            CreateMinigameObject(tutorialEventObject_04, spawnPoint[3].position, 1);
    //            CreateMinigameObject(tutorialEventObject_05, spawnPoint[4].position, 1);
    //            CreateMinigameObject(tutorialEventObject_06, spawnPoint[5].position, 2);
    //            CreateMinigameObject(tutorialEventObject_07, spawnPoint[6].position, 2);
    //            //CreateMinigameObject(tutorialEventObject_08, spawnPoint[7].position, 2);
    //            break;
    //    }
    //}
}