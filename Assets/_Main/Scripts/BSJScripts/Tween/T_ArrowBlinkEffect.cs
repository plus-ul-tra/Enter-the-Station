using UnityEngine;
using DG.Tweening;
using System.Collections;

/// <summary>
/// ȭ��ǥ�� ���� ������ �����Ÿ����ϴ� Ʈ��
/// </summary>
public class T_ArrowBlinkEffect : MonoBehaviour
{
    [SerializeField] private float totalBlinkDuration = 7f;         // �������� ���ӵǴ� �� �ð�
    [SerializeField] private float blinkStartTime = 8f;             // Start �� �� �� �ڿ� ������ ��������
    [SerializeField] private float startInterval = 0.2f;            // ������ ���� ����
    [SerializeField] private float endInterval = 0.05f;             // ������ ������ ����(���� ������)

    private SpriteRenderer spriteRenderer;

    // --------------------------------------------------

    private Coroutine blinkCoroutine;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer�� �ʿ��մϴ�!");
        }
    }

    private void Start()
    {
        // blinkStartTime �� �Ŀ� ������ ��ƾ ����
        DOVirtual.DelayedCall(blinkStartTime, () => {
            blinkCoroutine = StartCoroutine(BlinkRoutine());
        });
    }

    /// <summary>
    /// �������� ���� ������ �ϸ� �ݺ��ϴ� ��ƾ
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

        // �������� �ٽ� ���̰�
        SetAlpha(1f);
    }

    /// <summary>
    /// ���� ������ �������� ���ĸ� ����
    /// </summary>
    private void ToggleAlpha()
    {
        float targetAlpha = spriteRenderer.color.a > 0.5f ? 0.2f : 1f;
        spriteRenderer.DOFade(targetAlpha, 0.05f); // ª�� fade�� ����ϰ�
    }

    /// <summary>
    /// ���� ���� ���� ����
    /// </summary>
    private void SetAlpha(float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

    // �ڷ�ƾ ����
    private void OnDisable()
    {
        // �ڷ�ƾ ����
        if(blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }

        // ���� �ʱ�ȭ
        SetAlpha(1);
    }
}
