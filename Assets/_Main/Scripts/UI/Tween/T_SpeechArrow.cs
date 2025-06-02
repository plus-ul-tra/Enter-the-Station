using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class T_SpeechArrow : MonoBehaviour
{
    private RectTransform rectTransform;
    public float moveDistance = 5f; // 위아래 이동 간격
    public float duration = 1f; // 주기

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
        sequence.SetLoops(-1, LoopType.Yoyo); // 무한 반복
    }
}
