using System.Collections;
using UnityEngine;

public class PassengerSpawner : MonoBehaviour
{
    [Header("스폰할 프리팹들 (두 개 이상)")]
    [SerializeField] private GameObject[] prefabs;

    [Header("스폰 지점들 (없으면 이 오브젝트 위치)")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("스폰 간격 (초)")]
    [SerializeField] private float spawnInterval = 2f;


    private int spawnedCount = 0;

    private void Start()
    {
        if (prefabs == null || prefabs.Length < 1)
        {
            Debug.LogError("Prefabs 배열에 프리팹을 하나 이상 할당하세요.");
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
        // 랜덤 프리팹 선택
        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

        // 스폰 위치·회전 결정
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
