using UnityEngine;

public class TestItem : MonoBehaviour, IPickable
{
    [SerializeField] private ItemSo testItemSo;
    
    public void Pickup(Transform pickupParent)
    {
        gameObject.SetActive(false);
        transform.parent = pickupParent;
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    public ItemSo GetItemSo()
    {
        return testItemSo;
    }
}
