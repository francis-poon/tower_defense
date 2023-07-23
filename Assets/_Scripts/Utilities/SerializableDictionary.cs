using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<SerialKeyValue<TKey, TValue>> keyValuePairs = new List<SerialKeyValue<TKey, TValue>>();

    // save the dictionary to lists
    public void OnBeforeSerialize()
    {
        keyValuePairs.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            keyValuePairs.Add(new SerialKeyValue<TKey, TValue>(pair.Key, pair.Value));
        }
    }

    // load dictionary from lists
    public void OnAfterDeserialize()
    {
        this.Clear();

        foreach (SerialKeyValue<TKey, TValue> pair in keyValuePairs)
        {
            this.Add(pair.key, pair.value);
        }
    }
}

