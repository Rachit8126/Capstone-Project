using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public event Action<List<IPickable>> OnNewItemAddedInventory;
    
    [Header("References")]
    [SerializeField] private Transform inventoryHolder;
    [Header("Constants")]
    [SerializeField] private int inventoryCapacity;
        
    private List<IPickable> inventoryItemList;

    private void Awake()
    {
        inventoryItemList = new List<IPickable>();
    }
    
    public void AddItem(IPickable item)
    {
        if (inventoryItemList.Count < inventoryCapacity)
        {
            item.Pickup(inventoryHolder);
            inventoryItemList.Add(item);
            OnNewItemAddedInventory?.Invoke(inventoryItemList);
        }
    }
}