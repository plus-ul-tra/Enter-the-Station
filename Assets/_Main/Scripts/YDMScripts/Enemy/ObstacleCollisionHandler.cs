using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class ObstacleCollisionHandler2D : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] private float verticalDistance = 0.01f;
    [SerializeField] private float forwardDistance = 0f; // 필요 없으면 0으로
    [Tooltip("이동에 걸릴 시간(초)")]
    [SerializeField] private float moveDuration = 0.1f;

    private Transform parentTf;

    private void Awake()
    {
        parentTf = transform.parent;
    }
    private void Start()
    {
        var mover = parentTf.GetComponent<MonsterMover>();
        int dir = mover != null ? mover.direction : 1;

        if (dir < 0)
        {
            Vector3 myPos = transform.position;
            myPos.x += -1f;
            transform.position = myPos;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MapObstacle"))
        {
            StopAllCoroutines();               // 혹시 중복 시작 방지
            StartCoroutine(SmoothMoveVertical(other));
        }
        if (other.CompareTag("BoothObstacle"))
        {
            StopAllCoroutines();               // 혹시 중복 시작 방지
            StartCoroutine(SmoothMoveDown());
        }
    }


    private IEnumerator SmoothMoveVertical(Collider2D obstacle)
    {
        // 1) 현재 MonsterMover 스크립트에서 방향 가져오기
        var mover = parentTf.GetComponent<MonsterMover>();
        int dir = mover != null ? mover.direction : 1;
        float obstacleY = obstacle.transform.position.y;
        float monsterY = parentTf.position.y;

        int vSign = (monsterY > obstacleY) ? -1 : +1;

        // 3) 시작/끝 위치 계산
        Vector3 startPos = parentTf.position;
        Vector3 verticalOffset = Vector3.up * verticalDistance * vSign;
        Vector3 horizontalOffset = Vector3.right * dir * forwardDistance;
        Vector3 endPos = startPos + verticalOffset + horizontalOffset;

        // 4) 부드러운 이동 보간
        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);
            parentTf.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        // 5) 최종 위치 보정
        parentTf.position = endPos;
    }

    private IEnumerator SmoothMoveDown()
    {
        // 1) 현재 MonsterMover 스크립트에서 방향 가져오기
        var mover = parentTf.GetComponent<MonsterMover>();
        int dir = mover != null ? mover.direction : 1;
        Vector3 forwardDir = (dir < 0) ? Vector3.left : Vector3.right;

        // 2) 수직 이동 방향을 무조건 아래(-1)로 설정
        int vSign = -1;

        // 3) 시작/끝 위치 계산
        Vector3 startPos = parentTf.position;
        Vector3 verticalOffset = Vector3.up * verticalDistance * vSign;
        Vector3 horizontalOffset = forwardDir * forwardDistance;
        Vector3 endPos = startPos + verticalOffset + horizontalOffset;

        // 4) 부드러운 이동 보간
        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);
            parentTf.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        // 5) 최종 위치 보정
        parentTf.position = endPos;
    }
}