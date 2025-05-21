using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class UpDownMove : MonoBehaviour
{
    [Header("�ε��� �� �̸� �Ǵ� ���� �ε���")]
    [Tooltip("�� �̸����� �ε��Ϸ��� Scene Name��, �ε����� �ε��Ϸ��� Use Index üũ �� Scene Index�� ���� �Է��ϼ���.")]
    [SerializeField] private string sceneName;
    [Header("������ �Ÿ�")]
    [SerializeField] private float distance = 1100f;
    [Header("�̵��� �ɸ� �ð�(��)")]
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

        // ��Ȯ�� ��ǥ ��ġ�� ���缭 ������
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

        // ��Ȯ�� ��ǥ ��ġ�� ���缭 ������
        transform.position = endPos;
    }
}
