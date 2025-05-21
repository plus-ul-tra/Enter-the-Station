using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MonsterMover : MonoBehaviour
{

    [Tooltip("�̵� �ӵ� (���밪)")]
    public float speed = 2f;

    // �̵� ����: -1 = ����, +1 = ������
    public int direction = 1;
    private SpriteRenderer spriteRenderer;


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


    }


    public void SetDirection(int dir)
    {
        direction = dir >= 0 ? 1 : -1;
        // ���⿡ ���� ��������Ʈ ������
        spriteRenderer.flipX = (direction < 0);
    }
}
