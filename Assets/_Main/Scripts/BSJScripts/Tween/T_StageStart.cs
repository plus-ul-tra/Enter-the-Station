using DG.Tweening;
using UnityEngine;

/// <summary>
/// 스테이지 전환 될 때 나오는 텍스트 트윈입니다.
/// 페이드 인 되면서 위로 떠오르는 느낌
/// </summary>
public class T_StageStart : MonoBehaviour
{
    public GameObject startUI;
    public RectTransform textRect;

    private CanvasGroup uiCanvasGroup;
    private CanvasGroup textCanvasGroup;

    public float fadeDuration = 2f;
    public float moveUpDuration = 2f;
    public float moveUpDistance = 50f;

    private Sequence seq;

    private void Start()
    {
        uiCanvasGroup = startUI.GetComponent<CanvasGroup>();
        if(uiCanvasGroup != null)
        {
            uiCanvasGroup.alpha = 1;
        }

        textCanvasGroup = textRect.GetComponent<CanvasGroup>();
        if (textCanvasGroup != null)
        {
            textCanvasGroup.alpha = 0f;
        }

        // 10초 대기 후 시작 ( 인트로 컷신 8초 )
        Invoke("StartStage", 10f);
    }

    /// <summary>
    /// 스테이지 전환 될 때 나오는 텍스트 트윈을 실행하는 함수
    /// </summary>
    public void StartStage()
    {
        // 스테이지 시작 UI 활성화
        startUI.SetActive(true);

        // 아래로 위치시킨 후 시작
        Vector3 startPos = textRect.anchoredPosition;
        textRect.anchoredPosition = startPos - new Vector3(0, moveUpDistance, 0);

        // 트윈 재생
        seq = DOTween.Sequence();
        seq.Join(textCanvasGroup.DOFade(1f, fadeDuration));
        seq.Join(textRect.DOAnchorPos(startPos, moveUpDuration).SetEase(Ease.OutCubic));
        seq.Append(uiCanvasGroup.DOFade(0f, fadeDuration));
        seq.OnComplete(() => startUI.SetActive(false));
    }

    private void OnDisable()
    {
        seq?.Kill();
        seq = null;
    }
}
