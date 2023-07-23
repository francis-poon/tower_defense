using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : ItemHandler
{
    public override void ProcessItem(GameObject _item)
    {
        Debug.Log("Collector");
        InventoryManager.main.AddItem(_item, 1);
        Destroy(_item);
    }
}
