using UnityEngine;
using DG.Tweening;

public class T_TalkleEffect : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Vector2 _originalAnchoredPosition;
    private float _moveAmount = 150f; // 올라가는 높이 (UI는 보통 픽셀 기준)
    private float _duration = 0.5f;  // 트윈 시간

    Sequence seq;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _originalAnchoredPosition = _rectTransform.anchoredPosition;
    }

    public void MoveUp()
    {
        seq = DOTween.Sequence();

        // 흔들리는 모션 v02
        // 위로 이동
        seq.Append(
            _rectTransform.DOAnchorPosY(_originalAnchoredPosition.y + _moveAmount, _duration)
                          .SetEase(Ease.InExpo)
        );

        // 0.25초 대기
        seq.AppendInterval(0.25f);

        // 회전 흔들기 (X축 -30 <-> 30 count번 반복)
        int count = 10;
        for (int i = 0; i < count; i++)
        {
            seq.Append(_rectTransform.DOLocalRotate(new Vector3(0f, 0f, -5f), 0.05f)
                                     .SetEase(Ease.InOutSine));
            seq.Append(_rectTransform.DOLocalRotate(new Vector3(0f, 0f, 5f), 0.05f)
                                     .SetEase(Ease.InOutSine));
        }

        // 마지막엔 다시 0도로 회복
        seq.Append(_rectTransform.DOLocalRotate(Vector3.zero, 0.1f)
                                 .SetEase(Ease.InOutSine));

        // LEGACY : 흔들리는 모션 v01
        //// 위로 이동
        //seq.Append(
        //    _rectTransform.DOAnchorPosY(_originalAnchoredPosition.y + _moveAmount, _duration)
        //              .SetEase(Ease.InExpo)
        //    );

        //// 흔들림
        //seq.Append(
        //    _rectTransform.DOShakeAnchorPos(
        //        duration: 1.5f,     // 흔들리는 시간
        //        strength: new Vector2(20f, 10f), // 좌우로 10픽셀 정도 흔들림
        //        vibrato: 20,        // 진동 횟수
        //        randomness: 90,     // 진동의 불규칙 정도
        //        snapping: false,
        //        fadeOut: true
        //        )
        //    );

    }

    public void MoveDown()
    {
        _rectTransform.DOAnchorPosY(_originalAnchoredPosition.y, _duration)
                      .SetEase(Ease.InExpo);
    }
}
