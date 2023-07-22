using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform sendPoint;

    [Header("Attributes")]
    [SerializeField] private List<GameObject> inputItems;
    [SerializeField] private List<ItemConversion> itemConversions;
    [SerializeField] private float itemMoveSpeed;

    private Dictionary<string, ItemConversion> conversions;
    private Queue<ItemConversion> conversionQueue;
    private float conversionTimeLeft;
    private GameObject outputItem;


    private void Awake()
    {
        conversions = new Dictionary<string, ItemConversion>();
        for (int itemIndex = 0; itemIndex < inputItems.Count; itemIndex ++)
        {
            conversions.Add(inputItems[itemIndex].GetComponent<Item>().itemName, itemConversions[itemIndex]);
        }
        conversionQueue = new Queue<ItemConversion>();
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
    public void ProcessItem(GameObject _item)
    {
        if (conversions.ContainsKey(_item.GetComponent<Item>().itemName))
        {
            conversionQueue.Enqueue(conversions[_item.GetComponent<Item>().itemName]);
            Destroy(_item);
        }
        else
        {
            _item.GetComponent<ItemMovement>().MoveTo(sendPoint, itemMoveSpeed);
        }
    }
}
