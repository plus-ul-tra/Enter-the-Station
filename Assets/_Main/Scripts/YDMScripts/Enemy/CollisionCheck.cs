using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    private Transform parentTf;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        parentTf = transform.parent;
    }

    

    private void OnCollisionEnter2D(Collision2D other)
    {
        // �浹�� ��밡 Wall �̰ų� Stairs_down �̸� �θ� ������Ʈ �ı�
        if (other.collider.CompareTag("Wall") ||
            other.collider.CompareTag("Stairs_down") ||
            other.collider.CompareTag("Stairs_up"))
        {
            Destroy(transform.parent.gameObject);
        }

        //if (other.collider.CompareTag(gameObject.tag))
        //{
        //    Transform parent = transform.parent;
        //    if (parent != null)
        //    {
        //        Vector3 pos = parent.position;
        //        pos.y += 0.1f*dir;  // ���ϴ� ��ŭ ������
        //        parent.position = pos;
        //    }
        //}
    }
}
