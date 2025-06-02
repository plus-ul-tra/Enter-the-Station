using UnityEngine;
using DG.Tweening;
using System.Collections;

/// <summary>
/// 화살표를 점점 빠르게 깜빡거리게하는 트윈
/// </summary>
public class T_ArrowBlinkEffect : MonoBehaviour
{
    [SerializeField] private float totalBlinkDuration = 7f;         // 깜빡임이 지속되는 총 시간
    [SerializeField] private float blinkStartTime = 8f;             // Start 후 몇 초 뒤에 깜박임 시작할지
    [SerializeField] private float startInterval = 0.2f;            // 깜빡임 시작 간격
    [SerializeField] private float endInterval = 0.05f;             // 깜빡임 마지막 간격(점점 빨라짐)

    private SpriteRenderer spriteRenderer;

    // --------------------------------------------------

    private Coroutine blinkCoroutine;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer가 필요합니다!");
        }
    }

    private void Start()
    {
        // blinkStartTime 초 후에 깜빡임 루틴 시작
        DOVirtual.DelayedCall(blinkStartTime, () => {
            blinkCoroutine = StartCoroutine(BlinkRoutine());
        });
    }

    /// <summary>
    /// 깜빡임을 점점 빠르게 하며 반복하는 루틴
    /// </summary>
    private IEnumerator BlinkRoutine()
    {
        float elapsed = 0f;

        while (elapsed < totalBlinkDuration)
        {
            float t = elapsed / totalBlinkDuration;
            float interval = Mathf.Lerp(startInterval, endInterval, t);

            ToggleAlpha();
            yield return new WaitForSeconds(interval);

            elapsed += interval;
        }

        // 마지막엔 다시 보이게
        SetAlpha(1f);
    }

    /// <summary>
    /// 현재 투명도를 기준으로 알파를 반전
    /// </summary>
    private void ToggleAlpha()
    {
        float targetAlpha = spriteRenderer.color.a > 0.5f ? 0.2f : 1f;
        spriteRenderer.DOFade(targetAlpha, 0.05f); // 짧은 fade로 깔끔하게
    }

    /// <summary>
    /// 알파 값을 직접 설정
    /// </summary>
    private void SetAlpha(float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

    // 코루틴 정리
    private void OnDisable()
    {
        // 코루틴 정리
        if(blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }

        // 알파 초기화
        SetAlpha(1);
    }
}
