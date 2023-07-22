using System;
using UnityEngine;

[Serializable]
public class Machine
{
    public string name;
    public int cost;
    public GameObject prefab;

    public Machine(string _name,  int _cost, GameObject _prefab)
    {
        name = _name;
        cost = _cost;
        prefab = _prefab;
    }

    public void ProcessItem(GameObject _item)
    {
        return;
    }
}
