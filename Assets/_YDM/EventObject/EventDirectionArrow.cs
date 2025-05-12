using UnityEngine;
using System.Collections.Generic;

public class EventDirectionArrow : MonoBehaviour
{
    public GameObject arrowPrefab;          // ȭ��ǥ ������
    public float radius = 5f;               // �÷��̾� �߽� �� ������
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

            Vector2 dir = (eventObj.transform.position - player.position).normalized;
            arrow.position = player.position + (Vector3)(dir * radius);

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            arrow.rotation = Quaternion.Euler(0, 0, angle + spriteAngleOffset);
        }
    }
}
