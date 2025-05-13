using UnityEngine;
using System.Collections.Generic;

public class EventDirectionArrow : MonoBehaviour
{
    public GameObject arrowPrefab;          // 화살표 프리팹
    public float ellipseX = 1f;             // 타원형 X 반지름
    public float ellipseY = 0.6f;             // 타원형 Y 반지름
    public float spriteAngleOffset = -90f;  // 화살표 Sprite가 기본적으로 바라보는 방향과 X축 사이의 각도

    private Transform player;               // 플레이어 (위치 중심 잡기 위해서)

    // 추적할 돌발상황과 그 트랜스폼
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
    /// 돌발상황 추적하는 화살표 생성
    /// </summary>
    /// <param name="eventObj">랜덤 돌발상황</param>
    public void CreateArrow(RandomEventObject eventObj)
    {
        GameObject newArrow = Instantiate(arrowPrefab, player.position, Quaternion.identity, transform);
        arrowDict[eventObj] = newArrow.transform;
    }

    /// <summary>
    /// 특정 화살표 제거 (참조 또는 index로 가능)
    /// </summary>
    /// <param name="eventObj">랜덤 돌발상황</param>
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
                // 타원 궤도 위 위치 계산
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
                // 오브젝트 아래 화살표
                Vector3 offset = Vector3.down * 1.5f;
                arrow.position = eventObj.transform.position + offset;

                Vector2 dir = Vector2.up;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                arrow.rotation = Quaternion.Euler(0, 0, angle + spriteAngleOffset);
            }
        }
    }

    // 씬 뷰에서 타원 Gizmo로 그리기
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
