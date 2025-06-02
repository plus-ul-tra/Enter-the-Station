using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AlwaysOnTopChild : MonoBehaviour
{
    [Tooltip("부모보다 얼마나 더 위에 그릴지")]
    [SerializeField] private int orderOffset = 1;

    private SpriteRenderer _sr;
    private SpriteRenderer _parentSr;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        if (transform.parent != null)
            _parentSr = transform.parent.GetComponentInChildren<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (_parentSr == null) return;

        // 부모 order 읽어와서 +offset
        _sr.sortingOrder = _parentSr.sortingOrder + orderOffset;
    }
}
