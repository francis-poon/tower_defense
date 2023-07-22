using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemConversion
{
    public float conversionTime;
    public GameObject outputItem;
    public ItemConversion(float _conversionTime, GameObject _outputItem)
    {
        conversionTime = _conversionTime;
        outputItem = _outputItem;
    }
}
