using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputAssetSo inputAssetSo;
    [SerializeField] private GameObject inventory;
    [SerializeField] private Button closeBtn;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private List<Slot> slotList;
    
    private void Start()
    {
        inventory.SetActive(false);
        
        closeBtn.onClick.AddListener(InventoryClosed);
        inputAssetSo.OnPlayerOpenInventory += InventoryOpen;
        inventoryManager.OnNewItemAddedInventory += InventoryManager_OnNewItemAddedInventory;
    }

    private void InventoryManager_OnNewItemAddedInventory(List<IPickable> updatedInventoryItemsList)
    {
        UpdateVisual(updatedInventoryItemsList);
    }

    private void UpdateVisual(IReadOnlyList<IPickable> itemsList)
    {
        for (int i = 0; i < itemsList.Count; i++)
        {
            Slot slot = slotList[i];
            slot.SetImage(itemsList[i].GetItemSo().uiSprite);
            slot.SetText(itemsList[i].GetItemSo().itemText);
        }
    }
    
    private void InventoryOpen()
    {
        inventory.SetActive(true);
        inputAssetSo.SetCursorState(true);
    }

    private void InventoryClosed()
    {
        inventory.SetActive(false);
        inputAssetSo.SetCursorState(false);
        inputAssetSo.UiClosed();
    }
}
