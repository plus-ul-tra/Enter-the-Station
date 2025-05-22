using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;
public class UpDownMove : MonoBehaviour
{
    [Header("������ �Ÿ�")]
    [SerializeField] private float distance = 1100f;
    [Header("�̵��� �ɸ� �ð�(��)")]
    [SerializeField] private float duration = 2f;

    [SerializeField] private bool down;
    [SerializeField] private bool up;

    [SerializeField] private CanvasGroup canvasGroup;

    public IEnumerator MoveDown()
    {
        canvasGroup.alpha = 1f;

        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.down * distance;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        // ��Ȯ�� ��ǥ ��ġ�� ���缭 ������
        transform.position = endPos;
        canvasGroup.DOFade(0f, 1f);
    }
    public IEnumerator MoveUp()
    {
        canvasGroup.alpha = 1f;

        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.up * distance;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        // ��Ȯ�� ��ǥ ��ġ�� ���缭 ������
        transform.position = endPos;
        canvasGroup.DOFade(0f, 1f);
    }
}
