using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private List<GameObject> turretPrefabs;
    [SerializeField] private List<int> turretCost;

    private TowerType towerType;

    private void Awake()
    {
        main = this;
    }

    public void SelectTurret(TowerType _towerType)
    {
        towerType = _towerType;
    }

    public GameObject GetSelectedTurret()
    {
        if ((int)towerType >= turretPrefabs.Count)
        {
            Debug.Log("Cannot find prefab for tower type <" + towerType.ToString() + ">. Using Basic tower instead.");
            return turretPrefabs[(int)TowerType.Basic];
        }
        return turretPrefabs[(int)towerType];
    }

    public int GetSelectedTurretCost()
    {
        if ((int)towerType >= turretCost.Count)
        {
            Debug.Log("Cannot find cost for tower type <" + towerType.ToString() + ">. Using Basic tower instead.");
            return turretCost[(int)TowerType.Basic];
        }
        return turretCost[(int)towerType];
    }
    

    public enum TowerType
    {
        Basic = 0
    }
}
