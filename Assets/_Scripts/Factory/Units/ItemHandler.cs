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

    private Queue<ItemConversion> conversionQueue;
    private float conversionTimeLeft;
    private GameObject outputItem;
    private int recipeIndex;

    private void Awake()
    {
        conversionQueue = new Queue<ItemConversion>();
        recipeIndex = -1;
    }

    private void Update()
    {
        if (conversionQueue.Count == 0 && outputItem == null) return;

        if (outputItem == null)
        {
            ItemConversion itemConversion = conversionQueue.Dequeue();
            conversionTimeLeft = itemConversion.conversionTime;
            outputItem = itemConversion.outputItem;
        }
        else
        {
            conversionTimeLeft -= Time.deltaTime;
            if (conversionTimeLeft <= 0)
            {
                Instantiate(outputItem, transform.position, Quaternion.identity).GetComponent<ItemMovement>().MoveTo(sendPoint, itemMoveSpeed);
                outputItem = null;
            }
        }
        
    }
    public virtual void ProcessItem(GameObject _item)
    {
        if (recipeIndex >= 0 && recipeIndex < itemConversions.Count &&
            itemConversions[recipeIndex].inputItem.GetComponent<Item>().itemName == _item.GetComponent<Item>().itemName)
        {
            conversionQueue.Enqueue(itemConversions[recipeIndex]);
            Destroy(_item);
        }
        else
        {
            _item.GetComponent<ItemMovement>().MoveTo(sendPoint, itemMoveSpeed);
        }
    }

    public void SelectItemConversion(int _index)
    {
        if (_index < -1 || _index > itemConversions.Count)
        {
            Debug.LogWarning("Item Conversion index out of bounds, using None recipe.");
            recipeIndex = -1;
        }
        else
        {
            recipeIndex = _index;
        }
    }

    [Serializable]
    public class ItemConversion
    {
        public GameObject inputItem;
        public float conversionTime;
        public GameObject outputItem;
        public ItemConversion(GameObject _inputItem, float _conversionTime, GameObject _outputItem)
        {
            inputItem = _inputItem;
            conversionTime = _conversionTime;
            outputItem = _outputItem;
        }
    }
}
