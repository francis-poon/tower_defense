using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipePicker : MonoBehaviour
{
    private GameObject processor;

    public void SetProcessor(GameObject _processor)
    {
        processor = _processor;
    }

    public void SelectItemConversion(int _conversionIndex)
    {
        processor.GetComponent<ItemHandler>().SelectItemConversion(_conversionIndex);
        FactoryLevelManager.main.OnRecipeSelected();
    }
}
