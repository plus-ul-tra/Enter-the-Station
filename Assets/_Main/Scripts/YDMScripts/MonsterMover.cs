using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MonsterMover : MonoBehaviour
{

    [Tooltip("이동 속도 (절대값)")]
    public float speed = 2f;

    // 이동 방향: -1 = 왼쪽, +1 = 오른쪽
    private int direction = 1;
    private SpriteRenderer spriteRenderer;

    [Header("파괴 경계 (절대 X값)")]
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
        // 1) 이동
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime, Space.World);

        // 2) 경계 체크 및 파괴
        if (Mathf.Abs(transform.position.x) >= destroyBoundX)
        {
            Destroy(gameObject);
        }
    }


    public void SetDirection(int dir)
    {
        direction = dir >= 0 ? 1 : -1;
        // 방향에 따라 스프라이트 뒤집기
        spriteRenderer.flipX = (direction > 0);
    }
}
