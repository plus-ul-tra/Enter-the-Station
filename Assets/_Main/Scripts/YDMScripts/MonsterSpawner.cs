using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Header("몬스터 프리팹")]
    [SerializeField] private GameObject monsterPrefab;

    [Header("스폰 지점들")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("스폰 설정")]
    [Tooltip("스폰 간격 (초)")]
    [SerializeField] private float spawnInterval = 2f;
    [Tooltip("씬에 동시에 존재할 몬스터 최대 수")]
    [SerializeField] private int maxMonsterCount = 10;

    // 활성화된 몬스터 리스트
    private readonly List<GameObject> activeMonsters = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // null(파괴된) 몬스터 제거
            activeMonsters.RemoveAll(m => m == null);

            if (activeMonsters.Count >= maxMonsterCount)
                continue;

            // 1) 랜덤 스폰 지점 인덱스 선택
            int idx = Random.Range(0, spawnPoints.Length);
            Transform point = spawnPoints[idx];

            // 2) 몬스터 인스턴스화
            GameObject m = Instantiate(monsterPrefab, point.position, Quaternion.identity);
            //m.transform.SetParent(transform);
            activeMonsters.Add(m);

            // 3) 방향 결정: spawnPoints[0..2] → 왼쪽(-1), [3..5] → 오른쪽(+1)
            int dir = (idx <= 2) ? -1 : +1;

            // 4) MonsterMover 컴포넌트에 방향 전달
            MonsterMover mover = m.GetComponent<MonsterMover>();
            if (mover != null)
                mover.SetDirection(dir);
        }
    }
}
