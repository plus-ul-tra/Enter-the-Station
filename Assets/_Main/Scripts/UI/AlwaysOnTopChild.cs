using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AlwaysOnTopChild : MonoBehaviour
{
    [Tooltip("�θ𺸴� �󸶳� �� ���� �׸���")]
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

        // �θ� order �о�ͼ� +offset
        _sr.sortingOrder = _parentSr.sortingOrder + orderOffset;
    }
}
