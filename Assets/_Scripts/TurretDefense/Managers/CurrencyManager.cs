using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager main;

    [Header("Attributes")]
    [SerializeField] private int startingGold;

    private int gold;

    private void Awake()
    {
        main = this;
        gold = startingGold;
    }

    public void AddGold(int _gold)
    {
        gold += _gold;
    }

    public Boolean RemoveGold(int _gold)
    {
        if (_gold > gold)
        {
            return false;
        }

        gold -= _gold;
        return true;
    }

    public int GetGold()
    {
        return gold;
    }
}
