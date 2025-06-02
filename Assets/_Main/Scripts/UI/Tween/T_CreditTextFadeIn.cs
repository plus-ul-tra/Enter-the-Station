using DG.Tweening;
using UnityEngine;

public class T_CreditTextFadeIn : MonoBehaviour
{
    public float fadeDuration = 1f;

    private void OnEnable()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            // 없으면 자동 추가
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f; // 처음엔 안 보이게
        canvasGroup.DOFade(1f, fadeDuration).SetEase(Ease.InOutQuad);
    }
}
