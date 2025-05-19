using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] 
    private GameObject itemPrefab;
    [SerializeField] 
    private List<ItemDataSO> itemDataList;

    private void Update()
    {
       //SpawnRandomItem();
    }

    public void SpawnRandomItem()
    {
        int index = Random.Range(0, itemDataList.Count);
        ItemDataSO selectedData = itemDataList[index];

        GameObject obj = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        obj.GetComponent<Item>().SetUp(selectedData);
    }
}
