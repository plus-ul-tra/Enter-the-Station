using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DropAndFade : MonoBehaviour
{
    [Header("이동할 거리 (Y축 아래)")]
    [SerializeField] private float dropDistance = 500f;
    [Header("이동 및 페이드에 걸릴 시간 (초)")]
    [SerializeField] private float duration = 2f;
    [Header("끝나면 오브젝트 파괴 여부")]
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

            // 위치 보간
            transform.position = Vector3.Lerp(startPos, endPos, t);
            // 투명도 보간
            float alpha = Mathf.Lerp(startColor.a, 0f, t);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

            yield return null;
        }

        // 정확하게 끝값 적용
        transform.position = endPos;
        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, 0f);

        if (destroyOnComplete)
            Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }
}
