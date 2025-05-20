using UnityEngine;
using DG.Tweening;

public class T_AnchorMove : MonoBehaviour
{
    public Vector2 targetPosition; // �ν����Ϳ��� ������ ��ġ
    public float duration = 1f;  // �����̴� �ð�

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
            // ��ġ �̵�
            rectTransform.DOAnchorPos(targetPosition, duration).SetEase(Ease.OutQuad);

            // ���̵� �� (���İ� 0 �� 1)
            canvasGroup.alpha = 0f;
            canvasGroup.DOFade(1f, duration).SetEase(Ease.OutQuad);
        }
    }
}
