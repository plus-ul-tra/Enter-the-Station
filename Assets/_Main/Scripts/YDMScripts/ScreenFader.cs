using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance { get; private set; }

    private Image fadeImage;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        fadeImage = GetComponent<Image>();
        // 시작 시 완전 투명
        var col = fadeImage.color;
        col.a = 0f;
        fadeImage.color = col;
    }

    /// <summary>
    /// 페이드아웃 → action → optional 대기 → 페이드인
    /// </summary>
    /// <param name="action">페이드 중 실행할 콜백</param>
    /// <param name="fadeOutDuration">페이드아웃에 걸릴 시간(초)</param>
    /// <param name="fadeInDuration">페이드인에 걸릴 시간(초)</param>
    /// <param name="holdDuration">action 후 대기 시간(초)</param>
    public IEnumerator FadeTeleport(Action action,
                                     float fadeOutDuration,
                                     float fadeInDuration,
                                     float holdDuration = 0f)
    {
        // 1) 페이드아웃
        yield return StartCoroutine(Fade(0f, 1f, fadeOutDuration));

        // 2) 액션 실행
        action?.Invoke();

        // 3) 대기
        if (holdDuration > 0f)
            yield return new WaitForSeconds(holdDuration);

        // 4) 페이드인
        yield return StartCoroutine(Fade(1f, 0f, fadeInDuration));
    }

    /// <summary>
    /// from 알파 → to 알파로 duration 초 동안 선형 보간
    /// </summary>
    public IEnumerator Fade(float from, float to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float a = Mathf.Lerp(from, to, elapsed / duration);
            var col = fadeImage.color;
            col.a = a;
            fadeImage.color = col;
            yield return null;
        }
        // 최종 보정
        var finalCol = fadeImage.color;
        finalCol.a = to;
        fadeImage.color = finalCol;
    }
}
