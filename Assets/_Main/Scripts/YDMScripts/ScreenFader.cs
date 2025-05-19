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
        // ���� �� ���� ����
        var col = fadeImage.color;
        col.a = 0f;
        fadeImage.color = col;
    }

    /// <summary>
    /// ���̵�ƿ� �� action �� optional ��� �� ���̵���
    /// </summary>
    /// <param name="action">���̵� �� ������ �ݹ�</param>
    /// <param name="fadeOutDuration">���̵�ƿ��� �ɸ� �ð�(��)</param>
    /// <param name="fadeInDuration">���̵��ο� �ɸ� �ð�(��)</param>
    /// <param name="holdDuration">action �� ��� �ð�(��)</param>
    public IEnumerator FadeTeleport(Action action,
                                     float fadeOutDuration,
                                     float fadeInDuration,
                                     float holdDuration = 0f)
    {
        // 1) ���̵�ƿ�
        yield return StartCoroutine(Fade(0f, 1f, fadeOutDuration));

        // 2) �׼� ����
        action?.Invoke();

        // 3) ���
        if (holdDuration > 0f)
            yield return new WaitForSeconds(holdDuration);

        // 4) ���̵���
        yield return StartCoroutine(Fade(1f, 0f, fadeInDuration));
    }

    /// <summary>
    /// from ���� �� to ���ķ� duration �� ���� ���� ����
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
        // ���� ����
        var finalCol = fadeImage.color;
        finalCol.a = to;
        fadeImage.color = finalCol;
    }
}
