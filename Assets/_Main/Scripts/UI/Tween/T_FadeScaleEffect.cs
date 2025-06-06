using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 이미지가 커지면서 페이드 아웃 되는 트윈입니다.
/// </summary>
public class T_FadeScaleEffect : MonoBehaviour
{
    public Image targetImage;       // 대상 이미지
    public float duration = 1.0f;   // 애니메이션 시간
    public float targetScale = 2f;  // 최종 스케일 배수

    private Sequence seq;

    private void Start()
    {
        PlayEffect();
    }

    public void PlayEffect()
    {
        // 이미지 초기 상태 설정
        targetImage.color = new Color(1, 1, 1, 1); // 완전 불투명
        targetImage.transform.localScale = Vector3.one;

        // 동시에 스케일업 + 페이드아웃
        seq = DOTween.Sequence();

        seq.Join(targetImage.transform.DOScale(targetScale, duration).SetEase(Ease.OutQuad));
        seq.Join(targetImage.DOFade(0f, duration).SetEase(Ease.InQuad));

        // 끝난 뒤에 비활성화하고 싶다면
        seq.OnComplete(() => Destroy(this.gameObject));
    }

    private void OnDisable()
    {
        // 트윈 정리
        seq?.Kill();
        seq = null;
    }
}
