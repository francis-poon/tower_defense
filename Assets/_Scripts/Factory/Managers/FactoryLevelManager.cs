using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FactoryLevelManager : MonoBehaviour
{
    public static FactoryLevelManager main;

    [Header("References")]
    [SerializeField] private GameObject gridPrefab;
    [SerializeField] private GameObject editingMenu;
    [SerializeField] private GameObject gameMenu;

    [Header("Attributes")]
    [SerializeField] private int gridHeight;
    [SerializeField] private int gridWidth;

    private float heightOffset;
    private float widthOffset;
    private GameObject[,] gridArray;
    private HashSet<GameObject> selectedObjects;

    private void Awake()
    {
        main = this;
        gridArray = new GameObject[gridHeight,gridWidth];
        selectedObjects = new HashSet<GameObject>();
    }

    private void Start()
    {
        heightOffset = (float)gridHeight / 2 - gridHeight + 0.5f;
        widthOffset = (float)gridWidth / 2 - gridWidth + 0.5f;
        gridArray = new GameObject[gridHeight, gridWidth];
        for (int height = 0; height < gridHeight; height++)
        {
            for (int width = 0; width < gridWidth; width++)
            {
                gridArray[height, width] = Instantiate(gridPrefab, ArrayIndexToPosition(width, height), Quaternion.identity);
            }
        }

        DisableEditingMenu();
        EnableGameMenu();
        Pause();
    }

    /*
     * ===================================================================================
     * Game Menu Handers
     * ===================================================================================
     */

    public void Play()
    {
        GameManager.Instance.ChangeState(GameState.Play);
    }

    public void Pause()
    {
        GameManager.Instance.ChangeState(GameState.Pause);
    }

    public void EnableEditingMenu()
    {
        if (!editingMenu.activeSelf)
        {
            GameManager.Instance.ChangeState(GameState.Editing);
            editingMenu.SetActive(true);
        }
    }

    public void DisableEditingMenu()
    {
        if (editingMenu.activeSelf)
        {
            editingMenu.SetActive(false);
        }
    }

    public void EnableGameMenu()
    {
        if (!gameMenu.activeSelf)
        {
            gameMenu.SetActive(true);
        }
    }

    public void DisableGameMenu()
    {
        if (gameMenu.activeSelf)
        {
            gameMenu.SetActive(false);
        }
    }

    /*
     * ===================================================================================
     * Editing Interface Button Handling Functions
     * ===================================================================================
     */

    public void RotateSelectedCounterClockwise()
    {
        foreach (GameObject obj in selectedObjects)
        {
            if (obj.GetComponent<GridPlot>().HasMachine())
            {
                obj.GetComponent<GridPlot>().GetMachine().GetComponent<FactoryComponent>().RotateCounterClockwise();
            }
        }
    }

    public void RotateSelectedClockwise()
    {
        foreach (GameObject obj in selectedObjects)
        {
            if (obj.GetComponent<GridPlot>().HasMachine())
            {
                obj.GetComponent<GridPlot>().GetMachine().GetComponent<FactoryComponent>().RotateClockwise();
            }
        }
    }

    public void DeselectAll()
    {
        foreach (GameObject obj in selectedObjects)
        {
            if (obj.GetComponent<GridPlot>().HasMachine())
            {
                obj.GetComponent<GridPlot>().GetMachine().GetComponent<FactoryComponent>().Deselect();
            }
        }
        selectedObjects.Clear();
    }

    public void SellSelected()
    {
        foreach (GameObject obj in selectedObjects)
        {
            if (obj.GetComponent<GridPlot>().HasMachine())
            {
                obj.GetComponent<GridPlot>().RemoveMachine();
            }
        }
        selectedObjects.Clear();
    }

    /*
     * ===================================================================================
     * Game Grid Component Handling
     * ===================================================================================
     */

    public void ProcessItem(GameObject _item)
    {
        GameObject grid = null;
        (int width, int height) = PositionToArrayIndex(_item.transform.position);

        if (width >= 0 && width < gridArray.GetLength(1) && height >= 0 && height < gridArray.GetLength(0))
        {
            grid = gridArray[height, width];
        }

        if (grid != null && grid.GetComponent<GridPlot>().HasMachine())
        {
            grid.GetComponent<GridPlot>().GetMachine().GetComponent<ItemHandler>().ProcessItem(_item);
        }
    }

    public void HandleFactoryComponentClick(FactoryComponent component)
    {
        (int width, int height) = PositionToArrayIndex(component.transform.position);
        GameObject clickedObject = gridArray[height, width];
        switch (GameManager.Instance.State)
        {
            case GameState.Editing:
                if (selectedObjects.Contains(clickedObject))
                {
                    selectedObjects.Remove(clickedObject);
                    component.Deselect();
                }
                else
                {
                    selectedObjects.Add(gridArray[height, width]);
                    component.Select();
                }
                break;
        }
    }

    /*
     * ===================================================================================
     * Helper Functions
     * ===================================================================================
     */
    private (int, int) PositionToArrayIndex(Vector2 position)
    {
        int width = (int)Mathf.Floor(position.x - transform.position.x - widthOffset + 0.5f);
        int height = (int)Mathf.Floor(-(position.y - transform.position.y + heightOffset));

        return (width, height);
    }

    private Vector2 ArrayIndexToPosition(int width, int height)
    {
        return (Vector2)transform.position + new Vector2((float)width + widthOffset, -((float)height + heightOffset));
    }
}
