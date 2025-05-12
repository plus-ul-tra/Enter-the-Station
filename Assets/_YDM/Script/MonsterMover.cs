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
    [Tooltip("X좌표가 이 값을 벗어나면 몬스터를 파괴합니다.")]
    public float destroyBoundX = 10f;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    /// <summary>
    /// 외부에서 호출해서 방향을 지정합니다.
    /// </summary>
    /// <param name="dir">-1: 왼쪽, +1: 오른쪽</param>
    public void SetDirection(int dir)
    {
        direction = dir >= 0 ? 1 : -1;
        // 방향에 따라 스프라이트 뒤집기
        spriteRenderer.flipX = (direction > 0);
    }
}
