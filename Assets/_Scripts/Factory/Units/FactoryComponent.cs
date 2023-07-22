using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FactoryComponent : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private Color baseColor;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color selectedColor;

    private Boolean isSelected;


    private void Awake()
    {
        GetComponent<SpriteRenderer>().color = baseColor;
        isSelected = false;
    }

    protected void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (isSelected) return;
        GetComponent<SpriteRenderer>().color = hoverColor;
    }

    private void OnMouseExit()
    {
        if (isSelected) return;
        GetComponent<SpriteRenderer>().color = baseColor;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        FactoryLevelManager.main.HandleFactoryComponentClick(this);
    }

    public void RotateCounterClockwise()
    {
        transform.Rotate(new Vector3(0f, 0f, 90f));
    }

    public void RotateClockwise()
    {
        transform.Rotate(new Vector3(0f, 0f, -90f));
    }

    public Boolean IsSelected()
    {
        return isSelected;
    }

    public void Select()
    {
        isSelected = true;
        GetComponent<SpriteRenderer>().color = selectedColor;
    }

    public void Deselect()
    {
        isSelected = false;
        GetComponent<SpriteRenderer>().color = baseColor;
    }
}
