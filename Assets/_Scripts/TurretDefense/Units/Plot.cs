using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private Color hoverColor;

    private Color originalColor;
    private GameObject turret;

    private void Start()
    {
        originalColor = gameObject.GetComponent<SpriteRenderer>().color;
    }
    private void OnMouseEnter()
    {
        gameObject.GetComponent<SpriteRenderer>().color = hoverColor;
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = originalColor;
    }

    private void OnMouseDown()
    {
        if (turret != null) return;

        BuildManager.main.SelectTurret(BuildManager.TowerType.Basic);

        if (CurrencyManager.main.RemoveGold(BuildManager.main.GetSelectedTurretCost()))
        {
            turret = BuildManager.main.GetSelectedTurret();
            Instantiate(turret, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Current Gold: " + CurrencyManager.main.GetGold() + "\nTurret Cost: " + BuildManager.main.GetSelectedTurretCost());
        }
    }
}
