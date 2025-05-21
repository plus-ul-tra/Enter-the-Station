using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class UpDownMove : MonoBehaviour
{
    [Header("로드할 씬 이름 또는 빌드 인덱스")]
    [Tooltip("씬 이름으로 로드하려면 Scene Name에, 인덱스로 로드하려면 Use Index 체크 후 Scene Index에 값을 입력하세요.")]
    [SerializeField] private string sceneName;
    [Header("내려갈 거리")]
    [SerializeField] private float distance = 1100f;
    [Header("이동에 걸릴 시간(초)")]
    [SerializeField] private float duration = 2f;

    [SerializeField] private bool down;
    [SerializeField] private bool up;
    private void Start()
    {
        if(up)
        {
            StartCoroutine(MoveUp());
        }
        if (down)
        {
            StartCoroutine(MoveDown());
        }
    }
    private IEnumerator MoveDown()
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
        SceneManager.LoadScene(sceneName);
    }
    private IEnumerator MoveUp()
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
