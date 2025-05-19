using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Header("�÷��̾�")]
    [SerializeField] private Transform player;

    [Header("���� �����յ�")]
    [SerializeField] private GameObject[] monsterPrefabs;

    [Header("1�� ���� ������")]
    [SerializeField] private Transform[] spawnPoints_1;

    [Header("2�� ���� ������")]
    [SerializeField] private Transform[] spawnPoints_2;

    [Header("���� ����")]
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int maxMonsterCount = 10;

    private List<GameObject> activeMonsters = new List<GameObject>();

    private Camera cam;

    void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player").transform;

        cam = Camera.main;
        if (cam == null) Debug.LogError("Main Camera ����!");

        if (monsterPrefabs == null || monsterPrefabs.Length == 0)
        {
            Debug.LogError("monsterPrefabs ��� ����");
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

            // 1) �ı��� ���� ����
            activeMonsters.RemoveAll(m => m == null);
            if (activeMonsters.Count >= maxMonsterCount)
                continue;

            // 2) �÷��̾ ��� ���� �ִ��� ���� (��: y <= 1 �� 1��, y > 1 �� 2��)
            Transform[] currentSpawnPoints;
            if (player.position.y > -10f)
                currentSpawnPoints = spawnPoints_1;
            else if (player.position.y <= -10f)
                currentSpawnPoints = spawnPoints_2;
            else
                continue; // �ָ��� ��ġ�� �������� ����

            // 3) ���� ����Ʈ�� ��� ������ �ǳʶٱ�
            if (currentSpawnPoints == null || currentSpawnPoints.Length == 0)
                continue;

            // 4) ���� ���� ���� ����
            int idx = Random.Range(0, currentSpawnPoints.Length);
            Transform point = currentSpawnPoints[idx];

            // 5) ����Ʈ ������ Ȯ�� (�ɼ�)
            Vector3 vp = cam.WorldToViewportPoint(point.position);
            bool isInside =
                vp.x >= 0f && vp.x <= 1f &&
                vp.y >= 0f && vp.y <= 1f &&
                vp.z > 0f;
            if (isInside)
                continue;

            // 6) ������ ��� �ν��Ͻ�ȭ
            GameObject prefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
            GameObject m = Instantiate(prefab, point.position, Quaternion.identity);
            activeMonsters.Add(m);

            // 7) ���� ���� (��: 1���� ����, 2���� ���������� ���� �� ���� �ְ�, 
            //    �ƴϸ� ����Ʈ �ε����� ����)
            int dir = (idx < currentSpawnPoints.Length / 2) ? -1 : +1;
            var mover = m.GetComponent<MonsterMover>();
            if (mover != null) mover.SetDirection(dir);
        }
    }
}
