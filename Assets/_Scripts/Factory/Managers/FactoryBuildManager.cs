using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryBuildManager : MonoBehaviour
{
    public static FactoryBuildManager main;

    [Header("References")]
    [SerializeField] private List<Machine> machines;

    private int machineIndex;

    private void Awake()
    {
        main = this;
    }

    public void SelectMachine(int _machineIndex)
    {
        if (machineIndex < machines.Count)
        {
            machineIndex = _machineIndex;
        }
        else
        {
            machineIndex = 0;
        }
        
    }

    public GameObject GetSelectedMachine()
    {
        if (machineIndex < machines.Count)
        {
            return machines[machineIndex].prefab;
        }
        return machines[0].prefab;
    }
}
