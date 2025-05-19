using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class EventDirectionArrow : MonoBehaviour
{
    public GameObject arrowPrefab_1th;          // 1층 화살표 프리팹
    public GameObject arrowPrefab_2nd;          // 2층 화살표 프리팹
    public float radius = 0.2f;               // 원형 반지름
    public float yOffset = 0.3f; // 원을 얼마나 아래로 내릴지 설정
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
    public void CreateArrow(RandomEventObject eventObj, int zoneIndex = 1)
    {
        GameObject arrowSelect;

        switch(zoneIndex)
        {
            case 1:
            case 2:
            case 3:
                arrowSelect = arrowPrefab_1th;
                break;

            case 4:
            case 5:
            case 6:
                arrowSelect = arrowPrefab_2nd;
                break;

            default:
                arrowSelect = arrowPrefab_1th;
                break;
        }

        GameObject newArrow = Instantiate(arrowSelect, player.position, Quaternion.identity, transform);
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

            if (distance >= 2.75f)
            {
                // 🔥 적의 위치를 반영하여 원 궤도 이동 각도 계산
                float angleRad = Mathf.Atan2(toTarget.y, toTarget.x);

                // 🔄 시간에 따른 회전 추가 (적을 향하는 방향 고려)
                angleRad += Time.deltaTime * 2f; // 회전 속도 조정

                float x = Mathf.Cos(angleRad) * radius;
                float y = Mathf.Sin(angleRad) * radius;

                // 🌍 중심을 아래로 yOffset 만큼 이동 (이 부분 추가!)
                Vector3 orbitCenter = player.position + new Vector3(0f, -yOffset, 0f);
                arrow.position = orbitCenter + new Vector3(x, y, 0f);

                // 🔄 적을 향하도록 회전
                float angle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;
                arrow.rotation = Quaternion.Euler(0, 0, angle + spriteAngleOffset);
            }
            else
            {
                // 오브젝트 아래 화살표
                Vector3 offset = new Vector3(0, -0.6f, 0);
                arrow.position = eventObj.transform.position + offset;

                Vector2 dir = Vector2.up;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                arrow.rotation = Quaternion.Euler(0, 0, angle + spriteAngleOffset);
            }
        }
    }
}
