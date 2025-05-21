using System.Collections;
using UnityEngine;

public class PassengerSpawner : MonoBehaviour
{
    [Header("������ �����յ� (�� �� �̻�)")]
    [SerializeField] private GameObject[] prefabs;

    [Header("���� ������ (������ �� ������Ʈ ��ġ)")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("���� ���� (��)")]
    [SerializeField] private float spawnInterval = 2f;


    private int spawnedCount = 0;

    private void Start()
    {
        if (prefabs == null || prefabs.Length < 1)
        {
            Debug.LogError("Prefabs �迭�� �������� �ϳ� �̻� �Ҵ��ϼ���.");
            enabled = false;
            return;
        }
        //StartCoroutine(SpawnRoutine());
    }

    public IEnumerator SpawnRoutine(float time)
    {
        float endTime = Time.time + time;
        while (Time.time < endTime)
        {
            SpawnOne();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnOne()
    {
        // ���� ������ ����
        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

        // ���� ��ġ��ȸ�� ����
        Vector3 pos;
        Quaternion rot;
        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            var t = spawnPoints[Random.Range(0, spawnPoints.Length)];
            pos = t.position;
            rot = t.rotation;
        }
        else
        {
            pos = transform.position;
            rot = transform.rotation;
        }

        Instantiate(prefab, pos, rot);
    }
}
