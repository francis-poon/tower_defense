using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>
{
    [SerializeField] private List<SerialKeyValue<TKey, TValue>> keyValuePairs = new List<SerialKeyValue<TKey, TValue>>();

    private void Awake()
    {
        this.Clear();

        foreach (SerialKeyValue<TKey, TValue> pair in keyValuePairs)
        {
            this.Add(pair.Key, pair.Value);
        }
    }
}

