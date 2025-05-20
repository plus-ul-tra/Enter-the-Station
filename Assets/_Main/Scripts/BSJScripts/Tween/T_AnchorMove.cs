using UnityEngine;
using DG.Tweening;

public class T_AnchorMove : MonoBehaviour
{
    public Vector2 targetPosition; // 인스펙터에서 설정할 위치
    public float duration = 1f;  // 움직이는 시간

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    [ContextMenu("Move To Target Position")]
    public void MoveToTargetPosition()
    {
        if (rectTransform != null)
        {
            // 위치 이동
            rectTransform.DOAnchorPos(targetPosition, duration).SetEase(Ease.OutQuad);

            // 페이드 인 (알파값 0 → 1)
            canvasGroup.alpha = 0f;
            canvasGroup.DOFade(1f, duration).SetEase(Ease.OutQuad);
        }
    }
}
