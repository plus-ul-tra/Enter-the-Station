using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class ObstacleCollisionHandler2D : MonoBehaviour
{
    [Header("�̵� ����")]
    [SerializeField] private float verticalDistance = 0.01f;
    [SerializeField] private float forwardDistance = 0f; // �ʿ� ������ 0����
    [Tooltip("�̵��� �ɸ� �ð�(��)")]
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
            StopAllCoroutines();               // Ȥ�� �ߺ� ���� ����
            StartCoroutine(SmoothMoveVertical(other));
        }
        if (other.CompareTag("BoothObstacle"))
        {
            StopAllCoroutines();               // Ȥ�� �ߺ� ���� ����
            StartCoroutine(SmoothMoveDown());
        }
    }


    private IEnumerator SmoothMoveVertical(Collider2D obstacle)
    {
        // 1) ���� MonsterMover ��ũ��Ʈ���� ���� ��������
        var mover = parentTf.GetComponent<MonsterMover>();
        int dir = mover != null ? mover.direction : 1;
        float obstacleY = obstacle.transform.position.y;
        float monsterY = parentTf.position.y;

        int vSign = (monsterY > obstacleY) ? -1 : +1;

        // 3) ����/�� ��ġ ���
        Vector3 startPos = parentTf.position;
        Vector3 verticalOffset = Vector3.up * verticalDistance * vSign;
        Vector3 horizontalOffset = Vector3.right * dir * forwardDistance;
        Vector3 endPos = startPos + verticalOffset + horizontalOffset;

        // 4) �ε巯�� �̵� ����
        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);
            parentTf.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        // 5) ���� ��ġ ����
        parentTf.position = endPos;
    }

    private IEnumerator SmoothMoveDown()
    {
        // 1) ���� MonsterMover ��ũ��Ʈ���� ���� ��������
        var mover = parentTf.GetComponent<MonsterMover>();
        int dir = mover != null ? mover.direction : 1;
        Vector3 forwardDir = (dir < 0) ? Vector3.left : Vector3.right;

        // 2) ���� �̵� ������ ������ �Ʒ�(-1)�� ����
        int vSign = -1;

        // 3) ����/�� ��ġ ���
        Vector3 startPos = parentTf.position;
        Vector3 verticalOffset = Vector3.up * verticalDistance * vSign;
        Vector3 horizontalOffset = forwardDir * forwardDistance;
        Vector3 endPos = startPos + verticalOffset + horizontalOffset;

        // 4) �ε巯�� �̵� ����
        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);
            parentTf.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        // 5) ���� ��ġ ����
        parentTf.position = endPos;
    }
}