using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class T_ButtonHoverScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleFactor = 1.2f;
    public float duration = 0.2f;

    private Vector3 originalScale;
    private Tween currentTween;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySFX("UIButton_hover");

        currentTween?.Kill(); // 이전 트윈 중지
        currentTween = transform
            .DOScale(originalScale * scaleFactor, duration)
            .SetEase(Ease.OutBack)
            .SetUpdate(true);   // Time.timeScale 무시
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        currentTween?.Kill();
        currentTween = transform
            .DOScale(originalScale, duration)
            .SetEase(Ease.OutBack)
            .SetUpdate(true);   // Time.timeScale 무시
    }

    private void OnDisable()
    {
        currentTween.Kill();
    }
}
