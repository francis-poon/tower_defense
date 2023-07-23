using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager main;

    [Header("Attributes")]
    [SerializeField] List<SerialKeyValue<GameObject, int>> inventoryData;

    private Dictionary<string, int> inventory;

    private void Start()
    {
        main = this;
        inventory = new Dictionary<string, int>();
        foreach (SerialKeyValue<GameObject, int> pair in inventoryData)
        {
            inventory.Add(pair.Key.GetComponent<Item>().itemName, pair.Value);
        }
    }

    public void AddItem(GameObject _item, int _itemCount)
    {
        string itemName = _item.GetComponent<Item>().itemName;
        if (!inventory.ContainsKey(itemName))
        {
            inventory.Add(itemName, _itemCount);
        }
        else
        {
            Debug.Log("Inventory: " + itemName + "\nCount Before Add: " + inventory[itemName]);
            inventory[itemName] += _itemCount;
            Debug.Log("Inventory: " + itemName + "\nCount After Add: " + inventory[itemName]);
        }
    }

    public Boolean RemoveItem(GameObject _item, int _itemCount)
    {
        string itemName = _item.GetComponent<Item>().itemName;
        if (!inventory.ContainsKey(itemName) || inventory[itemName] < _itemCount)
        {
            return false;
        }
        Debug.Log("Inventory: " + itemName + "\nCount Before Remove: " + inventory[itemName]);
        inventory[itemName] -= _itemCount;
        Debug.Log("Inventory: " + itemName + "\nCount After Remove: " + inventory[itemName]);
        return true;
    }

    public int GetItem(GameObject _item)
    {
        string itemName = _item.GetComponent<Item>().itemName;
        if (!inventory.ContainsKey(itemName))
        {
            return 0;
        }
        return inventory[itemName];
    }

    [Serializable]
    public class InventoryData
    {
        public GameObject item;
        public int itemCount;

        public InventoryData(GameObject _item, int _itemCount)
        {
            this.item = _item;
            this.itemCount = _itemCount;
        }
    }

    [Serializable]
    public class KeyValue<TKey, TValue>
    {
        public TKey key;
        public TValue value;
    }
}
