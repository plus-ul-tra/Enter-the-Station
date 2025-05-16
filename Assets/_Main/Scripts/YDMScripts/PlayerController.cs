using UnityEngine;
using System.Collections;
using Unity.Cinemachine;
using DG.Tweening.Core.Easing;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [Header("수평 이동 속도")]
    [SerializeField] private float horizontalSpeed = 5f;
    [Header("수직 이동 속도")]
    [SerializeField] private float verticalSpeed = 0.5f;

    [Header("몬스터스포너")]
    [SerializeField] private Transform[] spawner;

    [Header("몬스터 컨테이너")]
    [SerializeField] private Transform monsterContainer;

    [Header("Cinemachine 가상 카메라")]
    [SerializeField] private CinemachineCamera cinemachineCam;
    [Header("위층(Up)으로 올라갈 때 사용할 카메라 제한 콜라이더")]
    [SerializeField] private PolygonCollider2D upZoneCollider;
    [Header("아래층(Down)으로 내려갈 때 사용할 카메라 제한 콜라이더")]
    [SerializeField] private PolygonCollider2D downZoneCollider;

    [Header("올라가는 계단")]
    [SerializeField] GameObject stairs_Up;
    [Header("내려가는 계단")]
    [SerializeField] GameObject stairs_Down;

    [Header("옵션 창")]
    [SerializeField] private GameObject optionPanel;

    private SpriteRenderer spriteRenderer;
    private PlayerAnimator playerAnim;
    //private Rigidbody2D rigidbody;
    private int horizontalDirection = 1;
    [HideInInspector]public bool canMove = true;
    private CinemachineCameraClamp cameraClamp;
    //private TaskManager taskManager;
    [SerializeField] private IntroCameraSwitcher introCameraSwitcher;
    // --------------------------------------------------
    RandomEventObject randomEventObject;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();//플립시 사용
        playerAnim = GetComponent<PlayerAnimator>();
        cameraClamp = cinemachineCam.GetComponent<CinemachineCameraClamp>();        
        //rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        StartCoroutine(PauseMovement(introCameraSwitcher.introDuration+2));
    }
    void Update()
    {
        if (InputManager.Instance.PausePressed)
        {
            Debug.Log("GameController에서 Pause 감지!");
            //if (optionPanel == null) return;
            optionPanel.SetActive(true);
            canMove = true;
        }

        // E 키 눌러서 상호작용
        if (Input.GetKeyDown(KeyCode.E) && randomEventObject != null)
        {
            playerAnim.SetMoved(false);

            randomEventObject.CompleteInteractEvent(); // 이벤트 실행
            

            // TODO :
            switch (randomEventObject.task)
            {
                case KindOfTask.FixWire://선 잇기
                    // TODO : FixWire 애니메이션 변경
                    playerAnim.SetSitting(true);
                    break;

                case KindOfTask.ArrowMatch://화살표 맞추기
                    // TODO : ArrowMatch 애니메이션 변경
                    playerAnim.SetFight(true);
                    break;

                case KindOfTask.MaintainingGauge://게이지 유지
                    // TODO : MaintainingGauge 애니메이션 변경
                    playerAnim.SetWork(true);
                    break;

                case KindOfTask.MovingCircle:
                    // TODO : MovingCircle 애니메이션 변경
                    playerAnim.SetFight(true);
                    break;

                case KindOfTask.RythmGauge:
                    // TODO : RythmGauge 애니메이션 변경
                    playerAnim.SetFight(true);
                    break;

                case KindOfTask.StackingGauge:
                    // TODO : StackingGauge 애니메이션 변경
                    playerAnim.SetWork(true);
                    break;

                case KindOfTask.MapGuide:
                    // TODO : MapGuide 애니메이션 변경
                    playerAnim.SetMap(true);
                    break;

                //default:
                //    // TODO : 기타 처리
                //    break;
            }

            randomEventObject = null; // 참조 초기화
        }
    }
    private void FixedUpdate()
    {
        if (!canMove) return;
        

        Vector2 input = ReadInput();//키입력 받음
        ApplyMovement(input);//이동 로직
    }
    private Vector2 ReadInput()//키입력
    {
        return new Vector2(
            InputManager.Instance.Horizontal,
            InputManager.Instance.Vertical
        );
    }
    private void ApplyMovement(Vector2 input)//이동로직
    {
        if (!Mathf.Approximately(input.x, 0f))
        {
            horizontalDirection = input.x > 0f ? 1 : -1;
            spriteRenderer.flipX = horizontalDirection < 0;
        }

        // 2) 원시 이동 벡터 만들기 (Y축에만 verticalSpeed 반영)
        Vector2 raw = new Vector2(
            input.x,
            input.y * verticalSpeed
        );

        // 3) 방향 벡터 정규화: (0,0)일 땐 Normalize 하지 않음
        if (raw.sqrMagnitude > 1f)
            raw.Normalize();

        if(!Mathf.Approximately(input.x, 0f) || !Mathf.Approximately(input.y, 0f))
            playerAnim.SetMoved(true);
        else
            playerAnim.SetMoved(false);


        // 4) 실제 이동: 방향(normalized) * 수평 속도 * deltaTime
        Vector3 move = new Vector3(raw.x, raw.y, 0f)
                         * horizontalSpeed
                         * Time.deltaTime;
        transform.Translate(move, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 delta = Vector3.zero;

        if (other.CompareTag("obstacle"))//승객충돌확인
        {
            HandleObstacleCollision();
        }
        if (other.CompareTag("Stairs_up") || other.CompareTag("Stairs_down"))//계단충돌확인
        {
            bool isUp = other.CompareTag("Stairs_up");
            HandleStairsCollision(isUp);
        }

        if (other.CompareTag("EventObject"))
        {
            randomEventObject = other.GetComponent<RandomEventObject>();
        }
    }
    private void HandleObstacleCollision()
    {
        playerAnim.SetStunned(true);
        StartCoroutine(StunRoutine(2f));//정지애니메이션
        StartCoroutine(PauseMovement(2f));//정지
    }
    private void HandleStairsCollision(bool isUp)//계단이동
    {
        // 이동 벡터 결정
        Vector2 delta = isUp
            ? new Vector2(0f, 9f) 
         : new Vector2(0f, -9f);

        

        StartCoroutine(PauseMovement(3f));//정지 코루틴

        StartCoroutine(ScreenFader.Instance.FadeTeleport(() =>//화면전환 페이드아웃
        {
            var zoneCollider = isUp ? upZoneCollider : downZoneCollider;
            // A) 플레이어 & 스포너 이동, 몬스터 초기화, 카메라 워프
            Vector2 pos = isUp
          ? stairs_Down.transform.position
         : stairs_Up.transform.position;

            float offsetX = isUp
                ? -3f   // stairs_Down 일 때
                : +3f; // stairs_Up 일 때

            pos.x = pos.x + offsetX;

            transform.position = pos;
            playerAnim.SetMoved(false);
            PerformStairsTeleport(delta, zoneCollider);
        }));
        
    }

    private void PerformStairsTeleport(Vector3 delta, PolygonCollider2D nextZone)
    {
       
        // 2) 스포너 위치 이동 및 Y 범위 업데이트
        //Vector3 yOffset = new Vector3(0f, delta.y, 0f);
        //cameraClamp.minPos.y += delta.y;
        //cameraClamp.maxPos.y += delta.y;
        //foreach (var sp in spawner)
        //    sp.position += yOffset;

        // 3) 몬스터 초기화
        ClearAllMonsters();

        cinemachineCam.OnTargetObjectWarped(transform, delta);
        // 4) Cinemachine Warp 알림
        var confiner = cinemachineCam.GetComponent<CinemachineConfiner2D>();
        if (confiner != null)
        {
            confiner.BoundingShape2D = nextZone;//카메라 콜라이더 변경
            confiner.InvalidateBoundingShapeCache();
        }
        else
        {
            Debug.LogWarning("CinemachineConfiner2D를 찾을 수 없습니다!");
        }
    }
    private IEnumerator StunRoutine(float duration)//애니메이션 코루틴
    {
        yield return new WaitForSeconds(duration);

        // 3) 넉다운 해제
        playerAnim.SetStunned(false);
    }
    private IEnumerator PauseMovement(float duration)//정지 코루틴
    {
        
        canMove = false;
        yield return new WaitForSeconds(duration);
        canMove = true;
    }

    private void ClearAllMonsters()//몬스터컨테이너 순회 및 삭제
    {

        for (int i = monsterContainer.childCount - 1; i >= 0; --i)
        {
            Destroy(monsterContainer.GetChild(i).gameObject);
        }
    }
}
