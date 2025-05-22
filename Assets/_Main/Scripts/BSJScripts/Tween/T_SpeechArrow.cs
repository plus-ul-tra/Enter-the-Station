using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class T_SpeechArrow : MonoBehaviour
{
    private RectTransform rectTransform;
    public float moveDistance = 5f; // ���Ʒ� �̵� ����
    public float duration = 1f; // �ֱ�

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        MoveVertically();
    }

    void MoveVertically()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + moveDistance, duration).SetEase(Ease.InOutSine));
        sequence.Append(rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y - moveDistance, duration).SetEase(Ease.InOutSine));
        sequence.SetLoops(-1, LoopType.Yoyo); // ���� �ݺ�
    }
}
