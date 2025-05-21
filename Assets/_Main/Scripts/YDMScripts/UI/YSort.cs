using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class YSort : MonoBehaviour
{
    [Tooltip("값이 크면 더 앞쪽(위)에 그려집니다.")]
    public int offset = 0;

    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        // y 좌표가 낮을수록 sortingOrder 가 커져서 앞에 그려짐
        sr.sortingOrder = -(int)(transform.position.y * 100) + offset;
    }
}
