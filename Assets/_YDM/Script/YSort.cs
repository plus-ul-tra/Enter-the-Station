using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class YSort : MonoBehaviour
{
    [Tooltip("���� ũ�� �� ����(��)�� �׷����ϴ�.")]
    public int offset = 0;

    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        // y ��ǥ�� �������� sortingOrder �� Ŀ���� �տ� �׷���
        sr.sortingOrder = -(int)(transform.position.y * 100) + offset;
    }
}
