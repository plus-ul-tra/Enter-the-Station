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
            // ������ �ڵ� �߰�
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f; // ó���� �� ���̰�
        canvasGroup.DOFade(1f, fadeDuration).SetEase(Ease.InOutQuad);
    }
}
