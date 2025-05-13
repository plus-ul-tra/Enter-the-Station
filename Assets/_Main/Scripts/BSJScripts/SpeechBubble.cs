using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Text;

public class SpeechBubble : MonoBehaviour
{
    public GameObject popUpObject;
    public TMP_Text tmpText;
    public RectTransform imageRect;

    public int punchCount = 7; // 몇 번 펀칭할지
    public float punchInterval = 0.5f; // 펀칭 간격
    public float punchPower = 0.3f;

    private CanvasGroup canvasGroup;
    private StringBuilder sb;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        sb = new StringBuilder();
    }

    void Start()
    {
        canvasGroup.alpha = 0;
    }

    /// <summary>
    /// 무전기 말풍선을 실행시키는 함수
    /// </summary>
    public void PlaySpeechBubble()
    {
        SetSpeechBubbleText();
        tmpText.text = sb.ToString();

        StartCoroutine(PunchMultipleTimes());
    }

    void SetSpeechBubbleText()
    {
        sb.Clear();
        sb.Append("2번 출구 방향\n엘리베이터 고장!");
    }

    IEnumerator PunchMultipleTimes()
    {
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

        // 팝업 오브젝트 페이드아웃
        canvasGroup.DOFade(0, 1f);
    }
}
