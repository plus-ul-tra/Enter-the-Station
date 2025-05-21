using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
using System.Xml;
using DG.Tweening.Core.Easing;

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

    private int currentLine = -1;

    private Coroutine typingCoroutine;
    private bool isTyping = false;

    // ----------------------------------------

    private FadeController fadeController;

    // ----------------------------------------

    // Ʃ�丮���� �ѹ��̶� Ŭ�����ߴ°�?
    private bool[] tutorialClears = { false, false, false, false, false, false, false, false};

    void Start()
    {
        // ������ �ƽ� ����
        event01_Lines = new List<string>
        {
            "�ʰ� ���ο� �����̱���.",
            "�츮 ����\n���� ��Ȳ�� ���� ����ŵ�?",
            "������ �� ���� ���ٰž�.",
            "�ذ� �����ؼ� �ο� ������\n�հ� ���Ѵ�~"
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
            "������ ������ ���缭 �� ������ ��.\n���� ��.",
        };

        // Ʃ�丮�� �̴ϰ��� 4  (���� �ȳ�)
        event05_Lines = new List<string>
        {
            "������ �����ؼ�\n���� ���� �������� �̾�.",
            "�������� ����� �ٸ��ž�.",
        };

        // Ʃ�丮�� �̴ϰ��� 5  (���������� ����)
        event06_Lines = new List<string>
        {
            "����? ���� ���缭 �մ� �ž�.\n������ �𸣰ھ�?",
        };

        // ���ǹ� ȹ��
        event07_Lines = new List<string>
        {
            "�� ��,",
            "���� ���� ���ǹ���\n������ ��찡 �־�.",
            "�߰��ϸ� �����Ƿ� ������~",
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
            "�� �տ� �޷ȴ�.",
        };

        // Ʃ�丮�� �̴ϰ��� 9  (���� �밴 �����)
        event10_Lines = new List<string>
        {
            "���� �밴���� \n�����ϸ� ���� �Ŵ�.",
            "���� Ÿ�ֿ̹� ���缭\n ��ư�� ����",
        };

        // Ʃ�丮�� ����
        event11_Lines = new List<string>
        {
            "���� ��. ���Ϻ��� �����.",
            "�ٹ��ð���\n12�ú��� 0�ñ����� ������.",
            "���Ϻ��� �Ͽ� �ð����ѵ� �ɷ��־�.\n�����ϰ� ���°� ������?",
        };

        fadeController = GetComponent<FadeController>();
        canvasGroup.alpha = 0;

        // ������Ʈ ��ü ����
        // Ʃ�丮�� 1 ����
        CreateMinigameObject(tutorialEventObject_01, spawnPoint[0].position, 1);
        CreateMinigameObject(tutorialEventObject_02, spawnPoint[1].position, 1);
        CreateMinigameObject(tutorialEventObject_03, spawnPoint[2].position, 1);
        CreateMinigameObject(tutorialEventObject_04, spawnPoint[3].position, 1);
        CreateMinigameObject(tutorialEventObject_05, spawnPoint[4].position, 1);
        CreateMinigameObject(tutorialEventObject_06, spawnPoint[5].position, 2);
        CreateMinigameObject(tutorialEventObject_07, spawnPoint[6].position, 2);
        CreateMinigameObject(tutorialEventObject_08, spawnPoint[7].position, 2);

        // 10�� �� �̺�Ʈ1 ����
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

        // �׽�Ʈ ( Ʃ�丮�� �̴ϰ��� ��ǲ ���� ������ �ݹ� �޾Ƽ� ���� �̺�Ʈ ������Ѿ��� )
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

            // �÷��̾ �ٽ� �̵��� �� �ְ�
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
    }
    #endregion

    #region �̺�Ʈ 2 �����÷����� ����
    public void StartEvent2()
    {
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
    }
    #endregion

    #region �̺�Ʈ 3 �밴 �����
    public void StartEvent3()
    {
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
    }
    #endregion

    #region �̺�Ʈ 4 ���� �� ����
    public void StartEvent4()
    {
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
    }
    #endregion

    #region �̺�Ʈ 5 ���� �ȳ�
    public void StartEvent5()
    {
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
    }
    #endregion

    #region �̺�Ʈ 6 ���������� ����
    public void StartEvent6()
    {
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
    }
    #endregion

    #region �̺�Ʈ 7 ���ǹ�
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
    }
    #endregion

    #region �̺�Ʈ 9 ���帶��
    public void StartEvent9()
    {
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
    }
    #endregion

    #region �̺�Ʈ 10 ���� �밴 �����
    public void StartEvent10()
    {
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
        if(AllTutorialsCleared()) { Invoke(nameof(StartEvent11), 10f); }
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
            fadeController.DirectEndingFade(false);
        }
    }
    #endregion
    #endregion

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
            // eventDirectionArrow.CreateArrow(randomEvent, tutorialIndex);
        }
    }

    // ���� ���߻�Ȳ ��ȣ�ۿ� ���� ����
    private void OnRandomEventSuccess(RandomEventObject successEvent)
    {
        Debug.Log("�̺�Ʈ ��ȣ�ۿ� ���� ���� : " + successEvent.name);

        // ��� ����
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
}