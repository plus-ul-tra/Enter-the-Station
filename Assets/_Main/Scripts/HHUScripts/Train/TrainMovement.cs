using UnityEngine;
using System.Collections;

public class TrainMovement : MonoBehaviour
{
    [Header("���� �̵� �ӵ�")]
    public float trainSpeed = 1f;
    [Header("���� �ð� (��)")]
    public float pauseDuration = 2f;

    [Header("���� ����")]
    [SerializeField] private GameObject[] leftDoor;
    [Header("������ ����")]
    [SerializeField] private GameObject[] rightDoor;

    [Header("�� ���� �Ÿ�")]
    public float doorOpenDistance = 1f;
    [Header("�� �̵� �ִϸ��̼� �ð�(��)")]
    public float doorMoveDuration = 0.5f;

    private Vector3 startPos;//���� ���� ��ġ ����
    private bool isWaiting = false;//���� ���� �÷���
    private bool hasPaused = false;//���� ���� ���� Ȯ�� �÷���

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (isWaiting) return;

        // ���� �̵�
        transform.position += Vector3.left * trainSpeed * Time.deltaTime;

        // �߰� �������� �� ���� ����
        if (!hasPaused && transform.position.x < startPos.x - 25f)
        {
            hasPaused = true;
            StartCoroutine(PauseAndAnimateDoors());
            return;
        }

        // �� ���� ���� �� ����
        if (transform.position.x < startPos.x - 45f)
        {
            transform.position = startPos;
            hasPaused = false;
        }
    }

    private IEnumerator PauseAndAnimateDoors()
        //���� ���� �ڷ�ƾ
    {
        isWaiting = true;

        // 1) ���� ���� �� ��ġ ���� (���� ����)
        Vector3[] initialLeftPositions = new Vector3[leftDoor.Length];
        Vector3[] initialRightPositions = new Vector3[rightDoor.Length];
        for (int i = 0; i < leftDoor.Length; i++)
            initialLeftPositions[i] = leftDoor[i].transform.position;
        for (int i = 0; i < rightDoor.Length; i++)
            initialRightPositions[i] = rightDoor[i].transform.position;

        // 2) ���� ���� �� ��ġ ���� (���� ����)
        Vector3[] openedLeftPositions = new Vector3[leftDoor.Length];
        Vector3[] openedRightPositions = new Vector3[rightDoor.Length];
        for (int i = 0; i < leftDoor.Length; i++)
            openedLeftPositions[i] = initialLeftPositions[i] + Vector3.left * doorOpenDistance;
        for (int i = 0; i < rightDoor.Length; i++)
            openedRightPositions[i] = initialRightPositions[i] + Vector3.right * doorOpenDistance;

        yield return new WaitForSeconds(1.0f);

        // 3) �� ���� �ִϸ��̼� (���� �� ����)
        yield return StartCoroutine(AnimateDoors(initialLeftPositions, openedLeftPositions,
                                                 initialRightPositions, openedRightPositions));

        // 4) ������ ���� �ð� ���
        yield return new WaitForSeconds(pauseDuration);

        // 5) �� �ݱ� �ִϸ��̼� (���� �� ����)
        yield return StartCoroutine(AnimateDoors(openedLeftPositions, initialLeftPositions,
                                                 openedRightPositions, initialRightPositions));

        yield return new WaitForSeconds(0.5f);
        isWaiting = false;
    }

    private IEnumerator AnimateDoors(
        Vector3[] leftOrigins, Vector3[] leftTargets,
        Vector3[] rightOrigins, Vector3[] rightTargets)
    //�� �̵� �ڷ�ƾ
    {
        if (leftDoor.Length != leftOrigins.Length || rightDoor.Length != rightOrigins.Length)
            yield break;

        float timer = 0f;

        while (timer < doorMoveDuration)//�� �̵� �ð�
        {
            float t = timer / doorMoveDuration;
            for (int i = 0; i < leftDoor.Length; i++)//����Ǿ� �ִ� ��ġ�� �� �̵�
                leftDoor[i].transform.position = Vector3.Lerp(leftOrigins[i], leftTargets[i], t);
            for (int i = 0; i < rightDoor.Length; i++)
                rightDoor[i].transform.position = Vector3.Lerp(rightOrigins[i], rightTargets[i], t);

            timer += Time.deltaTime;
            yield return null;
        }

        // ��Ȯ�� ���� ��ġ ����
        for (int i = 0; i < leftDoor.Length; i++)
            leftDoor[i].transform.position = leftTargets[i];
        for (int i = 0; i < rightDoor.Length; i++)
            rightDoor[i].transform.position = rightTargets[i];
    }
}
