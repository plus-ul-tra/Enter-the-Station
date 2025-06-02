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
        // 충돌한 상대가 Wall 이거나 Stairs_down 이면 부모 오브젝트 파괴
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
        //        pos.y += 0.1f*dir;  // 원하는 만큼 오프셋
        //        parent.position = pos;
        //    }
        //}
    }
}
