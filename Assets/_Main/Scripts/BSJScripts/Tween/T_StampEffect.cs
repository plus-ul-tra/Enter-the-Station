using UnityEngine;
using DG.Tweening;
using UnityEngine.UI; // CanvasGroup 사용 시 필요

public class T_StampEffect : MonoBehaviour
{
    // 시작 스케일
    public Vector3 startScale = new Vector3(4f, 4f, 4f);          
    public float duration = 0.4f;       // 전체 애니메이션 시간

    private RectTransform target;       // 도장 효과를 줄 UI 오브젝트

    private void Awake()
    {
        target = this.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        PlayStamp();
    }

    public void PlayStamp()
    {
        // 초기화
        target.localScale = startScale;

        // 두트윈 시퀀스 애니메이션
        Sequence seq = DOTween.Sequence();

        seq.Append(target.DOScale(0.8f, duration * 0.6f).SetEase(Ease.OutQuad));
        seq.Append(target.DOScale(1.0f, duration * 0.4f).SetEase(Ease.OutBack));
    }
}
