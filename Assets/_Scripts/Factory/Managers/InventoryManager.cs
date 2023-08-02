using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager main;

    [Header("Attributes")]
    [SerializeField] List<InventoryMeta> inventoryData;

    private Dictionary<string, InventoryMeta> inventory;

    private void Start()
    {
        main = this;
        inventory = new Dictionary<string, InventoryMeta>();
        foreach (InventoryMeta metaData in inventoryData)
        {
            string itemName = metaData.item.GetComponent<Item>().itemName;
            inventory.Add(itemName, metaData);
            inventory[itemName].itemCountDisplay.GetComponent<TMP_Text>().text = inventory[itemName].itemCount.ToString();
        }
    }

    public void AddItem(GameObject _item, int _itemCount)
    {
        string itemName = _item.GetComponent<Item>().itemName;
        if (!inventory.ContainsKey(itemName))
        {
            // TODO: Add procedurally generated inventory display
            inventory.Add(itemName, new InventoryMeta(_item, _itemCount, null));
        }
        else
        {
            Debug.Log("Inventory: " + itemName + "\nCount Before Add: " + inventory[itemName].itemCount);
            inventory[itemName].itemCount += _itemCount;
            Debug.Log("Inventory: " + itemName + "\nCount After Add: " + inventory[itemName].itemCount);
        }
        inventory[itemName].itemCountDisplay.GetComponent<TMP_Text>().text = inventory[itemName].itemCount.ToString();
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
        inventory[itemName].itemCountDisplay.GetComponent<TMP_Text>().text = inventory[itemName].itemCount.ToString();
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

    public GameObject[] GetInventoryPrefabs()
    {
        GameObject[] output = new GameObject[inventoryData.Count];

        for (int c = 0; c < inventoryData.Count; c ++)
        {
            output[c] = inventoryData[c].item;
        }

        return output;
    }

    [Serializable]
    public class InventoryMeta
    {
        public GameObject item;
        public int itemCount;
        public GameObject itemCountDisplay;

        public InventoryMeta(GameObject _item, int _itemCount, GameObject itemCountDisplay)
        {
            this.item = _item;
            this.itemCount = _itemCount;
            this.itemCountDisplay = itemCountDisplay;
        }
    }
}
