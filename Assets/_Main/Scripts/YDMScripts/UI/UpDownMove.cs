using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class UpDownMove : MonoBehaviour
{
    [Header("������ �Ÿ�")]
    [SerializeField] private float distance = 1100f;
    [Header("�̵��� �ɸ� �ð�(��)")]
    [SerializeField] private float duration = 2f;

    [SerializeField] private bool down;
    [SerializeField] private bool up;

    public IEnumerator MoveDown()
    {
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
    }
    public IEnumerator MoveUp()
    {
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
    }
}
