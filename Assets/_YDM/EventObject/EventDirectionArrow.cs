using UnityEngine;
using System.Collections.Generic;

public class EventDirectionArrow : MonoBehaviour
{
    public GameObject arrowPrefab;          // 화살표 프리팹
    public float radius = 5f;               // 플레이어 중심 원 반지름
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

            Vector2 dir = (eventObj.transform.position - player.position).normalized;
            arrow.position = player.position + (Vector3)(dir * radius);

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            arrow.rotation = Quaternion.Euler(0, 0, angle + spriteAngleOffset);
        }
    }
}
