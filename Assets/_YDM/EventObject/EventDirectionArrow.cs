using UnityEngine;
using System.Collections.Generic;

public class EventDirectionArrow : MonoBehaviour
{
    public GameObject arrowPrefab;          // ȭ��ǥ ������
    public float ellipseX = 1f;             // Ÿ���� X ������
    public float ellipseY = 0.6f;             // Ÿ���� Y ������
    public float spriteAngleOffset = -90f;  // ȭ��ǥ Sprite�� �⺻������ �ٶ󺸴� ����� X�� ������ ����

    private Transform player;               // �÷��̾� (��ġ �߽� ��� ���ؼ�)

    // ������ ���߻�Ȳ�� �� Ʈ������
    private Dictionary<RandomEventObject, Transform> arrowDict = new Dictionary<RandomEventObject, Transform>();

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        UpdateArrows();
    }

    /// <summary>
    /// ���߻�Ȳ �����ϴ� ȭ��ǥ ����
    /// </summary>
    /// <param name="eventObj">���� ���߻�Ȳ</param>
    public void CreateArrow(RandomEventObject eventObj)
    {
        GameObject newArrow = Instantiate(arrowPrefab, player.position, Quaternion.identity, transform);
        arrowDict[eventObj] = newArrow.transform;
    }

    /// <summary>
    /// Ư�� ȭ��ǥ ���� (���� �Ǵ� index�� ����)
    /// </summary>
    /// <param name="eventObj">���� ���߻�Ȳ</param>
    public void RemoveArrow(RandomEventObject eventObj)
    {
        if(arrowDict.TryGetValue(eventObj, out Transform arrow))
        {
            Destroy(arrow.gameObject);
            arrowDict.Remove(eventObj);
        }
    }


    private void UpdateArrows()
    {
        foreach(KeyValuePair<RandomEventObject, Transform> pair in arrowDict)
        {
            RandomEventObject eventObj = pair.Key;
            Transform arrow = pair.Value;

            if (eventObj == null || arrow == null) continue;

            Vector3 toTarget = eventObj.transform.position - player.position;
            float distance = toTarget.magnitude;

            if (distance >= 10f)
            {
                // Ÿ�� �˵� �� ��ġ ���
                Vector2 dir = toTarget.normalized;
                float angle = Mathf.Atan2(dir.y, dir.x);
                float x = Mathf.Cos(angle) * ellipseX;
                float y = Mathf.Sin(angle) * ellipseY;

                arrow.position = player.position + new Vector3(x, y, 0f);

                float arrowAngle = angle * Mathf.Rad2Deg;
                arrow.rotation = Quaternion.Euler(0, 0, arrowAngle + spriteAngleOffset);
            }
            else
            {
                // ������Ʈ �Ʒ� ȭ��ǥ
                Vector3 offset = Vector3.down * 1.5f;
                arrow.position = eventObj.transform.position + offset;

                Vector2 dir = Vector2.up;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                arrow.rotation = Quaternion.Euler(0, 0, angle + spriteAngleOffset);
            }
        }
    }

    // �� �信�� Ÿ�� Gizmo�� �׸���
    private void OnDrawGizmos()
    {
        if (player == null) return;

        Gizmos.color = Color.yellow;

        int segments = 60;
        Vector3 prevPoint = Vector3.zero;

        for (int i = 0; i <= segments; i++)
        {
            float angle = 2 * Mathf.PI * i / segments;
            float x = Mathf.Cos(angle) * ellipseX;
            float y = Mathf.Sin(angle) * ellipseY;

            Vector3 point = player.position + new Vector3(x, -0.5f, 0f);

            if(i > 0)
            {
                Gizmos.DrawLine(prevPoint, point);
            }

            prevPoint = point;
        }
    }
}
