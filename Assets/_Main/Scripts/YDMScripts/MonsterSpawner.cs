using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Header("���� ������")]
    [SerializeField] private GameObject monsterPrefab;

    [Header("���� ������")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("���� ����")]
    [Tooltip("���� ���� (��)")]
    [SerializeField] private float spawnInterval = 2f;
    [Tooltip("���� ���ÿ� ������ ���� �ִ� ��")]
    [SerializeField] private int maxMonsterCount = 10;

    // Ȱ��ȭ�� ���� ����Ʈ
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

            // null(�ı���) ���� ����
            activeMonsters.RemoveAll(m => m == null);

            if (activeMonsters.Count >= maxMonsterCount)
                continue;

            // 1) ���� ���� ���� �ε��� ����
            int idx = Random.Range(0, spawnPoints.Length);
            Transform point = spawnPoints[idx];

            // 2) ���� �ν��Ͻ�ȭ
            GameObject m = Instantiate(monsterPrefab, point.position, Quaternion.identity);
            //m.transform.SetParent(transform);
            activeMonsters.Add(m);

            // 3) ���� ����: spawnPoints[0..2] �� ����(-1), [3..5] �� ������(+1)
            int dir = (idx <= 2) ? -1 : +1;

            // 4) MonsterMover ������Ʈ�� ���� ����
            MonsterMover mover = m.GetComponent<MonsterMover>();
            if (mover != null)
                mover.SetDirection(dir);
        }
    }
}
