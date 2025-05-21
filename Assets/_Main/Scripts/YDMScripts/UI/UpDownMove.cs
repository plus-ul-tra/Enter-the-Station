using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class UpDownMove : MonoBehaviour
{
    [Header("내려갈 거리")]
    [SerializeField] private float distance = 1100f;
    [Header("이동에 걸릴 시간(초)")]
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

        // 정확히 목표 위치에 맞춰서 끝맺음
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

        // 정확히 목표 위치에 맞춰서 끝맺음
        transform.position = endPos;
    }
}
