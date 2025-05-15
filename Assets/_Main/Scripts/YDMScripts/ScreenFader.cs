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
        // 시작 시 완전 투명 상태
        var col = fadeImage.color;
        col.a = 0f;
        fadeImage.color = col;
    }

    /// 페이드아웃 → 액션 → 페이드인 순으로 실행합니다.
    public IEnumerator FadeTeleport(System.Action action, float fadeDuration = 2f)
    {
        // 1) 페이드아웃
        yield return StartCoroutine(Fade(0f, 1f, fadeDuration));

        // 2) 액션 (텔레포트, 스포너 이동, 카메라 warp 등)
        action?.Invoke();

        // 3) 살짝 대기 (옵션)
        yield return new WaitForSeconds(0.1f);

        // 4) 페이드인
        yield return StartCoroutine(Fade(1f, 0f, fadeDuration));
    }

    public IEnumerator Fade(float from, float to, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(from, to, t / duration);
            var col = fadeImage.color;
            col.a = a;
            fadeImage.color = col;
            yield return null;
        }
        // 보정
        var finalCol = fadeImage.color;
        finalCol.a = to;
        fadeImage.color = finalCol;
    }
}
