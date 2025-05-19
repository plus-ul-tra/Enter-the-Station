using UnityEngine;
using DG.Tweening;

public class T_TalkleEffect : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Vector2 _originalAnchoredPosition;
    private float _moveAmount = 150f; // �ö󰡴� ���� (UI�� ���� �ȼ� ����)
    private float _duration = 0.5f;  // Ʈ�� �ð�

    Sequence seq;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _originalAnchoredPosition = _rectTransform.anchoredPosition;
    }

    public void MoveUp()
    {
        seq = DOTween.Sequence();

        // ��鸮�� ��� v02
        // ���� �̵�
        seq.Append(
            _rectTransform.DOAnchorPosY(_originalAnchoredPosition.y + _moveAmount, _duration)
                          .SetEase(Ease.InExpo)
        );

        // 0.25�� ���
        seq.AppendInterval(0.25f);

        // ȸ�� ���� (X�� -30 <-> 30 count�� �ݺ�)
        int count = 10;
        for (int i = 0; i < count; i++)
        {
            seq.Append(_rectTransform.DOLocalRotate(new Vector3(0f, 0f, -5f), 0.05f)
                                     .SetEase(Ease.InOutSine));
            seq.Append(_rectTransform.DOLocalRotate(new Vector3(0f, 0f, 5f), 0.05f)
                                     .SetEase(Ease.InOutSine));
        }

        // �������� �ٽ� 0���� ȸ��
        seq.Append(_rectTransform.DOLocalRotate(Vector3.zero, 0.1f)
                                 .SetEase(Ease.InOutSine));

        // LEGACY : ��鸮�� ��� v01
        //// ���� �̵�
        //seq.Append(
        //    _rectTransform.DOAnchorPosY(_originalAnchoredPosition.y + _moveAmount, _duration)
        //              .SetEase(Ease.InExpo)
        //    );

        //// ��鸲
        //seq.Append(
        //    _rectTransform.DOShakeAnchorPos(
        //        duration: 1.5f,     // ��鸮�� �ð�
        //        strength: new Vector2(20f, 10f), // �¿�� 10�ȼ� ���� ��鸲
        //        vibrato: 20,        // ���� Ƚ��
        //        randomness: 90,     // ������ �ұ�Ģ ����
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
