using UnityEngine;
using System.Collections;

public class TrainMovement : MonoBehaviour
{
    [Header("기차 이동 속도")]
    public float trainSpeed = 1f;
    [Header("멈춤 시간 (초)")]
    public float pauseDuration = 2f;

    [Header("왼쪽 문들")]
    [SerializeField] private GameObject[] leftDoor;
    [Header("오른쪽 문들")]
    [SerializeField] private GameObject[] rightDoor;

    [Header("문 열림 거리")]
    public float doorOpenDistance = 1f;
    [Header("문 이동 애니메이션 시간(초)")]
    public float doorMoveDuration = 0.5f;

    private Vector3 startPos;//열차 시작 위치 저장
    private bool isWaiting = false;//열차 정지 플래그
    private bool hasPaused = false;//현재 정지 상태 확인 플래그

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (isWaiting) return;

        // 기차 이동
        transform.position += Vector3.left * trainSpeed * Time.deltaTime;

        // 중간 지점에서 한 번만 멈춤
        if (!hasPaused && transform.position.x < startPos.x - 25f)
        {
            hasPaused = true;
            StartCoroutine(PauseAndAnimateDoors());
            return;
        }

        // 끝 지점 도달 시 리셋
        if (transform.position.x < startPos.x - 45f)
        {
            transform.position = startPos;
            hasPaused = false;
        }
    }

    private IEnumerator PauseAndAnimateDoors()
        //열차 정지 코루틴
    {
        isWaiting = true;

        // 1) 닫힌 상태 문 위치 저장 (닫힌 상태)
        Vector3[] initialLeftPositions = new Vector3[leftDoor.Length];
        Vector3[] initialRightPositions = new Vector3[rightDoor.Length];
        for (int i = 0; i < leftDoor.Length; i++)
            initialLeftPositions[i] = leftDoor[i].transform.position;
        for (int i = 0; i < rightDoor.Length; i++)
            initialRightPositions[i] = rightDoor[i].transform.position;

        // 2) 열린 상태 문 위치 저장 (열린 상태)
        Vector3[] openedLeftPositions = new Vector3[leftDoor.Length];
        Vector3[] openedRightPositions = new Vector3[rightDoor.Length];
        for (int i = 0; i < leftDoor.Length; i++)
            openedLeftPositions[i] = initialLeftPositions[i] + Vector3.left * doorOpenDistance;
        for (int i = 0; i < rightDoor.Length; i++)
            openedRightPositions[i] = initialRightPositions[i] + Vector3.right * doorOpenDistance;

        yield return new WaitForSeconds(1.0f);

        // 3) 문 열기 애니메이션 (닫힌 → 열린)
        yield return StartCoroutine(AnimateDoors(initialLeftPositions, openedLeftPositions,
                                                 initialRightPositions, openedRightPositions));

        // 4) 지정된 멈춤 시간 대기
        yield return new WaitForSeconds(pauseDuration);

        // 5) 문 닫기 애니메이션 (열린 → 닫힌)
        yield return StartCoroutine(AnimateDoors(openedLeftPositions, initialLeftPositions,
                                                 openedRightPositions, initialRightPositions));

        yield return new WaitForSeconds(0.5f);
        isWaiting = false;
    }

    private IEnumerator AnimateDoors(
        Vector3[] leftOrigins, Vector3[] leftTargets,
        Vector3[] rightOrigins, Vector3[] rightTargets)
    //문 이동 코루틴
    {
        if (leftDoor.Length != leftOrigins.Length || rightDoor.Length != rightOrigins.Length)
            yield break;

        float timer = 0f;

        while (timer < doorMoveDuration)//문 이동 시간
        {
            float t = timer / doorMoveDuration;
            for (int i = 0; i < leftDoor.Length; i++)//저장되어 있는 위치로 문 이동
                leftDoor[i].transform.position = Vector3.Lerp(leftOrigins[i], leftTargets[i], t);
            for (int i = 0; i < rightDoor.Length; i++)
                rightDoor[i].transform.position = Vector3.Lerp(rightOrigins[i], rightTargets[i], t);

            timer += Time.deltaTime;
            yield return null;
        }

        // 정확히 최종 위치 설정
        for (int i = 0; i < leftDoor.Length; i++)
            leftDoor[i].transform.position = leftTargets[i];
        for (int i = 0; i < rightDoor.Length; i++)
            rightDoor[i].transform.position = rightTargets[i];
    }
}
