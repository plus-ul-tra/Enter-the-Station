using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Text;
using System.Collections.Generic;

public enum SpeechKey
{
    FALL,
    FALL_FAIL,
    SLEEP,
    SLEEP_FAIL,
    ELV,
    ELV_FAIL,
    CS,
    CS_FAIL,
    MAP,
    MAP_FAIL,
    ESC,
    ESC_FAIL,
    HEART,
    HEART_FAIL,
    CSSLEEP,
    CSSLEEP_FAIL,
    SUCCESS,
    SUPPLICATE,
}

public class SpeechBubble : MonoBehaviour
{
    [Header("팝업 오브젝트")]
    public GameObject popUpObject;

    [Header("텍스트")]
    public TMP_Text tmpText;

    [Header("이미지 (말풍선)")]
    public RectTransform imageRect;

    public int punchCount = 7; // 몇 번 펀칭할지
    public float punchInterval = 0.5f; // 펀칭 간격
    public float punchPower = 0.3f;

    private CanvasGroup canvasGroup;
    private StringBuilder sb;

    // 스크립트 참조
    [Header("무전기 트윈 이펙트")]
    [SerializeField] private T_TalkleEffect t_talkleEffect;
    private WaitForSeconds speechWaitTime = new WaitForSeconds(0.5f);    // 말하기 까지 대기시간

