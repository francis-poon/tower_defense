using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridPlot : MonoBehaviour
{
    // A7FFF1
    [Header("Attributes")]
    [SerializeField] private Color hoverColor;

    private Color originalColor;
    private GameObject machine;

    private void Start()
    {
        originalColor = gameObject.GetComponent<SpriteRenderer>().color;
    }
    private void OnMouseEnter()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            gameObject.GetComponent<SpriteRenderer>().color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = originalColor;
    }

    private void OnMouseDown()
    {
        if (machine != null) return;

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // TODO: Adding a Z: -1 to the position of the spawned machine so that it's box collider is selected over the grid's box collider
            // This is probably not the correct solution.
            machine = Instantiate(FactoryBuildManager.main.GetSelectedMachine(), transform.position + new Vector3(0f, 0f, -1f), Quaternion.identity);
        }
    }

    public bool HasMachine()
    {
        return machine != null;
    }

    public void RemoveMachine()
    {
        Destroy(machine);
        machine = null;
    }

    public GameObject GetMachine()
    {
        return machine;
    }
}
