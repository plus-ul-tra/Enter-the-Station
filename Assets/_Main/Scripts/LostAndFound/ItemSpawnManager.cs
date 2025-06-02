using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    List<ItemSpawner> spawners;
    [SerializeField]
    float maxSpawnTime = 30.0f;
    [SerializeField]
    float minSpawnTime = 15.0f;
    float nextSpawnTime = 0.0f;
    float timer = 0.0f;
    int spawnerIndex = 0;
    private void OnEnable()
    {
        spawners = new List<ItemSpawner>(GetComponentsInChildren<ItemSpawner>());
        //spawnerCount = spawners.Count;
        SetNextSpawnTime();

    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= nextSpawnTime)
        {
            SpawnItem();
            SetNextSpawnTime();
            timer = 0.0f;
        }
    }

    private void SetNextSpawnTime() {
        nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    private void SpawnItem()
    {
        if (spawners == null || spawners.Count == 0) return;
        //Debug.Log("아이템 소환");
        spawnerIndex = Random.Range(0, spawners.Count);
        spawners[spawnerIndex].SpawnRandomItem();
    }


}
