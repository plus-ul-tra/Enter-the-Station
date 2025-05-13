using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MonsterMover : MonoBehaviour
{

    [Tooltip("�̵� �ӵ� (���밪)")]
    public float speed = 2f;

    // �̵� ����: -1 = ����, +1 = ������
    private int direction = 1;
    private SpriteRenderer spriteRenderer;

    [Header("�ı� ��� (���� X��)")]
    public float destroyBoundX = 10f;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        var container = GameObject.FindWithTag("MonsterContainer");
        if (container != null)
        {
            transform.SetParent(container.transform, worldPositionStays: true);
        }
    }

    void Update()
    {
        // 1) �̵�
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime, Space.World);

        // 2) ��� üũ �� �ı�
        if (Mathf.Abs(transform.position.x) >= destroyBoundX)
        {
            Destroy(gameObject);
        }
    }


    public void SetDirection(int dir)
    {
        direction = dir >= 0 ? 1 : -1;
        // ���⿡ ���� ��������Ʈ ������
        spriteRenderer.flipX = (direction > 0);
    }
}
