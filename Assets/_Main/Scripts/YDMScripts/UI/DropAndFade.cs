using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DropAndFade : MonoBehaviour
{
    [Header("�̵��� �Ÿ� (Y�� �Ʒ�)")]
    [SerializeField] private float dropDistance = 500f;
    [Header("�̵� �� ���̵忡 �ɸ� �ð� (��)")]
    [SerializeField] private float duration = 2f;
    [Header("������ ������Ʈ �ı� ����")]
    [SerializeField] private bool destroyOnComplete = true;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(MoveDownAndFade());
    }

    private IEnumerator MoveDownAndFade()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.down * dropDistance;
        Color startColor = spriteRenderer.color;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            // ��ġ ����
            transform.position = Vector3.Lerp(startPos, endPos, t);
            // ���� ����
            float alpha = Mathf.Lerp(startColor.a, 0f, t);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

            yield return null;
        }

        // ��Ȯ�ϰ� ���� ����
        transform.position = endPos;
        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, 0f);

        if (destroyOnComplete)
            Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }
}
