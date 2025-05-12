using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [Header("수평 이동 속도")]
    [SerializeField] private float horizontalSpeed = 5f;
    [Header("수직 이동 속도")]
    [SerializeField] private float verticalSpeed = 0.5f;

    [Header("이동 범위 제한")]
    [SerializeField] private float minX = -5f, maxX = 5f;
    [SerializeField] private float minY = -3f, maxY = 3f;

    [Header("몬스터스포너")]
    [SerializeField] private Transform[] spawner;

    private SpriteRenderer spriteRenderer;
    private PlayerAnimator playerAnim;
    private Rigidbody2D rigidbody;
    private int horizontalDirection = 1;
    private bool canMove = true;

    // --------------------------------------------------

    RandomEventObject randomEventObject;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<PlayerAnimator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!canMove) return;

        // New Input System 값 읽기
        float inputX = InputManager.Instance.Horizontal;
        float inputY = InputManager.Instance.Vertical;
        //bool jump = InputManager.Instance.JumpPressed;

        bool horizontalKey = !Mathf.Approximately(inputX, 0f);
        bool verticalKey = !Mathf.Approximately(inputY, 0f);

        // 좌우 방향 전환
        if (horizontalKey)
        {
            horizontalDirection = inputX > 0f ? 1 : -1;
            spriteRenderer.flipX = (horizontalDirection < 0);
        }

        // 수평/수직 이동 로직 (이전과 동일)
        float dx = horizontalSpeed * horizontalDirection;
        if (verticalKey && !horizontalKey) dx = 0f;
        float dy = verticalKey
                 ? inputY * horizontalSpeed * verticalSpeed
                 : 0f;

        // 예: 점프 처리 (간단히 로그)
        //if (jump)
        //    Debug.Log("Jump!");

        // 실제 이동
        Vector3 move = new Vector3(dx, dy, 0f) * Time.deltaTime;
        transform.Translate(move, Space.World);

        // 화면 제한
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;

        // E 키 눌러서 상호작용
        if (Input.GetKeyDown(KeyCode.E) && randomEventObject != null)
        {
            randomEventObject.CompleteInteractEvent(); // 이벤트 실행
            randomEventObject = null; // 참조 초기화
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 delta = Vector3.zero;

        if (other.CompareTag("obstacle"))
        {
            playerAnim.SetStunned(true);
            StartCoroutine(StunRoutine(2f));
            StartCoroutine(PauseMovement(2f));
        }
        if (other.CompareTag("Stairs_up"))// 현재 버그 있음 수정필요
            delta = new Vector3(-1, 5.5f, 0f);
        else if (other.CompareTag("Stairs_down"))
            delta = new Vector3(+1, -5.5f, 0f);

        if (delta != Vector3.zero)
        {
            // 1) 플레이어 이동
            rigidbody.transform.position += delta;

            // 2) 모든 스포너 이동
            foreach (var spawner in spawner)
            {
                // 스포너 오브젝트 자체를 옮길 경우
                spawner.transform.position += delta;

                // 만약 MonsterSpawner 내부에 ShiftSpawnPoints(delta)가 구현되어 있다면
                // spawner.ShiftSpawnPoints(delta);
            }
        }

        if (other.CompareTag("EventObject"))
        {
            randomEventObject = other.GetComponent<RandomEventObject>();
        }
    }
    private IEnumerator StunRoutine(float duration)
    {
        canMove = false;
        yield return new WaitForSeconds(duration);

        // 3) 넉다운 해제
        playerAnim.SetStunned(false);
        canMove = true;
    }
    private IEnumerator PauseMovement(float duration)
    {
        canMove = false;
        yield return new WaitForSeconds(duration);
        canMove = true;
    }
}
