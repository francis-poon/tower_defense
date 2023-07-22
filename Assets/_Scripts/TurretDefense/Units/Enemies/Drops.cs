using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Drops : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int goldValue;

    public void DropResources()
    {
        CurrencyManager.main.AddGold(goldValue);
    }
}
