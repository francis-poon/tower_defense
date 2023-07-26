using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager main;

    [Header("Attributes")]
    [SerializeField] List<SerialKeyValue<GameObject, int>> inventoryData;

    private Dictionary<string, InventoryMeta> inventory;

    private void Start()
    {
        main = this;
        inventory = new Dictionary<string, InventoryMeta>();
        foreach (SerialKeyValue<GameObject, int> pair in inventoryData)
        {
            inventory.Add(pair.Key.GetComponent<Item>().itemName, new InventoryMeta(pair.Key, pair.Value));
        }
    }

    public void AddItem(GameObject _item, int _itemCount)
    {
        string itemName = _item.GetComponent<Item>().itemName;
        if (!inventory.ContainsKey(itemName))
        {
            inventory.Add(itemName, new InventoryMeta(_item, _itemCount));
        }
        else
        {
            Debug.Log("Inventory: " + itemName + "\nCount Before Add: " + inventory[itemName].itemCount);
            inventory[itemName].itemCount += _itemCount;
            Debug.Log("Inventory: " + itemName + "\nCount After Add: " + inventory[itemName].itemCount);
        }
    }

    public Boolean RemoveItem(GameObject _item, int _itemCount)
    {
        string itemName = _item.GetComponent<Item>().itemName;
        if (!inventory.ContainsKey(itemName) || inventory[itemName].itemCount < _itemCount)
        {
            return false;
        }
        Debug.Log("Inventory: " + itemName + "\nCount Before Remove: " + inventory[itemName].itemCount);
        inventory[itemName].itemCount -= _itemCount;
        Debug.Log("Inventory: " + itemName + "\nCount After Remove: " + inventory[itemName].itemCount);
        return true;
    }

    public int GetItemCount(GameObject _item)
    {
        string itemName = _item.GetComponent<Item>().itemName;
        if (!inventory.ContainsKey(itemName))
        {
            return 0;
        }
        return inventory[itemName].itemCount;
    }

    [Serializable]
    public class InventoryMeta
    {
        public GameObject item;
        public int itemCount;

        public InventoryMeta(GameObject _item, int _itemCount)
        {
            this.item = _item;
            this.itemCount = _itemCount;
        }
    }
}
