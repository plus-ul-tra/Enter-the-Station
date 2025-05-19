using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Header("플레이어")]
    [SerializeField] private Transform player;

    [Header("몬스터 프리팹들")]
    [SerializeField] private GameObject[] monsterPrefabs;

    [Header("1층 스폰 지점들")]
    [SerializeField] private Transform[] spawnPoints_1;

    [Header("2층 스폰 지점들")]
    [SerializeField] private Transform[] spawnPoints_2;

    [Header("스폰 설정")]
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int maxMonsterCount = 10;

    private List<GameObject> activeMonsters = new List<GameObject>();

    private Camera cam;

    void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player").transform;

        cam = Camera.main;
        if (cam == null) Debug.LogError("Main Camera 없음!");

        if (monsterPrefabs == null || monsterPrefabs.Length == 0)
        {
            Debug.LogError("monsterPrefabs 비어 있음");
            enabled = false;
            return;
        }

        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // 1) 파괴된 몬스터 정리
            activeMonsters.RemoveAll(m => m == null);
            if (activeMonsters.Count >= maxMonsterCount)
                continue;

            // 2) 플레이어가 어느 층에 있는지 판정 (예: y <= 1 → 1층, y > 1 → 2층)
            Transform[] currentSpawnPoints;
            if (player.position.y > -10f)
                currentSpawnPoints = spawnPoints_1;
            else if (player.position.y <= -10f)
                currentSpawnPoints = spawnPoints_2;
            else
                continue; // 애매한 위치면 스폰하지 않음

            // 3) 스폰 포인트가 비어 있으면 건너뛰기
            if (currentSpawnPoints == null || currentSpawnPoints.Length == 0)
                continue;

            // 4) 랜덤 스폰 지점 선택
            int idx = Random.Range(0, currentSpawnPoints.Length);
            Transform point = currentSpawnPoints[idx];

            // 5) 뷰포트 밖인지 확인 (옵션)
            Vector3 vp = cam.WorldToViewportPoint(point.position);
            bool isInside =
                vp.x >= 0f && vp.x <= 1f &&
                vp.y >= 0f && vp.y <= 1f &&
                vp.z > 0f;
            if (isInside)
                continue;

            // 6) 프리팹 골라서 인스턴스화
            GameObject prefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
            GameObject m = Instantiate(prefab, point.position, Quaternion.identity);
            activeMonsters.Add(m);

            // 7) 방향 결정 (예: 1층은 왼쪽, 2층은 오른쪽으로 가게 할 수도 있고, 
            //    아니면 포인트 인덱스로 판정)
            int dir = (idx < currentSpawnPoints.Length / 2) ? -1 : +1;
            var mover = m.GetComponent<MonsterMover>();
            if (mover != null) mover.SetDirection(dir);
        }
    }
}
