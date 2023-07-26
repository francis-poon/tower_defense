using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    private GameObject spawner;

    public void SetSpawner(GameObject _spawner)
    {
        spawner = _spawner;
    }

    public void SelectItem(int _itemIndex)
    {
        spawner.GetComponent<ItemSpawner>().SelectItem(_itemIndex);
        FactoryLevelManager.main.OnItemSelected();
    }
}
