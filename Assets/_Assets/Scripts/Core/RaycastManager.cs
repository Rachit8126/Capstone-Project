using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private InputAssetSo inputAssetSo;
    [Header("Constants")]
    [SerializeField] private float distance;

    private void Start()
    {
        inputAssetSo.OnPlayerPrimaryInteract += ShootRaycast;
    }

    private void ShootRaycast()
    {
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out RaycastHit hitInfo, distance))
        {
            if (hitInfo.collider.TryGetComponent(out IPickable pickable))
            {
                inventoryManager.AddItem(pickable);
            }
            else if (hitInfo.collider.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(raycastOrigin.position, raycastOrigin.forward * distance);
    }
}