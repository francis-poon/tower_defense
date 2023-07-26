using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] itemPrefabs;
    [SerializeField] private GameObject sendPoint;

    [Header("Attributes")]
    [SerializeField] private float itemSpawnRate;

    private float timeSinceLastSpawn;
    private int selectedItem;

    private void Start()
    {
        selectedItem = -1;
    }

    void Update()
    {
        if (selectedItem == -1) return;

        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= (1f / itemSpawnRate))
        {
            SpawnItem();
            timeSinceLastSpawn = 0f;
        }
    }

    private void SpawnItem()
    {
        if (InventoryManager.main.RemoveItem(itemPrefabs[selectedItem], 1))
        {
            GameObject item = Instantiate(itemPrefabs[selectedItem], transform.position, Quaternion.identity);
            GetComponent<ItemHandler>().ProcessItem(item);
        }
    }

    public void SelectItem(int _itemIndex)
    {
        selectedItem = _itemIndex;
    }
}
