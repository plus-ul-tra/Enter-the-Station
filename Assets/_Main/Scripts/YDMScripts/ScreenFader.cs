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
        // ���� �� ���� ���� ����
        var col = fadeImage.color;
        col.a = 0f;
        fadeImage.color = col;
    }

    /// ���̵�ƿ� �� �׼� �� ���̵��� ������ �����մϴ�.
    public IEnumerator FadeTeleport(System.Action action, float fadeDuration = 2f)
    {
        // 1) ���̵�ƿ�
        yield return StartCoroutine(Fade(0f, 1f, fadeDuration));

        // 2) �׼� (�ڷ���Ʈ, ������ �̵�, ī�޶� warp ��)
        action?.Invoke();

        // 3) ��¦ ��� (�ɼ�)
        yield return new WaitForSeconds(0.1f);

        // 4) ���̵���
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
        // ����
        var finalCol = fadeImage.color;
        finalCol.a = to;
        fadeImage.color = finalCol;
    }
}