    // 화살표 말풍선
    private List<string> arrowNotice_Lines;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        sb = new StringBuilder();
    }

    void Start()
    {
        canvasGroup.alpha = 0;

        // 화살표 대사 초기화
        arrowNotice_Lines = new List<string>
        {
            "화살표를 따라가면\n네 할일이 보여",
            "빨간색 화살표는 지하 1층,",
            "노란색은 지하 2층이니\n명심해둬",
            "이제 할일을 하러 가볼까?"
        };
    }

    /// <summary>
    /// 화살표 말풍선을 실행시키는 함수
    /// </summary>
    public void PlayArrowNotice()
    {
        StopAllCoroutines(); // 기존 코루틴 정지
        StartCoroutine(DisplayMessagesOneByOne());
    }

    /// <summary>
    /// 무전기 말풍선을 실행시키는 함수
    /// </summary>
    public void PlaySpeechBubble(SpeechKey speechKey, int zoneIndex = 1)
    {
        SetSpeechBubbleText(speechKey, zoneIndex);
        tmpText.text = sb.ToString();

        StartCoroutine(PunchMultipleTimes());
    }

    void SetSpeechBubbleText(SpeechKey speechKey, int zoneIndex = 1)
    {
        int randomSpeechIndex = 0;
        sb.Clear();

        switch (zoneIndex)
        {
            case 1:
            case 2:
            case 3:
                sb.AppendLine("\"지하 1층\"");
                break;
            case 4:
            case 5:
            case 6:
                sb.AppendLine("\"지하 2층\"");
                break;
        }

        switch (speechKey)
        {
            case SpeechKey.FALL:
                sb.Append("\"승객 추락 발생!\"");
                break;
            case SpeechKey.FALL_FAIL:
                randomSpeechIndex = Random.Range(0, 3);
                switch (randomSpeechIndex)
                {
                    case 0:
                        sb.Append("\"너 죽고싶어?\"");
                        break;
                    case 1:
                        sb.Append("\"우리 다 짤릴 뻔했어!\"");
                        break;
                    case 2:
                        sb.Append("\"미쳤어?\"");
                        break;
                }
                break;
            case SpeechKey.SLEEP:
                sb.Append("\"취객 발생!\"");
                break;
            case SpeechKey.SLEEP_FAIL:
                randomSpeechIndex = Random.Range(0, 4);
                switch (randomSpeechIndex)
                {
                    case 0:
                        sb.Append("\"깨우는 거 맞냐? 자장가 부르냐?\"");
                        break;
                    case 1:
                        sb.Append("\"그 사람이랑 같이 퇴근 할거야?\"");
                        break;
                    case 2:
                        sb.Append("\"자장 자장 해주는 거냐?\"");
                        break;
                    case 3:
                        sb.Append("\"아주 같이 집까지 가주지 그래?\"");
                        break;
                }
                break;
            case SpeechKey.ELV:
                sb.Append("\"엘리베이터 고장 발생!\"");
                break;
            case SpeechKey.ELV_FAIL:
                randomSpeechIndex = Random.Range(0, 3);
                switch (randomSpeechIndex)
                {
                    case 0:
                        sb.Append("\"전선 좀 잇는 게 그렇게 어렵냐?\"");
                        break;
                    case 1:
                        sb.Append("\"전보다 더 못했는데?\"");
                        break;
                    case 2:
                        sb.Append("\"그거 잇다 감전되겠다!\"");
                        break;
                }
                break;
            case SpeechKey.CS:
                sb.Append("\"진상 고객 발생!\"");
                break;
            case SpeechKey.CS_FAIL:
                randomSpeechIndex = Random.Range(0, 4);
                switch (randomSpeechIndex)
                {
                    case 0:
                        sb.Append("\"그깟 진상한테 기 죽었어? 뭐가 무서워?");
                        break;
                    case 1:
                        sb.Append("\"졌어? 승객이 널 제압하더라.\"");
                        break;
                    case 2:
                        sb.Append("\"내가 대신 가서 혼내줘야해? 이건 연습이 아니야.\"");
                        break;
                    case 3:
                        sb.Append("\"에이, 그냥 호구 나셨네\"");
                        break;
                }
                break;
            case SpeechKey.MAP:
                sb.Append("\"승객 분이 길 물어보려고 부르잖냐!\"");
                break;
            case SpeechKey.MAP_FAIL:
                randomSpeechIndex = Random.Range(0, 2);
                switch (randomSpeechIndex)
                {
                    case 0:
                        sb.Append("\"길을 알려준 거야, 미궁에 던진 거야?");
                        break;
                    case 1:
                        sb.Append("\"알려주는 꼴을 보니 저 분은 다시 역으로 돌아올 것 같은데?\"");
                        break;
                }
                break;
            case SpeechKey.ESC:
                sb.Append("\"에스컬레이터 고장 발생\"");
                break;
            case SpeechKey.ESC_FAIL:
                sb.Append("\"그 열쇠로 문 여는 줄 알았네. 왜 계속 돌려?\"");
                break;
            case SpeechKey.HEART:
                sb.Append("\"응급 상황 발생!\"");
                break;
            case SpeechKey.HEART_FAIL:
                sb.Append("\"그 상태로 놔두면 넌 구급차에 같이 실려가.\"");
                break;
            case SpeechKey.CSSLEEP:
                sb.Append("\"진상 취객 발생!\"");
                break;
            case SpeechKey.CSSLEEP_FAIL:
                randomSpeechIndex = Random.Range(0, 2);
                switch (randomSpeechIndex)
                {
                    case 0:
                        sb.Append("\"이것보다 더한 진상들은 어떻게 처리하려는거야?");
                        break;
                    case 1:
                        sb.Append("\"너한테 기대한 내가 바보지.\"");
                        break;
                }
                break;
            case SpeechKey.SUCCESS:
                randomSpeechIndex = Random.Range(0, 3);
                switch (randomSpeechIndex)
                {
                    case 0:
                        sb.Append("\"이번엔 꽤 빠르더라. 깜짝 놀랐어. 아주 약간.");
                        break;
                    case 1:
                        sb.Append("\"그래, 지금까지 후임 중 네가 그나마 낫네.\"");
                        break;
                    case 2:
                        sb.Append("\"그래, 이번엔 눈빛 하나는 좋았어.\"");
                        break;
                }
                break;
            case SpeechKey.SUPPLICATE:
                randomSpeechIndex = Random.Range(0, 3);
                switch (randomSpeechIndex)
                {
                    case 0:
                        sb.Append("\"기세로 밀어붙여! 유리멘탈 말고!");
                        break;
                    case 1:
                        sb.Append("\"언제쯤 끝날까~ 오늘 안엔 가능해?\"");
                        break;
                    case 2:
                        sb.Append("\"상황 발생이라고! 뭘 망설여, 당장 움직여!\"");
                        break;
                }
                break;
            default:
                sb.Append("\"그래, 이번엔 눈빛 하나는 좋았어.\"");
                break;
        }
    }

    IEnumerator DisplayMessagesOneByOne()
    {
        // 무전기 올라오는 트윈
        t_talkleEffect.MoveUp();
        yield return speechWaitTime;

        // 캔버스 그룹 초기화
        canvasGroup.alpha = 1;

        // 이미지 한 번만 흔들기
        if (imageRect != null)
        {
            imageRect.DOKill();
            imageRect.DOShakeAnchorPos(
                1.2f,
                strength: new Vector2(20f, 20f),
                vibrato: 10,
                randomness: 90f,
                snapping: false,
                fadeOut: true
            );
        }

        // 대화 한 줄씩 출력
        foreach (string message in arrowNotice_Lines)
        {
            tmpText.text = message;

            tmpText.transform.DOKill();
            tmpText.transform.localScale = Vector3.one;
            tmpText.transform.DOPunchScale(Vector3.one * punchPower, 0.2f, 1, 0.1f);

            yield return new WaitForSeconds(2f);
        }

        // 무전기 내려가는 트윈
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // 팝업 오브젝트 페이드아웃
        canvasGroup.DOFade(0, 1f);
    }

    IEnumerator PunchMultipleTimes()
    {
        // 무전기 올라오는 트윈
        t_talkleEffect.MoveUp();
        yield return speechWaitTime;

        // 캔버스 그룹 초기화
        canvasGroup.alpha = 1;

        // 이미지 한 번만 흔들기
        if (imageRect != null)
        {
            imageRect.DOKill();
            imageRect.DOShakeAnchorPos(
                1.2f,
                strength: new Vector2(20f, 20f),
                vibrato: 10,
                randomness: 90f,
                snapping: false,
                fadeOut: true
            );
        }

        // 텍스트 펀칭
        tmpText.transform.localScale = Vector3.one;

        for (int i = 0; i < punchCount; i++)
        {
            tmpText.transform.DOKill();
            tmpText.transform.localScale = Vector3.one;
            tmpText.transform.DOPunchScale(Vector3.one * punchPower, 0.2f, 1, 0.1f);

            yield return new WaitForSeconds(punchInterval);
        }

        // 무전기 내려가는 트윈
        t_talkleEffect.MoveDown();
        yield return speechWaitTime;

        // 팝업 오브젝트 페이드아웃
        canvasGroup.DOFade(0, 1f);
    }
}
