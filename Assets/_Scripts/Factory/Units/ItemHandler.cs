using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform sendPoint;

    [Header("Attributes")]
    [SerializeField] private List<ItemConversion> itemConversions;
    [SerializeField] private float itemMoveSpeed;

    private ItemConversion conversion;
    private Dictionary<string, int> inputRequirements;
    private Dictionary<string, int> inputStorage;
    private float conversionTimeLeft;
    private Boolean processing;

    private void Awake()
    {
        conversion = null;
        inputRequirements = new Dictionary<string, int>();
        inputStorage = new Dictionary<string, int>();
        processing = false;
    }

    private void Update()
    {
        if (conversion == null) return;

        if (InputRequirementsMet() && !processing)
        {
            Debug.Log("Starting conversion time: " + conversionTimeLeft);
            conversionTimeLeft = conversion.conversionTime;
            processing = true;
        }
        else if (processing)
        {
            conversionTimeLeft -= Time.deltaTime;
            if (conversionTimeLeft <= 0)
            {
                foreach (GameObject outputItem in conversion.outputItems)
                {
                    Instantiate(outputItem, transform.position, Quaternion.identity).GetComponent<ItemMovement>().MoveTo(sendPoint, itemMoveSpeed);
                }
                inputStorage.Clear();
                processing = false;
            }
        }
        
    }
    public virtual void ProcessItem(GameObject _item)
    {
        string itemName = _item.GetComponent<Item>().itemName;
        // TODO: This debug log is preventing proper runs for spawner
        
        if (conversion!= null && !processing && inputRequirements.ContainsKey(itemName) && inputStorage.GetValueOrDefault(itemName, 0) < inputRequirements[itemName])
        {
            if (!inputStorage.ContainsKey(itemName))
            {
                inputStorage[itemName] = 0;
            }
            inputStorage[itemName] += 1;
            Destroy(_item);
            
        }
        else
        {
            _item.GetComponent<ItemMovement>().MoveTo(sendPoint, itemMoveSpeed);
        }
    }

    public void SelectItemConversion(int _index)
    {
        inputRequirements = new Dictionary<string, int>();
        if (_index < -1 || _index > itemConversions.Count)
        {
            Debug.LogWarning("Item Conversion index out of bounds, using None recipe.");
            conversion = null;
        }
        else
        {
            conversion = itemConversions[_index];
            foreach (GameObject inputItem in conversion.inputItems)
            {
                if (inputRequirements.ContainsKey(inputItem.GetComponent<Item>().itemName))
                {
                    inputRequirements[inputItem.GetComponent<Item>().itemName] += 1;
                }
                else
                {
                    inputRequirements.Add(inputItem.GetComponent<Item>().itemName, 1);
                }
            }
        }
    }

    private Boolean InputRequirementsMet()
    {
        foreach (string itemName in inputRequirements.Keys)
        {
            if (inputStorage.GetValueOrDefault(itemName, 0) < inputRequirements[itemName])
            {
                return false;
            }
        }

        return true;
    }

    [Serializable]
    public class ItemConversion
    {
        public GameObject[] inputItems;
        public float conversionTime;
        public GameObject[] outputItems;
        public ItemConversion(GameObject[] _inputItems, float _conversionTime, GameObject[] _outputItems)
        {
            inputItems = _inputItems;
            conversionTime = _conversionTime;
            outputItems = _outputItems;
        }
    }
}
